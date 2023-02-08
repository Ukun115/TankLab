using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace nsTankLab
{
    public class RocketBullet : MonoBehaviourPun
    {
        bool m_isPressedButton = false;
        bool m_skillFlg = true;

        int m_playerNum = 0;

        ControllerData m_controllerData = null;

        SkillCool m_skillCoolScript = null;
        int m_coolTime = 5;

        SaveData m_saveData = null;

        SoundManager m_soundManager = null;

        void Start()
        {
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", string.Empty));

            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
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

            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                m_isPressedButton = m_controllerData.GetGamepad(m_playerNum).leftShoulder.wasPressedThisFrame;
            }
            else
            {
                m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
            }

            if (m_isPressedButton && m_skillFlg)
            {
                switch (m_saveData.GetSetSelectGameMode)
                {
                    //�`�������W���[�h,���[�J���v���C,�g���[�j���O
                    case "CHALLENGE":
                    case "LOCALMATCH":
                    case "TRAINING":
                        Create();
                        m_skillCoolScript.CoolStart(m_coolTime);
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
                        m_skillCoolScript.CoolStart(m_coolTime);
                        break;
                }
            }
        }

        [PunRPC]
        void Create()
        {
            m_skillFlg = false;
            RocketBulletInstantiate();

            //���ˉ����Đ�����
            m_soundManager.PlaySE("RocketBulletFireSE");

            Invoke(nameof(Ct), m_coolTime);
        }

        void RocketBulletInstantiate()
        {
            GameObject m_bulletObject = Instantiate(
                        Resources.Load("RocketBullet") as GameObject,
                        transform.position,
                        new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
                        );
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