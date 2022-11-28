using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �Q�[���p�b�h�p�̃J�[�\���ړ�����
/// </summary>
namespace nsTankLab
{
public class GamePadCursorMovement : MonoBehaviour
{
    //�ړ�����
    Vector2 m_moveDirection = Vector2.zero;

    [SerializeField, TooltipAttribute("�v���C���[�ԍ�"), Range(1,4)]int m_playerNum = 0;

    //�J�[�\���̈ړ����x
    const float CURSOR_SPEED = 800.0f;

    Rigidbody2D m_rigidbody2d = null;

        [SerializeField, TooltipAttribute("�C���Q�[�����ǂ���")] bool m_isInGame = false;

        Vector2 m_stickValue = Vector2.zero;

        void Start()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            if(m_isInGame)
            {
                m_stickValue = Gamepad.current.rightStick.ReadValue();
            }
            else
            {
                m_stickValue = Gamepad.current.leftStick.ReadValue();
            }
            m_moveDirection = m_stickValue;
        //�΂߂̋����������Ȃ��2�{�ɂȂ�̂�h���B
        m_moveDirection.Normalize();
        //�ړ������ɑ��x���|����(�ʏ�ړ�)
        m_moveDirection *= CURSOR_SPEED;

        //�ړ��������X�V
        m_rigidbody2d.velocity = m_moveDirection;
    }
}
}