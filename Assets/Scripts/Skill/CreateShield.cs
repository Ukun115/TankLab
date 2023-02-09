using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace nsTankLab
{
    /// <summary>
    /// �O���ɃV�[���h�𐶐�����X�L��
    /// </summary>
    public class CreateShield : MonoBehaviourPun
    {
        bool m_isPressedButton = false;

        ControllerData m_controllerData = null;

        SoundManager m_soundManager = null;

        SaveData m_saveData = null;

        int m_playerNum = 0;

        bool m_skillFlg = true;

        SkillCool m_skillCoolScript = null;
        int m_coolTime = 5;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", string.Empty));
        }

        void Update()
        {
            //�Q�[���i�s���~�܂��Ă���Ƃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            //����ؑ�
            SwitchingOperation();

            if (m_isPressedButton && m_skillFlg)
            {
                switch (m_saveData.GetSetSelectGameMode)
                {
                    //�`�������W���[�h,���[�J���v���C,�g���[�j���O
                    case "CHALLENGE":
                    case "LOCALMATCH":
                    case "TRAINING":
                        Create();
                        break;
                    //�I�����C���v���C
                    case "RANDOMMATCH":
                    case "PRIVATEMATCH":
                        if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
                        {
                            photonView.RPC(nameof(Create), RpcTarget.All);
                        }
                        else if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                        {
                            Create();
                        }
                        break;
                }
            }
        }

        [PunRPC]
        void Create()
        {
            m_skillFlg = false;

            ShieldInstantiate();

            //�������Đ�
            m_soundManager.PlaySE("DropBombSE");

            Invoke(nameof(Ct), m_coolTime);
            m_skillCoolScript.CoolStart(m_coolTime);
        }

        //����ؑ�
        void SwitchingOperation()
        {
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                if (m_controllerData.GetGamepad(1) is not null)
                {
                    m_isPressedButton = m_controllerData.GetGamepad(1).leftShoulder.wasPressedThisFrame;
                }
                else
                {
                    m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
                }
            }
            else
            {
                if (m_controllerData.GetGamepad(m_playerNum) is not null)
                {
                    m_isPressedButton = m_controllerData.GetGamepad(m_playerNum).leftShoulder.wasPressedThisFrame;
                }
                else
                {
                    m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
                }
            }
        }

        void ShieldInstantiate()
        {
            GameObject shieldObject = Instantiate(
                Resources.Load("ShieldWall") as GameObject,
                new Vector3(transform.position.x, -0.4f, transform.position.z),
                transform.rotation
            );
            shieldObject.name = $"{m_playerNum}PShield";
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Ct()
        {
            m_skillFlg = true;
        }

        public void SetSkillCoolScript(SkillCool skillCool)
        {
            m_skillCoolScript = skillCool;
        }
    }
}