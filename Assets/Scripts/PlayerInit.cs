using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInit : MonoBehaviourPunCallbacks
{
    GameObject m_gameObject = null;

    //�v���C���[�̃v���t�@�u
    [SerializeField] GameObject m_playerPrefab;

    Vector3[] m_initPosition = {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f)};

    void Start()
    {
        if (GameObject.Find("SaveData").GetComponent<SaveData>().GetSetIsOnline)
        {
            m_gameObject = PhotonNetwork.Instantiate(
                m_playerPrefab.name,
                m_initPosition[PhotonNetwork.LocalPlayer.ActorNumber - 1],    //�|�W�V����
                Quaternion.identity,        //��]
                0
                );
            //��������Q�[���I�u�W�F�N�g�̖��O��Player1or2�ɂ���
            m_gameObject.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
        }
        else
        {
            m_gameObject = Instantiate(
                m_playerPrefab,
                m_initPosition[0],    //�|�W�V����
                Quaternion.identity        //��]
                );
            //��������Q�[���I�u�W�F�N�g�̖��O��Player1�ɂ���
            m_gameObject.name = "Player1";
        }

        //�f�o�b�N
        Debug.Log("�v���C���[�ԍ��́u" + m_gameObject.name + "�v�ł��B");
    }
}
