using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

/// <summary>
/// �v���C���[�̈ړ��X�N���v�g
/// </summary>
namespace nsTankLab
{
    public class PlayerMovement : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("�v���C���[�̃g�����X�t�H�[��")] Transform m_playerTransform = null;
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

        //����
        Rigidbody m_rigidbody = null;

        ControllerData m_controllerData = null;

        //�ړ�����
        Vector3 m_moveDirection = Vector3.zero;

        //���E���L�[�̒l(-1.0f�`1.0f)
        float m_horizontal = 0.0f;
        //�㉺���L�[�̒l(-1.0f�`1.0f)
        float m_vertical = 0.0f;

        //���g�̃v���C���[�ԍ�
        int m_playerNum = 0;

        float m_skillSpeed = 1.0f;

        SaveData m_saveData = null;

        Vector2 m_stickValue = Vector2.zero;

        bool m_isTeleportStopping = false;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            //���g�̃v���C���[�ԍ����擾
            m_playerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", string.Empty));

            //�I�����ꂽ�^���N�ԍ��f�o�b�N
            Debug.Log($"<color=yellow>{name}�̃^���N : {m_saveData.GetSelectTankName(m_playerNum-1)}</color>");
            //�I�����ꂽ�X�L���ԍ��f�o�b�N
            Debug.Log($"<color=yellow>{name}�̃X�L�� : {m_saveData.GetSelectSkillName(m_playerNum-1)}</color>");
        }

        void Update()
        {
            //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            //�Q�[���i�s���~�܂��Ă���Ƃ��͎��s���Ȃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h����
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                m_stickValue = m_controllerData.GetGamepad(m_playerNum).leftStick.ReadValue();
                m_horizontal = m_stickValue.x;
                m_vertical = m_stickValue.y;
            }
            //�L�[�{�[�h����
            else
            {
                //D�L�[�������ꂽ
                if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    m_horizontal = 1.0f;
                }
                //A�L�[�������ꂽ
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    m_horizontal = -1.0f;
                }
                //W�L�[�������ꂽ
                if (Keyboard.current.wKey.wasPressedThisFrame)
                {
                    m_vertical = 1.0f;
                }
                //S�L�[�������ꂽ
                if (Keyboard.current.sKey.wasPressedThisFrame)
                {
                    m_vertical = -1.0f;
                }

                //D�L�[�������ꂽ
                if(Keyboard.current.dKey.wasReleasedThisFrame)
                {
                    m_horizontal = 0.0f;
                }
                //A�L�[�������ꂽ
                if (Keyboard.current.aKey.wasReleasedThisFrame)
                {
                    m_horizontal = 0.0f;
                }

                //W�L�[�������ꂽ
                if (Keyboard.current.wKey.wasReleasedThisFrame)
                {
                    m_vertical = 0.0f;
                }
                //S�L�[�������ꂽ
                if (Keyboard.current.sKey.wasReleasedThisFrame)
                {
                    m_vertical = 0.0f;
                }
            }

            //���͂��ꂽ�L�[�̒l��ۑ�
            m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
            m_moveDirection = m_playerTransform.TransformDirection(m_moveDirection);
            //�΂߂̋����������Ȃ�(��2�{�ɂȂ�)�̂�h���B
            m_moveDirection.Normalize();

            //�ړ������ɑ��x���|����(�ʏ�ړ�)
            m_moveDirection *= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_playerNum-1)].GetTankSpeed() * m_skillSpeed;
        }

        void FixedUpdate()
        {
            //�Q�[���i�s���~�܂��Ă���Ƃ��͈ړ����x��0�ɂ���B
            if (!m_saveData.GetSetmActiveGameTime || m_isTeleportStopping)
            {
                m_moveDirection = Vector3.zero;
            }
            //���̂Ɉړ������蓖��(�ꏏ�ɏd�͂����蓖��)
            m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
        }

        public Vector3 GetMoveDirection()
        {
            return m_moveDirection;
        }

        public float GetVertical()
        {
            return m_vertical;
        }
        public float SetSkillSpeed(float skillspeed)
        {
            return m_skillSpeed = skillspeed;
        }

        public Vector3 GetSetPlayerPosition
        {
            get { return m_playerTransform.position; }
            set { m_playerTransform.position = value; }
        }

        public void SetTeleportStopping(bool isStop)
        {
            m_isTeleportStopping = isStop;
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_rigidbody = GetComponent<Rigidbody>();
        }
    }
}