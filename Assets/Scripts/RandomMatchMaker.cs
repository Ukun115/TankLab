using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{
    [SerializeField]TextMeshProUGUI m_matchedText = null;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //Photonサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

     void Update()
    {
        //２人そろったら、
        if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            //デバック
            Debug.Log("ゲーム開始");

            //ゲームシーンに遷移
            photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        }
    }

    //サーバーへの接続が完了すると呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        //ランダムで部屋に入室する
        PhotonNetwork.JoinRandomRoom();

        //デバック
        Debug.Log("Photonサーバーへの接続が完了");
    }

    //ロビーへの入室が完了すると呼ばれるコールバック
    public override void OnJoinedLobby()
    {
        //ロビーに入室したら即座に部屋に入室する
        PhotonNetwork.JoinRandomRoom();

        //デバック
        Debug.Log("ロビーへの入室が完了");
    }

    // 入室に失敗した場合に呼ばれるコールバック
    // １人目は部屋がないため必ず失敗するので部屋を作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // 最大2人まで入室可能
        PhotonNetwork.JoinOrCreateRoom(m_saveData.GetSetInputPassword, roomOptions,TypedLobby.Default); //第一引数はルーム名

        //デバック
        Debug.Log("入室に失敗(部屋を作成)");
        Debug.Log("ルーム名は" + m_saveData.GetSetInputPassword + "です。");
    }

    //入室が完了したときに呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        //デバック
        Debug.Log("入室が完了");
    }

    //このスクリプトをアタッチしているオブジェクトが破棄されたときに呼ばれる
    //(シーンが切り替わったときに呼ばれる)
    void OnDestroy()
    {
        //オンラインがつながってなかったら、
        if (PhotonNetwork.LocalPlayer.ActorNumber != 2)
        {
            //フォトンサーバーから切断する
            PhotonNetwork.Disconnect();

            Debug.Log("Photonサーバーから切断しました");
        }
    }

    // ルームから退出した時に呼ばれるコールバック
    public override void OnLeftRoom()
    {
        Debug.Log("ルームから退出しました");
    }

    //ゲームシーンに移行する関数
    [PunRPC]
    void GoGameScene()
    {
        //マッチング完了テキストを表示
        m_matchedText.text = "Matched!!";

        //オンラインモードフラグを立てる
        m_saveData.GetSetIsOnline = true;

        //ゲームシーンに移行
        SceneManager.LoadScene("OnlineGameScene");
    }
}