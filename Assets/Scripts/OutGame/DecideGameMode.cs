using UnityEngine;

/// <summary>
/// ゲームモードを決定する処理
/// </summary>
namespace nsTankLab
{
    public class DecideGameMode : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("警告メッセージ表示処理スクリプト")] WarningTextDisplay m_warningTextDisplay = null;

        //シーンスイッチャースクリプト
        SceneSwitcher m_sceneSwitcher = null;

        //セーブデータ
        SaveData m_saveData = null;

        ControllerData m_controllerData = null;

        bool m_notGood = false;

        //ローカルマッチの必要プレイ人数
        int NEED_CONTROLLER_NUM = 4;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();
        }

        //押されたボタンの種類によって処理を分岐
        public void SetCharacter(string character)
        {
            m_notGood = false;

            var netWorkState = Application.internetReachability;

            //選択されたゲームモードを保存
            m_saveData.GetSetSelectGameMode = character;

            switch (character)
            {
                //チャレンジモードの場合、
                case "CHALLENGE":
                    //現在のチャレンジ数カウントシーンに遷移
                    ChangeScene(SceneName.ChallengeNowNumCountScene);
                    break;

                //ローカルマッチモードの場合、
                case "LOCALMATCH":
                    //繋がれているコントローラーの数が足りたら、
                    if (m_controllerData.GetConnectGamepad() >= NEED_CONTROLLER_NUM)
                    {
                        //タンクシーンに遷移
                        m_sceneSwitcher.StartTransition(SceneName.SelectTankScene);
                    }
                    //繋がれているコントローラーの数が足りなかったら、
                    else
                    {
                        Debug.Log($"<color=yellow>不足コントローラー数:{NEED_CONTROLLER_NUM  - m_controllerData.GetConnectGamepad()}</color>");

                        //警告メッセージを画面表示
                        m_warningTextDisplay.Display($"Not enough \ncontrollers connected.\nRequired number: {NEED_CONTROLLER_NUM - m_controllerData.GetConnectGamepad()}");

                        m_notGood = true;
                    }

                    break;

                //ランダムマッチの場合、
                case "RANDOMMATCH":
                    //インターネットに接続していない場合
                    if (netWorkState == NetworkReachability.NotReachable)
                    {
                        //インターネット通信のため、シーン遷移しないようにする。
                        //警告メッセージを画面表示
                        Debug.Log("インターネットに接続してください。");
                        m_warningTextDisplay.Display("Please connect to"+"\n"+"the internet.");

                    }
                    //インターネットに接続されている場合
                    else
                    {
                        //タンク選択シーンに遷移
                        ChangeScene(SceneName.SelectTankScene);
                    }

                    break;

                //プライベートマッチの場合、
                case "PRIVATEMATCH":
                    //インターネットに接続していない場合
                    if (netWorkState == NetworkReachability.NotReachable)
                    {
                        //インターネット通信のため、シーン遷移しないようにする。
                        //警告メッセージを画面表示
                        Debug.Log("インターネットに接続してください。");
                        m_warningTextDisplay.Display("Please connect to " + "\n" + "the internet.");

                    }
                    //インターネットに接続されている場合
                    else
                    {
                        //パスワード決定シーンに遷移
                        ChangeScene(SceneName.DecidePasswordScene);
                    }

                    break;

                //設定の場合、
                case "CONFIG":
                    //設定シーンに遷移
                    ChangeScene(SceneName.ConfigScene);

                    break;

                //終了の場合、
                case "EXIT":
                    //ゲーム終了
                    Invoke(nameof(GameEnd), 0.5f);

                    break;

                //プレイヤー名前の場合、
                case "PLAYERNAME":
                    //ユーザー名登録シーンに遷移
                    ChangeScene(SceneName.DecideNameScene);
                    break;
            }
        }

        void GameEnd()
        {
            Debug.Log("ゲーム終了");

            //ゲーム終了
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        //シーン遷移処理
        void ChangeScene(string nextSceneName)
        {
            //プレイヤー名を登録していなかった場合、
            if (!PlayerPrefs.HasKey("PlayerName"))
            {
                //プレイヤー名登録シーンを挟む
                m_sceneSwitcher.StartTransition(SceneName.DecideNameScene, false);
            }
            //通常遷移
            else
            {
                //次のシーンに遷移
                m_sceneSwitcher.StartTransition(nextSceneName);
            }
        }

        public bool GetNoGood()
        {
            return m_notGood;
        }

        void GetComponents()
        {
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}