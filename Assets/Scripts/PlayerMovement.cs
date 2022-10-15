using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �v���C���[�̈ړ��X�N���v�g
/// </summary>
public class PlayerMovement : MonoBehaviourPun
{
    //����
    Rigidbody m_rigidbody = null;

    //�ړ�����
    Vector3 m_moveDirection = Vector3.zero;

    //�ړ����x
    [SerializeField] float m_speed = 5.0f;

    //���E���L�[�̒l(-1.0f�`1.0f)
    float m_horizontal = 0.0f;
    //�㉺���L�[�̒l(-1.0f�`1.0f)
    float m_vertical = 0.0f;

    //�Z�[�u�f�[�^
    SaveData m_saveData = null;

    //�O�t���[���̃v���C���[�̈ʒu
    Vector3 beforeFramePosition;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    void Update()
    {
        //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

        //���E���L�[�̒l(-1.0f�`1.0f)���擾����
        m_horizontal = Input.GetAxis("Horizontal");
        //�㉺���L�[�̒l(-1.0f�`1.0f)���擾����
        m_vertical = Input.GetAxis("Vertical");

        //���͂��ꂽ�L�[�̒l��ۑ�
        m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
        m_moveDirection = transform.TransformDirection(m_moveDirection);

        //�΂߂̋����������Ȃ��2�{�ɂȂ�̂�h���B
        m_moveDirection.Normalize();

        //�ړ������ɑ��x���|����(�ʏ�ړ�)
        m_moveDirection *= m_speed;
    }

    void FixedUpdate()
    {
        //�v���C���[�̉�]����
        PlayerRotation();

        //���̂Ɉړ������蓖��(�ꏏ�ɏd�͂����蓖��)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
    }

    //�v���C���[�̉�]����
    void PlayerRotation()
    {
        //�O�t���[���Ƃ̈ʒu�̍�����i�s����������o���Ă��̕����ɉ�]���܂��B
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(beforeFramePosition.x, 0, beforeFramePosition.z);
        beforeFramePosition = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (m_moveDirection == new Vector3(0, 0, 0)) return;
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(m_rigidbody.transform.rotation, rot, 0.2f);
            this.transform.rotation = rot;
        }
    }
}
