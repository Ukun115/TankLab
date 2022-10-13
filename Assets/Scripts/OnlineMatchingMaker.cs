using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// オンラインのマッチングの処理
/// </summary>
public class OnlineMatchingMaker : MonoBehaviourPunCallbacks
{
    //マッチング完了！のテキスト
    [SerializeField]TextMeshProUGUI m_matchedText = null;
    //セーブデータ
    SaveData m_saveData = null;
    //オンラインルームのオプション
    RoomOptions m_roomOptions = new RoomOptions();

    void Awake()
    {
        // シーンの自動同期: 有効
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        m_roomOptions.MaxPlayers = 2; // 最大2人まで入室可能

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //オンラインモードフラグを立てる
        m_saveData.GetSetIsOnline = true;

        //Photonサーバーに接続
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //サーバーへの接続が完了すると呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        //デバック
        Debug.Log("Photonサーバーへの接続が完了");

        //ロビーに参加する
        PhotonNetwork.JoinLobby();
    }

    //ロビーへの参加が完了すると呼ばれるコールバック
    public override void OnJoinedLobby()
    {
        //プライベートマッチの場合
        if (m_saveData.GetSetInputPassword != "----")
        {
            //非公開にする
            m_roomOptions.IsVisible = false;
            PhotonNetwork.JoinOrCreateRoom(m_saveData.GetSetInputPassword,m_roomOptions,TypedLobby.Default);
        }
        //ランダムマッチの場合
        else
        {
            PhotonNetwork.JoinRandomOrCreateRoom(null);
        }
        //破壊されないようにする
        DontDestroyOnLoad(this);

        //デバック
        Debug.Log("ロビーへの入室が完了");
    }

    // ルームの作成が成功した時に呼ばれるコールバック
    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成に成功しました");
    }

    // ルームの作成が失敗した時に呼ばれるコールバック
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("ルームの作成に失敗しました");
    }

    // ルームへの参加が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームへ参加しました");
    }

    // ルーム名を指定したルームへの参加が失敗した時に呼ばれるコールバック
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("プライベートルームへの参加に失敗しました");
    }

    // ランダムなルームへの参加が失敗した時に呼ばれるコールバック
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // ランダムで参加できるルームが存在しないなら、新規でルームを作成する
        PhotonNetwork.CreateRoom(null);

        Debug.Log("ランダムルームへの参加に失敗しました。ランダムルームを作成します。");
    }

    //他プレイヤーがルームに参加したときに呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("他プレイヤーがルームに参加しました。");
        //ゲームシーンに遷移
        photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        //他プレイヤーが入ってこれないようにしておく
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    //このスクリプトをアタッチしているオブジェクトが破棄されたときに呼ばれる
    //(シーンが切り替わったときに呼ばれる)
    void OnDestroy()
    {
        if (PhotonNetwork.IsConnected)
        {
            //フォトンサーバーから切断する
            PhotonNetwork.Disconnect();

            Debug.Log("Photonサーバーから切断しました");

            Destroy(this);
        }

       //オンラインモードフラグをおる
       m_saveData.GetSetIsOnline = false;
    }

    // ルームから退出した時に呼ばれるコールバック
    public override void OnLeftRoom()
    {
        Debug.Log("ルームから退出しました");
    }

    //同じルームにいたほかプレイヤーが退出したときに呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player player)
    {
        Debug.Log("他プレイヤーがルームから退出しました");
    }

    //ゲームシーンに移行する関数
    [PunRPC]
    void GoGameScene()
    {
        //マッチング完了テキストを表示
        m_matchedText.text = "Matched!!";

        PhotonNetwork.IsMessageQueueRunning = false;

        //ゲームシーンに移行
        PhotonNetwork.LoadLevel("OnlineGameScene");
    }
}