using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// オンラインのマッチングの処理
/// </summary>
namespace nsTankLab
{
    public class OnlineMatchingMaker : MonoBehaviourPunCallbacks
    {
        [SerializeField, TooltipAttribute("マッチング完了テキスト")] TextMeshProUGUI m_matchedText = null;

        //セーブデータ
        SaveData m_saveData = null;
        //オンラインルームのオプション
        RoomOptions m_roomOptions = new RoomOptions();

        SoundManager m_soundManager = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            // シーンの自動同期: 有効
            PhotonNetwork.AutomaticallySyncScene = true;
            // 最大2人までルームに参加可能
            m_roomOptions.MaxPlayers = 2;

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
            Debug.Log("<color=blue>Photonサーバーへの接続が完了</color>");

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
                //プライベートルームに参加する。参加できるルームがなかったら、ルームを作成する。
                PhotonNetwork.JoinOrCreateRoom(m_saveData.GetSetInputPassword,m_roomOptions,TypedLobby.Default);
            }
            //ランダムマッチの場合
            else
            {
                //ランダムなルームに参加する。参加できるルームがなかったら、ルームを作成する。
                PhotonNetwork.JoinRandomOrCreateRoom(null);
            }

            //シーン遷移しても破壊されないようにする
            DontDestroyOnLoad(this);

            //デバック
            Debug.Log("<color=blue>ロビーへの参加が完了</color>");
        }

        // ルームの作成が成功した時に呼ばれるコールバック
        public override void OnCreatedRoom()
        {
            Debug.Log("<color=blue>ルームの作成に成功しました</color>");
        }

        // ルームの作成が失敗した時に呼ばれるコールバック
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("<color=blue>ルームの作成に失敗しました</color>");
        }

        // ルームへの参加が成功した時に呼ばれるコールバック
        public override void OnJoinedRoom()
        {
            Debug.Log("<color=blue>ルームへ参加しました</color>");
        }

        // ルーム名を指定したルームへの参加が失敗した時に呼ばれるコールバック
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("<color=blue>プライベートルームへの参加に失敗しました</color>");
        }

        // ランダムルームへの参加が失敗した時に呼ばれるコールバック
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            // ランダムルームが存在しないなら、新規でルームを作成する
            PhotonNetwork.CreateRoom(null);

            Debug.Log("<color=blue>ランダムルームへの参加に失敗しました。ランダムルームを作成します。</color>");
        }

        //他プレイヤーがルームに参加したときに呼ばれるコールバック
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("<color=blue>他プレイヤーがルームに参加しました。</color>");

            int randomNum = Random.Range(1, 10);
            photonView.RPC(nameof(Matched), RpcTarget.All,randomNum);

            //ゲームシーンに遷移
            //選択されているステージを抽選
            Invoke(nameof(DelayGoGameScene),2.0f);
            //ゲーム中に他プレイヤーがルームに参加してこないようにしておく
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        void DelayGoGameScene()
        {
            photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        }

        //このスクリプトをアタッチしているオブジェクトが破棄されたときに呼ばれる
        //(シーンが切り替わったときに呼ばれる)
        void OnDestroy()
        {
            if (PhotonNetwork.IsConnected)
            {
                //フォトンサーバーから切断する
                PhotonNetwork.Disconnect();

                Debug.Log("<color=blue>Photonサーバーから切断しました</color>");

                Destroy(this);
            }

           //オンラインモードフラグをおる
           m_saveData.GetSetIsOnline = false;
        }

        // ルームから退出した時に呼ばれるコールバック
        public override void OnLeftRoom()
        {
            Debug.Log("ルームから退出しました</color>");
        }

        //同じルームにいたほかプレイヤーが退出したときに呼ばれるコールバック
        public override void OnPlayerLeftRoom(Player player)
        {
            Debug.Log("他プレイヤーがルームから退出しました</color>");
        }

        [PunRPC]
        void Matched(int stageNum)
        {
            //マッチング完了テキストを表示
            m_matchedText.text = "Matched!!";

            //マッチング完了音を再生する
            m_soundManager.PlaySE("MatchingSE");

            m_saveData.GetSetSelectStageNum = stageNum;
        }

        //ゲームシーンに移行する関数
        [PunRPC]
        void GoGameScene()
        {
            //ゲームシーンに移行
            PhotonNetwork.LoadLevel("OnlineGameScene");

            PhotonNetwork.IsMessageQueueRunning = false;
        }
    }
}