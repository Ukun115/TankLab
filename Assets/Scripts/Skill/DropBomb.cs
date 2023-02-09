using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    public class DropBomb : MonoBehaviourPun
    {
        SoundManager m_soundManager = null;

        bool m_isPressedButton = false;

        int m_playerNum = 0;

        ControllerData m_controllerData = null;

        SaveData m_saveData = null;

        SkillCool m_skillCoolScript = null;
        int m_coolTime = 5;

        ExplosionBomb m_nowDroppingBomb = null;

        enum EnState
        {
            //�ݒu�\���
            enDroppableState,
            //�ݒu���Ă�����
            enDroppingState,
            //�N�����ăN�[���^�C�����������Ă�����
            enCoolState
        }
        EnState m_state = EnState.enDroppableState;

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

            //�X�L���{�^���������ꂽ�Ƃ��A
            if (m_isPressedButton)
            {
                //�ݒu�\��Ԃ̂Ƃ��A
                if (m_state == EnState.enDroppableState)
                {
                    switch (m_saveData.GetSetSelectGameMode)
                    {
                        //�`�������W���[�h,���[�J���v���C,�g���[�j���O
                        case "CHALLENGE":
                        case "LOCALMATCH":
                        case "TRAINING":
                            Drop();
                            break;
                        //�I�����C���v���C
                        case "RANDOMMATCH":
                        case "PRIVATEMATCH":
                            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
                            {
                                photonView.RPC(nameof(Drop), RpcTarget.All);
                            }
                            else if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                            {
                                Drop();
                            }
                            break;
                    }
                }
                //�ݒu���Ă����Ԃ̎��A
                else if(m_state == EnState.enDroppingState)
                {
                    //�{���N���J�n
                    m_nowDroppingBomb.ActiveFlashing();
                }
            }
        }

        [PunRPC]
        void Drop()
        {
           //�ݒu������Ԃɂ���
            m_state = EnState.enDroppingState;

            BombInstantiate();

            //�ݒu���Đ�
            m_soundManager.PlaySE("DropBombSE");
        }

        //�N�[���J�n����
        public void CoolStart()
        {
            m_state = EnState.enCoolState;
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

        void BombInstantiate()
        {
            GameObject bombObject = Instantiate(
                Resources.Load("Bomb") as GameObject,
                new Vector3(transform.position.x,-0.4f, transform.position.z),
                transform.rotation
            );
            bombObject.GetComponent<ExplosionBomb>().SetDropPlayer(gameObject);

            m_nowDroppingBomb = null;
            m_nowDroppingBomb = bombObject.GetComponent<ExplosionBomb>();
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
            //�ݒu�\��Ԃɖ߂�
            m_state = EnState.enDroppableState;
        }

        public void SetSkillCoolScript(SkillCool skillCool)
        {
            m_skillCoolScript = skillCool;
        }
    }
}
