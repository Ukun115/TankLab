using UnityEngine;

/// <summary>
/// �Q�[���p�b�h�p�̃J�[�\���ړ�����
/// </summary>
namespace nsTankLab
{
    public class GamePadCursorMovement : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�v���C���[�ԍ�"), Range(1,4)]int m_playerNum = 0;
        [SerializeField, TooltipAttribute("�C���Q�[�����ǂ���")] bool m_isInGame = false;

        //�ړ�����
        Vector2 m_moveDirection = Vector2.zero;

        //�J�[�\���̈ړ����x
        const float CURSOR_SPEED = 800.0f;

        Rigidbody2D m_rigidbody2d = null;

        Vector2 m_stickValue = Vector2.zero;

        ControllerData m_controllerData = null;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();
        }

        void Update()
        {
            if (m_isInGame)
            {
                m_stickValue = m_controllerData.GetGamepad(m_playerNum).rightStick.ReadValue();
            }
            else
            {
                m_stickValue = m_controllerData.GetGamepad(m_playerNum).leftStick.ReadValue();
            }
            m_moveDirection = m_stickValue;
            //�΂߂̋����������Ȃ��2�{�ɂȂ�̂�h���B
            m_moveDirection.Normalize();
            //�ړ������ɑ��x���|����(�ʏ�ړ�)
            m_moveDirection *= CURSOR_SPEED;

            //�ړ��������X�V
            m_rigidbody2d.velocity = m_moveDirection;
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_rigidbody2d = GetComponent<Rigidbody2D>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}