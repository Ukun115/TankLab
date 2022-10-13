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

        //自分自身じゃなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            if (m_saveData.GetSetPlayerNum == 0)
            {
                //生成するゲームオブジェクトの名前をPlayer2にする
                m_objectName = "2P";
                //カラーを設定する(赤)
                m_materialNum = 1;
            }
            else
            {
                //生成するゲームオブジェクトの名前をPlayer1にする
                m_objectName = "1P";
                //カラーを設定する(青)
                m_materialNum = 0;
            }
        }
        //自分自身だったら、
        else
        {
            //生成するゲームオブジェクトの名前をPlayer1or2にする
            m_objectName =(m_saveData.GetSetPlayerNum + 1) + "P";
            //カラーを設定する(赤or青)
            m_materialNum = m_saveData.GetSetPlayerNum;
        }

        this.gameObject.name = m_objectName;
        this.gameObject.GetComponent<MeshRenderer>().material = m_tankColor[m_materialNum];
    }
}
