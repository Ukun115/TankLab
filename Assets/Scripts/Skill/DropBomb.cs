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

        bool m_skillFlg = true;

        bool m_isPressedButton = false;

        int m_playerNum = 0;

        ControllerData m_controllerData = null;

        SaveData m_saveData = null;

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
                m_skillFlg = false;

                BombInstantiate();

                //�ݒu���Đ�
                m_soundManager.PlaySE("DropBombSE");

                Invoke(nameof(Ct), m_coolTime);
                m_skillCoolScript.CoolStart(m_coolTime);
            }
        }

        //����ؑ�
        void SwitchingOperation()
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

        void BombInstantiate()
        {
            GameObject bombObject = Instantiate(
                Resources.Load("Bomb") as GameObject,
                new Vector3(transform.position.x,-0.4f, transform.position.z),
                transform.rotation
            );
            bombObject.GetComponent<ExplosionBomb>().SetDropPlayer(gameObject);
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
