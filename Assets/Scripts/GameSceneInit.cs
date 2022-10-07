using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameSceneInit : MonoBehaviourPunCallbacks
{
    //�v���C���[�̃v���t�@�u
    [SerializeField] GameObject m_playerPrefab;

    Vector3[] m_initPosition = {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f)};

    [SerializeField] TextMeshProUGUI[] m_playerNameText = null;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�I�����ꂽ�X�e�[�W�ɂ���ăX�e�[�W�𐶐�
        SceneManager.LoadScene(m_saveData.GetSetSelectStageName, LoadSceneMode.Additive);
        //�f�o�b�N
        Debug.Log(m_saveData.GetSetSelectStageName + "����������܂����B");

        if (m_saveData.GetSetIsOnline)
        {
            GameObject m_gameObject = PhotonNetwork.Instantiate(
                m_playerPrefab.name,
                m_initPosition[PhotonNetwork.LocalPlayer.ActorNumber - 1],    //�|�W�V����
                Quaternion.identity,        //��]
                0
                );
            //��������Q�[���I�u�W�F�N�g�̖��O��Player1or2�ɂ���
            m_gameObject.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;

            //�v���C���[���\��
            photonView.RPC(nameof(PlayerName), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
        }
        else
        {
            GameObject m_gameObject = Instantiate(
                m_playerPrefab,
                m_initPosition[0],    //�|�W�V����
                Quaternion.identity        //��]
                );
            //��������Q�[���I�u�W�F�N�g�̖��O��Player1�ɂ���
            m_gameObject.name = "Player1";

            //�v���C���[����\��
            m_playerNameText[0].text = PlayerPrefs.GetString("PlayerName");

            //�f�o�b�N
            Debug.Log("�v���C���[���Q�����܂����B");
        }
    }

    [PunRPC]
    void PlayerName(int num)
    {
        //�v���C���[����\��
        m_playerNameText[num-1].text = PlayerPrefs.GetString("PlayerName");

        //�f�o�b�N
        Debug.Log("�v���C���[" + num + "���Q�����܂����B");
    }
}
