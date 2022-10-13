using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameSceneInit : MonoBehaviourPunCallbacks
{
    //プレイヤーのプレファブ
    [SerializeField] GameObject m_playerPrefab;

    Vector3[] m_initPosition = {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f)};

    [SerializeField] TextMeshProUGUI[] m_playerNameText = null;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //選択されたステージによってステージを生成
        SceneManager.LoadScene(m_saveData.GetSetSelectStageName, LoadSceneMode.Additive);
        //デバック
        Debug.Log(m_saveData.GetSetSelectStageName + "が生成されました。");

        if (m_saveData.GetSetIsOnline)
        {
            PhotonNetwork.IsMessageQueueRunning = true;

            GameObject m_gameObject = PhotonNetwork.Instantiate(
                m_playerPrefab.name,
                m_initPosition[PhotonNetwork.LocalPlayer.ActorNumber - 1],    //ポジション
                Quaternion.identity,        //回転
                0
                );
            //プレイヤー番号を保存
            m_saveData.GetSetPlayerNum = (PhotonNetwork.LocalPlayer.ActorNumber-1);

            //プレイヤー名表示
            photonView.RPC(nameof(PlayerName), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber-1, PlayerPrefs.GetString("PlayerName"));
        }
        else
        {
            GameObject m_gameObject = Instantiate(
                m_playerPrefab,
                m_initPosition[0],    //ポジション
                Quaternion.identity        //回転
                );
            //生成するゲームオブジェクトの名前をPlayer1にする
            m_gameObject.name = "Player1";

            //プレイヤー名を表示
            m_playerNameText[0].text = PlayerPrefs.GetString("PlayerName");

            //デバック
            Debug.Log("プレイヤーが参加しました。");
        }
    }

    [PunRPC]
    void PlayerName(int num,string playerName)
    {
        //プレイヤー名を表示
        m_playerNameText[num].text = playerName;

        //デバック
        Debug.Log("プレイヤー" + num + "が参加しました。");
    }
}
