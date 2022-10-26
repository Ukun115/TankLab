using UnityEngine;

/// <summary>
/// �Q�[���p�b�h�p�̃J�[�\���ړ�����
/// </summary>
public class GamePadCursorMovement : MonoBehaviour
{
    //���E���L�[�̒l(-1.0f�`1.0f)
    float m_horizontal = 0.0f;
    //�㉺���L�[�̒l(-1.0f�`1.0f)
    float m_vertical = 0.0f;

    //�ړ�����
    Vector2 m_moveDirection = Vector2.zero;

    [SerializeField, TooltipAttribute("�v���C���[�ԍ�"), Range(1,4)]int m_playerNum = 0;

    //�J�[�\���̈ړ����x
    const float CURSOR_SPEED = 800.0f;

    Rigidbody2D m_rigidbody2d = null;

    void Start()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //���E���L�[�̒l(-1.0f�`1.0f)���擾����
        m_horizontal = Input.GetAxisRaw($"{m_playerNum}PJoystickHorizontal_R");
        //�㉺���L�[�̒l(-1.0f�`1.0f)���擾����
        m_vertical = Input.GetAxisRaw($"{m_playerNum}PJoystickVertical_R");

        m_moveDirection = new Vector2(m_horizontal,m_vertical);
        //�΂߂̋����������Ȃ��2�{�ɂȂ�̂�h���B
        m_moveDirection.Normalize();
        //�ړ������ɑ��x���|����(�ʏ�ړ�)
        m_moveDirection *= CURSOR_SPEED;

        //�ړ��������X�V
        m_rigidbody2d.velocity = m_moveDirection;
    }
}