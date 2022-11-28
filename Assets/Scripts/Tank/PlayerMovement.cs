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
    //����
    Rigidbody m_rigidbody = null;

    ControllerData m_controllerData = null;

    //�ړ�����
    Vector3 m_moveDirection = Vector3.zero;

    [SerializeField, TooltipAttribute("�v���C���[�̃g�����X�t�H�[��")] Transform m_playerTransform = null;

    //���E���L�[�̒l(-1.0f�`1.0f)
    float m_horizontal = 0.0f;
    //�㉺���L�[�̒l(-1.0f�`1.0f)
    float m_vertical = 0.0f;

    [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

    //���g�̃v���C���[�ԍ�
    int m_myPlayerNum = 0;

    float m_skillSpeed = 1.0f;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        m_rigidbody = GetComponent<Rigidbody>();

        //���g�̃v���C���[�ԍ����擾
        m_myPlayerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", ""));

        //�I�����ꂽ�^���N�ԍ��f�o�b�N
        Debug.Log($"<color=yellow>{name}�̃^���N : Tank{m_saveData.GetSelectTankNum(m_myPlayerNum-1)}</color>");
        //�I�����ꂽ�X�L���ԍ��f�o�b�N
        Debug.Log($"<color=yellow>{name}�̃X�L�� : Skill{m_saveData.GetSelectSkillNum(m_myPlayerNum-1)}</color>");
    }

    void Update()
    {
        //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
        if (SceneManager.GetActiveScene().name == "OnlineGameScene" && !photonView.IsMine)
        {
            return;
        }

        //���͂��ꂽ�L�[�̒l��ۑ�
        m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
        m_moveDirection = m_playerTransform.TransformDirection(m_moveDirection);
        //�΂߂̋����������Ȃ�(��2�{�ɂȂ�)�̂�h���B
        m_moveDirection.Normalize();

        //�ړ������ɑ��x���|����(�ʏ�ړ�)
        m_moveDirection *= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum-1)].GetTankSpeed() * m_skillSpeed;
    }

    void FixedUpdate()
    {
            //�Q�[���i�s���~�܂��Ă���Ƃ��͈ړ����x��0�ɂ���B
            if (!m_saveData.GetSetmActiveGameTime)
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

        /// <summary>
        /// �ړ�����i�㉺���E�L�[�Ȃǁj���擾
        /// </summary>
        /// <param name="movementValue"></param>
        void OnMove(InputValue movementValue)
        {
            //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
            if (SceneManager.GetActiveScene().name == "OnlineGameScene" && !photonView.IsMine)
            {
                return;
            }

            // Move�A�N�V�����̓��͒l���擾
            Vector2 movementVector = movementValue.Get<Vector2>();

            // x,y�������̓��͒l��ϐ��ɑ��
            m_horizontal = movementVector.x;
            m_vertical = movementVector.y;
        }
    }
}