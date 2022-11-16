using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// ゲームシーンの初期化処理
/// </summary>
namespace nsTankLab
{
public class GameSceneInit : MonoBehaviourPunCallbacks
{
    //セーブデータ
    SaveData m_saveData = null;
    SoundManager m_soundManager = null;

    [SerializeField, TooltipAttribute("ローカル時の各プレイヤーの位置")] Transform[] m_localPlayerPosition = null;
    //ステージごとのプレイヤーの初期位置(ローカルの初期位置)
    Vector3[][] m_stageLocalPlayerInitPosition =
    {
        //ステージ1
          new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)},
          //ステージ2
          new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)}
    };

    /// <summary>
    /// オンラインの場合のプレイヤー生成で使用する変数たち
    /// </summary>
    [SerializeField, TooltipAttribute("オンライン時のプレイヤーオブジェクト")] GameObject m_onlinePlayerPrefab = null;
    //ステージごとのプレイヤーの初期位置(オンラインの初期位置)
    Vector3[][] m_stageOnlinePlayerInitPosition =
    {
        //ステージ1
        new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
        //ステージ2
        new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
    };
    [SerializeField, TooltipAttribute("各プレイヤーの名前")] TextMeshProUGUI[] m_playerNameText = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

        //選択されたステージによってステージをシーンに合成
        SceneManager.LoadScene($"Stage{ m_saveData.GetSetSelectStageNum}", LoadSceneMode.Additive);

        //デバック
        Debug.Log($"<color=yellow>生成ステージ：Stage{m_saveData.GetSetSelectStageNum}</color>");

        switch (m_saveData.GetSetSelectGameMode)
        {
            //チャレンジモード
            case "CHALLENGE":
                //名前をユーザー名にする
                m_playerNameText[0].text = PlayerPrefs.GetString("PlayerName");
                    //チャレンジモードのBGMを再生する
                    m_soundManager.PlayBGM("GameSceneBGM01");
                break;

            //ローカルプレイ
            case "LOCALMATCH":
                //プレイヤーに初期位置を設定する。
                for (int playerNum = 0; playerNum < 4; playerNum++)
                {
                    m_localPlayerPosition[playerNum].position = m_stageLocalPlayerInitPosition[m_saveData.GetSetSelectStageNum - 1][playerNum];
                }
                    //チャレンジモードのBGMを再生する
                    m_soundManager.PlayBGM("GameSceneBGM02");

                    break;

            //オンラインプレイの場合、プレイヤーを生成する。
            case "RANDOMMATCH":
            case "PRIVATEMATCH":
                    //マッチングシーンはオンライン対応のプレイヤーモデルは生成しない。
                    if(SceneManager.GetActiveScene().name == "MatchingScene")
                    {
                        break;
                    }

                PhotonNetwork.IsMessageQueueRunning = true;

                //プレイヤー生成処理
                PlayerGeneration();

                break;
        }
    }

    //プレイヤー名を表示させる関数
    [PunRPC]
    void DisplayPlayerName(int num,string playerName)
    {
        //プレイヤー名を表示
        m_playerNameText[num].text = playerName;

        //デバック
        Debug.Log($"<color=blue>参加プレイヤー:{num}</color>");
    }

        //プレイヤー生成処理
        void PlayerGeneration()
        {
            Debug.Log($"<color=blue>Photonローカルプレイヤーのアクターナンバー:{PhotonNetwork.LocalPlayer.ActorNumber}</color>");

        GameObject m_gameObjectOnline = PhotonNetwork.Instantiate(
                    m_onlinePlayerPrefab.name,
                    m_stageOnlinePlayerInitPosition[m_saveData.GetSetSelectStageNum-1][PhotonNetwork.LocalPlayer.ActorNumber-1],    //ポジション
                    Quaternion.identity,        //回転
                    0
                    );
        //プレイヤー番号を保存
        m_saveData.GetSetPlayerNum = PhotonNetwork.LocalPlayer.ActorNumber-1;
        //プレイヤー名表示
        photonView.RPC(nameof(DisplayPlayerName), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber-1, PlayerPrefs.GetString("PlayerName"));
    }
}
}