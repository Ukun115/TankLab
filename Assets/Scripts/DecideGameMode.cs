using UnityEngine;

/// <summary>
/// ゲームモードを決定する処理
/// </summary>
public class DecideGameMode : MonoBehaviour
{
    //シーンスイッチャースクリプト
    SceneSwitcher m_sceneSwitcher = null;

    //セーブデータ
    SaveData m_saveData = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    //押されたボタンの種類によって処理を分岐
    public void SetCharacter(string character)
    {
        m_saveData.GetSetSelectGameMode = character;

        switch (character)
        {
            //チャレンジモードの場合、
            case "CHALLENGE":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //通常遷移
                else
                {
                    //チャレンジゲームに遷移
                    m_sceneSwitcher.StartTransition("ChallengeGameScene");
                }

                break;

            //ローカルマッチモードの場合、
            case "LOCALMATCH":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //通常遷移
                else
                {
                    //ゲームパッドを登録するシーンに遷移
                    m_sceneSwitcher.StartTransition("GamePadRegister");
                }

                break;

            //ランダムマッチの場合、
            case "RANDOMMATCH":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //通常遷移
                else
                {
                    //タンクシーンに遷移
                    m_sceneSwitcher.StartTransition("SelectTankScene");
                }

                break;

            //プライベートマッチの場合、
            case "PRIVATEMATCH":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //通常遷移
                else
                {
                    //パスワード入力画面に遷移
                    m_sceneSwitcher.StartTransition("InputPasswordScene");
                }

                break;

            //設定の場合、
            case "SETTING":


                break;

            //終了の場合、
            case "EXIT":
                //ゲーム終了
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

                break;
        }
    }
}
