using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// ゲームシーンの初期化処理
/// </summary>
namespace nsTankLab
{
    public class GameSceneInit : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("ローカル時の各プレイヤーの位置")] Transform[] m_localPlayerPosition = null;
        [SerializeField, TooltipAttribute("オンライン時のプレイヤーオブジェクト")] GameObject m_onlinePlayerPrefab = null;
        [SerializeField, TooltipAttribute("ローカルマッチ時のプレイヤーオブジェクト")] GameObject[] m_localPlayPlayerObject = null;
        [SerializeField, TooltipAttribute("ローカルマッチ時のプレイヤーボードオブジェクト")] GameObject[] m_localPlayPlayerBoardObject = null;
        [SerializeField, TooltipAttribute("ローカルマッチ時のプレイヤーカーソルオブジェクト")] GameObject[] m_localPlayPlayerCursorObject = null;
        [SerializeField, TooltipAttribute("各プレイヤーの名前")] TextMeshProUGUI[] m_playerNameText = null;
        //セーブデータ
        SaveData m_saveData = null;

        [SerializeField]SkillCool[] m_skillCoolScript = { null };

        //ステージごとのプレイヤーの初期位置(3,4人用の初期位置)
        Vector3[][] m_playerInitPosition3and4Play =
        {
            //ステージ1
            new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)},
            //ステージ2
            new[] { new Vector3( -3.0f, 0.0f, 3.0f ),new Vector3(3.0f,0.0f,3.0f),new Vector3(-3.0f, 0.0f, -3.0f), new Vector3(3.0f, 0.0f, -3.0f)},
            //ステージ3
            new[] { new Vector3( -4.0f, 0.0f, 6.5f ),new Vector3(7.0f,0.0f,2.0f),new Vector3(-7.0f, 0.0f, -2.0f), new Vector3(4.0f, 0.0f, -6.5f)},
            //ステージ4
            new[] { new Vector3( -11.0f, 0.0f, 6.0f ),new Vector3(11.0f,0.0f,6.0f),new Vector3(-11.0f, 0.0f, -6.0f), new Vector3(11.0f, 0.0f, -6.0f)},
            //ステージ5
            new[] { new Vector3( -2.0f, 0.0f, 4.0f ),new Vector3(9.0f,0.0f,1.5f),new Vector3(-9.0f, 0.0f, -1.5f), new Vector3(2.0f, 0.0f, -4.0f)},
            //ステージ6
            new[] { new Vector3( -5.0f, 0.0f, 6.0f ),new Vector3(5.0f,0.0f,6.0f),new Vector3(-5.0f, 0.0f, -6.0f), new Vector3(5.0f, 0.0f, -6.0f)},
            //ステージ7
            new[] { new Vector3( -9.5f, 0.0f, 4.0f ),new Vector3(9.5f,0.0f,4.0f),new Vector3(-9.5f, 0.0f, -4.0f), new Vector3(9.5f, 0.0f, -4.0f)},
            //ステージ8
            new[] { new Vector3( -9.5f, 0.0f, 4.0f ),new Vector3(9.5f,0.0f,4.0f),new Vector3(-9.5f, 0.0f, -4.0f), new Vector3(9.5f, 0.0f, -4.0f)},
            //ステージ9
            new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)},
            //ステージ10
            new[] { new Vector3( -8.0f, 0.0f, 4.0f ),new Vector3(8.0f,0.0f,4.0f),new Vector3(-8.0f, 0.0f, -4.0f), new Vector3(8.0f, 0.0f, -4.0f)}
        };

        //ステージごとのプレイヤーの初期位置(２人用の初期位置)
        Vector3[][] m_playerInitPosition2Play =
        {
            //ステージ1
            new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
            //ステージ2
            new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
            //ステージ3
            new[] {new Vector3(-7.0f,0.0f,-2.0f ),new Vector3(7.0f,0.0f,2.0f) },
            //ステージ4
            new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
            //ステージ5
            new[] {new Vector3(-10.0f,0.0f,5.0f ),new Vector3(10.0f,0.0f,-5.0f) },
            //ステージ6
            new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
            //ステージ7
            new[] {new Vector3(-9.5f,0.0f,-4.0f ),new Vector3(9.5f,0.0f,4.0f) },
            //ステージ8
            new[] {new Vector3(-9.5f,0.0f,-4.0f ),new Vector3(9.5f,0.0f,4.0f) },
            //ステージ9
            new[] {new Vector3(-10.0f,0.0f,-5.0f ),new Vector3(10.0f,0.0f,5.0f) },
            //ステージ10
            new[] {new Vector3(-8.0f,0.0f,-4.0f ),new Vector3(8.0f,0.0f,4.0f) }
        };

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            switch (m_saveData.GetSetSelectGameMode)
            {
                //チャレンジモード
                case "CHALLENGE":
                    //名前をユーザー名にする
                    m_playerNameText[0].text = PlayerPrefs.GetString("PlayerName");

                    m_localPlayerPosition[0].position = m_playerInitPosition2Play[m_saveData.GetSetSelectStageNum - 1][0];

                    break;

                //ローカルプレイ
                case "LOCALMATCH":

                    //プレイヤーに初期位置を設定する。
                    for (int playerNum = 0; playerNum < m_saveData.GetSetLocalMatchPlayNum; playerNum++)
                    {
                        //プレイヤーオブジェクトをオン
                        m_localPlayPlayerObject[playerNum].SetActive(true);
                        m_localPlayPlayerBoardObject[playerNum].SetActive(true);
                        m_localPlayPlayerCursorObject[playerNum].SetActive(true);
                    }

                    switch (m_saveData.GetSetLocalMatchPlayNum)
                    {
                        //2人
                        case 2:
                            //プレイヤーに初期位置を設定する。
                            for (int playerNum = 0; playerNum < m_saveData.GetSetLocalMatchPlayNum; playerNum++)
                            {
                                m_localPlayerPosition[playerNum].position = m_playerInitPosition2Play[m_saveData.GetSetSelectStageNum - 1][playerNum];
                            }
                            break;
                        //3人
                        case 3:
                        case 4:
                            //プレイヤーに初期位置を設定する。
                            for (int playerNum = 0; playerNum < m_saveData.GetSetLocalMatchPlayNum; playerNum++)
                            {
                                m_localPlayerPosition[playerNum].position = m_playerInitPosition3and4Play[m_saveData.GetSetSelectStageNum - 1][playerNum];
                            }
                            break;
                    }

                    break;

                //オンラインプレイの場合、プレイヤーを生成する。
                case "RANDOMMATCH":
                case "PRIVATEMATCH":
                    //マッチングシーンはオンライン対応のプレイヤーモデルは生成しない。
                    if(SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                    {
                        m_localPlayerPosition[0].position = m_playerInitPosition2Play[m_saveData.GetSetSelectStageNum - 1][0];

                        break;
                    }

                    PhotonNetwork.IsMessageQueueRunning = true;

                    //プレイヤー生成処理
                    PlayerGeneration();

                    break;
            }

            //選択されたステージによってステージをシーンに合成
            SceneManager.LoadScene($"Stage{ m_saveData.GetSetSelectStageNum}", LoadSceneMode.Additive);
            //デバック
            Debug.Log($"<color=yellow>生成ステージ：Stage{m_saveData.GetSetSelectStageNum}</color>");
        }

        //プレイヤー名を表示させる関数
        [PunRPC]
        void DisplayPlayerName(int num,string playerName)
        {
            //プレイヤー名を表示
            m_playerNameText[num].text = playerName;

            //デバック
            Debug.Log($"<color=blue>参加プレイヤー:{num+1}P</color>");
        }

        //プレイヤー生成処理
        void PlayerGeneration()
        {
            Debug.Log($"<color=blue>Photonローカルプレイヤーのアクターナンバー:{PhotonNetwork.LocalPlayer.ActorNumber}</color>");

            GameObject gameObjectOnline = PhotonNetwork.Instantiate(
                m_onlinePlayerPrefab.name,
                m_playerInitPosition2Play[m_saveData.GetSetSelectStageNum-1][PhotonNetwork.LocalPlayer.ActorNumber-1],    //ポジション
                Quaternion.identity,        //回転
                0
             );

            //プレイヤー番号を保存
            m_saveData.GetSetPlayerNum = PhotonNetwork.LocalPlayer.ActorNumber-1;

            //1Pになった場合
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                //選択されているスキルを相手に渡す
                photonView.RPC(nameof(SkillTankInit), RpcTarget.All, 1, m_saveData.GetSelectSkillName(0), m_saveData.GetSelectTankName(0));
            }
            //2P になった場合
            else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                //選択されているスキルを相手に渡す
                photonView.RPC(nameof(SkillTankInit), RpcTarget.All, 2, m_saveData.GetSelectSkillName(1), m_saveData.GetSelectTankName(1));
            }

            //プレイヤー名表示
            photonView.RPC(nameof(DisplayPlayerName), RpcTarget.All, m_saveData.GetSetPlayerNum, PlayerPrefs.GetString("PlayerName"));

            gameObjectOnline.GetComponent<SkillInit>().SetSkillCoolScript(m_skillCoolScript[m_saveData.GetSetPlayerNum]);
        }

        [PunRPC]
        void SkillTankInit(int playerNum,string skillName,string tankName)
        {
            //選択されているスキルを相手に渡す
            m_saveData.SetSelectSkillName(playerNum, skillName);
            //選択されているタンクを相手に渡す
            m_saveData.SetSelectTankName(playerNum, tankName);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }
    }
}