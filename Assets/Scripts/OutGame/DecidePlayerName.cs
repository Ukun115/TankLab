using UnityEngine;
using TMPro;

/// <summary>
/// プレイヤー名を決定する処理
/// </summary>
namespace nsTankLab
{
public class DecidePlayerName : MonoBehaviour
{
    [SerializeField, TooltipAttribute("プレイヤー名TMPro")] TextMeshProUGUI m_playerNameText = null;
    //プレイヤー名にできる最大文字数
    const int MAX_CHARACTER_NUM = 8;

    //セーブデータ
    SaveData m_saveData = null;

        bool m_notGood = false;

        void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    //押されたボタンの種類によって処理を分岐
    public void SetCharacter(string character)
    {
            m_notGood = false;

            switch (character)
        {
            //戻るボタン
            case "BACK":
                //何も入力されていなかったら末尾を削除しない
                if (m_playerNameText.text.Length == 0)
                {
                        m_notGood = true;

                        return;
                }
                //名前の末尾を削除する。
                m_playerNameText.text = m_playerNameText.text[..^1];
                break;

            //OKボタン
            case "OK":
                //何も入力されていなかったらokさせない
                if (m_playerNameText.text.Length == 0)
                {
                        m_notGood = true;

                        return;
                }

                //登録された名前をセーブ
                PlayerPrefs.SetString("PlayerName", m_playerNameText.text);
                PlayerPrefs.Save();

                switch (m_saveData.GetSetSelectGameMode)
                {
                    //チャレンジモード
                    case "CHALLENGE":
                        //現在のチャレンジ数カウントシーンに遷移
                        ChangeScene("ChallengeNowNumCountScene");
                        break;

                        //ランダムマッチ
                    case "RANDOMMATCH":
                        //タンク選択シーンに遷移
                        ChangeScene("SelectTankScene");
                        break;

                        //プライベート
                    case "PRIVATEMATCH":
                        //パスワード入力画面に遷移
                        ChangeScene("InputPasswordScene");
                        break;

                        //プレイヤーネーム決め
                    case "PLAYERNAME":
                        //タイトル画面に遷移
                        ChangeScene("TitleScene");
                        break;

                            //コンフィグ
                        case "CONFIG":
                            //コンフィグ画面に遷移
                            ChangeScene("ConfigScene");
                            break;
                }
                break;

            //上記以外のボタン（アルファベットA～Z）
            default:
                //上限文字以上だったら反映させない
                if(m_playerNameText.text.Length > MAX_CHARACTER_NUM-1)
                {
                        m_notGood = true;

                        return;
                }

                m_playerNameText.text += character;
                break;
        }
    }

    //シーン遷移する処理
    void ChangeScene(string nextScene)
    {
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(nextScene);
    }

        public bool GetNoGood()
        {
            return m_notGood;
        }
    }
}