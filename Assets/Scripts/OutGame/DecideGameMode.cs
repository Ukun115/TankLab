using UnityEngine;

/// <summary>
/// ゲームモードを決定する処理
/// </summary>
namespace nsTankLab
{
public class DecideGameMode : MonoBehaviour
{
    //シーンスイッチャースクリプト
    SceneSwitcher m_sceneSwitcher = null;

    //セーブデータ
    SaveData m_saveData = null;

    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("警告メッセージ表示処理スクリプト")] WarningTextDisplay m_warningTextDisplay = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
    }

    //押されたボタンの種類によって処理を分岐
    public void SetCharacter(string character)
    {
        var netWorkState = Application.internetReachability;

        //選択されたゲームモードを保存
        m_saveData.GetSetSelectGameMode = character;

        switch (character)
        {
            //チャレンジモードの場合、
            case "CHALLENGE":
                //チャレンジゲームシーンに遷移
                ChangeScene("ChallengeGameScene");

                break;

            //ローカルマッチモードの場合、
            case "LOCALMATCH":
                //繋がれているコントローラーの数が足りたら、
                if (m_controllerData.GetConnectedControllerNum() >= 2)
                {
                    //タンクシーンに遷移
                    m_sceneSwitcher.StartTransition("SelectTankScene");
                }
                //繋がれているコントローラーの数が足りなかったら、
                else
                {
                    Debug.Log($"<color=yellow>不足コントローラー数:{4- m_controllerData.GetConnectedControllerNum()}</color>");

                    //警告メッセージを画面表示
                    m_warningTextDisplay.Display("Not enough "+"\n"+"controllers connected."+"\n"+"Required number: 4");
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
                    ChangeScene("SelectTankScene");
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
                    //パスワード入力シーンに遷移
                    ChangeScene("InputPasswordScene");
                }

                break;

            //設定の場合、
            case "SETTING":


                break;

            //終了の場合、
            case "EXIT":

                Debug.Log("ゲーム終了");

                //ゲーム終了
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

                break;

            //プレイヤー名前の場合、
            case "PLAYERNAME":
                //ユーザー名登録シーンに遷移
                ChangeScene("DecideNameScene");
                break;
        }
    }

    //シーン遷移処理
    void ChangeScene(string nextSceneName)
    {
        //プレイヤー名を登録していなかった場合、
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            //プレイヤー名登録シーンを挟む
            m_sceneSwitcher.StartTransition("DecideNameScene");
        }
        //通常遷移
        else
        {
            //次のシーンに遷移
            m_sceneSwitcher.StartTransition(nextSceneName);
        }
    }
}
}