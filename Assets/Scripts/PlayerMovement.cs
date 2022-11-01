using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �v���C���[�̈ړ��X�N���v�g
/// </summary>
public class PlayerMovement : Photon.Pun.MonoBehaviourPun
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

    float m_latestVertical;

    SaveData m_saveData = null;

    Vector3 latestPos;

    void Start()
    {
        //Rigidbody�̃R���|�[�l���g���擾����
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
        //�O�t���[���Ƃ̈ʒu�̍�����i�s����������o���Ă��̕����ɉ�]���܂��B
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f )
        {
			if (m_moveDirection == new Vector3(0, 0, 0)) return;
			if (m_vertical > 0 && m_latestVertical > 0)
			{
				Quaternion rot = Quaternion.LookRotation(differenceDis);
				rot = Quaternion.Slerp(m_rigidbody.transform.rotation, rot, 0.1f);
				this.transform.rotation = rot;
			}
			if (m_vertical < 0 && m_latestVertical < 0)
			{
				Quaternion rot = Quaternion.LookRotation(-differenceDis);
				rot = Quaternion.Slerp(m_rigidbody.transform.rotation, rot, 0.1f);
				this.transform.rotation = rot;
			}
		}
        m_latestVertical = m_vertical;

        //���̂Ɉړ������蓖��(�ꏏ�ɏd�͂����蓖��)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
    }
}
