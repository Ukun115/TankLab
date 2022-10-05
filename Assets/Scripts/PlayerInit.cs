using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInit : MonoBehaviourPunCallbacks
{
    GameObject m_gameObject = null;

    //�v���C���[�̃v���t�@�u
    [SerializeField] GameObject m_playerPrefab;

    Vector3[] m_initPosition = {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f)};

    void Start()
    {
        m_gameObject = PhotonNetwork.Instantiate(
            m_playerPrefab.name,
            m_initPosition[PhotonNetwork.LocalPlayer.ActorNumber-1],    //�|�W�V����
            Quaternion.identity,        //��]
            0
            );
        //��������Q�[���I�u�W�F�N�g�̖��O��Player1or2�ɂ���
        m_gameObject.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;

        //�f�o�b�N
        Debug.Log("�v���C���[�ԍ��́u" + m_gameObject.name + "�v�ł��B");
    }
}
