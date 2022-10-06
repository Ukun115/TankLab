using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //サーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

     void Update()
    {
        //２人そろったら、
        if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            //デバック
            Debug.Log("ゲーム開始");

            //オンラインモードフラグを立てる
            GameObject.Find("SaveData").GetComponent<SaveData>().GetSetIsOnline = true;

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
        Debug.Log("サーバーへの接続が完了");
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
        PhotonNetwork.CreateRoom(null, roomOptions); //第一引数はルーム名

        //デバック
        Debug.Log("入室に失敗(部屋を作成)");
    }

    //入室が完了したときに呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        //デバック
        Debug.Log("入室が完了");
    }

    //ゲームシーンに移行する関数
    [PunRPC]
    void GoGameScene()
    {
        //ゲームシーンに移行
        SceneManager.LoadScene("Stage2");
    }
}
