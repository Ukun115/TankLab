using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;

/// <summary>
/// �v���C���[�̈ړ��X�N���v�g
/// </summary>
public class PlayerMovement : MonoBehaviourPun
{
    //����
    Rigidbody m_rigidbody = null;

    ControllerData m_controllerData = null;

    //�ړ�����
    Vector3 m_moveDirection = Vector3.zero;

    [SerializeField, TooltipAttribute("�v���C���[�ړ����x")]float m_speed = 5.0f;

    [SerializeField, TooltipAttribute("�v���C���[�̃g�����X�t�H�[��")] Transform m_playerTransform = null;

    //���E���L�[�̒l(-1.0f�`1.0f)
    float m_horizontal = 0.0f;
    //�㉺���L�[�̒l(-1.0f�`1.0f)
    float m_vertical = 0.0f;

    //�������̖͂��O(�����ݒ�̓L�[�{�[�h����)
    string m_inputHorizontalName = "Horizontal";
    //�������̖͂��O(�����ݒ�̓L�[�{�[�h����)
    string m_inputVerticalName = "Vertical";

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h�ł̑���
        if (m_controllerData.GetIsConnectedController(int.Parse(Regex.Replace(this.transform.root.name, @"[^0-9]", ""))))
        {
            m_inputHorizontalName = $"{this.name}JoystickHorizontal_L";
            m_inputVerticalName = $"{this.name}JoystickVertical_L";
        }
    }

    void Update()
    {
        //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
        if (SceneManager.GetActiveScene().name == "OnlineGameScene" && !photonView.IsMine)
        {
            return;
        }

        //Input.GetAxisRaw()�́A���͂��ꂽ���ɕ␳���������A-1��0��1�ł�float�^�ŕԂ��܂��B
        //Input.GetAxis()�́A���͂��ꂽ���ɕ␳�������āA-1����1�͈̔͂�float�^�ŕԂ��܂��B

        //���E���L�[�̒l(-1.0f�`1.0f)���擾����
        m_horizontal = Input.GetAxisRaw(m_inputHorizontalName);
        //�㉺���L�[�̒l(-1.0f�`1.0f)���擾����
        m_vertical = Input.GetAxisRaw(m_inputVerticalName);

        //���͂��ꂽ�L�[�̒l��ۑ�
        m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
        m_moveDirection = m_playerTransform.TransformDirection(m_moveDirection);
        //�΂߂̋����������Ȃ��2�{�ɂȂ�̂�h���B
        m_moveDirection.Normalize();

        //�ړ������ɑ��x���|����(�ʏ�ړ�)
        m_moveDirection *= m_speed;
    }

    void FixedUpdate()
    {
        //���̂Ɉړ������蓖��(�ꏏ�ɏd�͂����蓖��)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
    }

    public Vector3 GetMoveDirection()
    {
        return m_moveDirection;
    }
}
