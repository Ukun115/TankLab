using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// プレイヤーの初期化処理
/// </summary>
public class PlayerInit : MonoBehaviourPun
{
    //1P2Pの機体のマテリアルカラー(1P:Blue,2P:Red)
    [SerializeField] Material[] m_tankColor = new Material[2];
    //マテリアル番号
    int m_materialNum = 0;
    //プレイヤーオブジェクト名
    string m_objectName = "";

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //オンラインだったら、
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
        //オフラインだったら、
        else
        {
            //生成するゲームオブジェクトの名前をPlayer1or2にする
            m_objectName =(m_saveData.GetSetPlayerNum + 1) + "P";
            //カラーを設定する(赤or青)
            m_materialNum = m_saveData.GetSetPlayerNum;
        }

        //タンクの名前とカラーを変更
        this.gameObject.name = m_objectName;
        this.gameObject.GetComponent<MeshRenderer>().material = m_tankColor[m_materialNum];
    }
}
