using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInit : Photon.Pun.MonoBehaviourPun
{
    [SerializeField] Material[] m_tankColor = new Material[2];
    int m_materialNum = 0;
    string m_objectName = "";

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�������g����Ȃ�������A
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            if (m_saveData.GetSetPlayerNum == 0)
            {
                //��������Q�[���I�u�W�F�N�g�̖��O��Player2�ɂ���
                m_objectName = "2P";
                //�J���[��ݒ肷��(��)
                m_materialNum = 1;
            }
            else
            {
                //��������Q�[���I�u�W�F�N�g�̖��O��Player1�ɂ���
                m_objectName = "1P";
                //�J���[��ݒ肷��(��)
                m_materialNum = 0;
            }
        }
        //�������g��������A
        else
        {
            //��������Q�[���I�u�W�F�N�g�̖��O��Player1or2�ɂ���
            m_objectName =(m_saveData.GetSetPlayerNum + 1) + "P";
            //�J���[��ݒ肷��(��or��)
            m_materialNum = m_saveData.GetSetPlayerNum;
        }

        this.gameObject.name = m_objectName;
        this.gameObject.GetComponent<MeshRenderer>().material = m_tankColor[m_materialNum];
    }
}
