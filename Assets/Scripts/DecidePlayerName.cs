using UnityEngine;
using TMPro;

/// <summary>
/// プレイヤー名を決定する処理
/// </summary>
public class DecidePlayerName : MonoBehaviour
{
    //プレイヤー名を格納するTMPro
    [SerializeField] TextMeshProUGUI m_playerNameText = null;
    //プレイヤー名にできる最大文字数
    [SerializeField] int m_maxCharacterNum = 0;

    //セーブデータ
    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    //押されたボタンの種類によって処理を分岐
    public void SetCharacter(string character)
    {
        switch (character)
        {
            //戻るボタン
            case "BACK":
                //何も入力されていなかったら末尾を削除しない
                if (m_playerNameText.text.Length == 0)
                {
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
                    return;
                }

                //登録された名前をセーブ
                PlayerPrefs.SetString("PlayerName", m_playerNameText.text);
                PlayerPrefs.Save();

                switch (m_saveData.GetSetSelectGameMode)
                {
                    //チャレンジモード
                    case "CHALLENGE":
                        //チャレンジゲームに遷移
                        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("ChallengeGameScene");
                        break;

                        //ランダムマッチ
                    case "RANDOMMATCH":
                        //タンク選択シーンに遷移
                        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                        break;

                        //プライベート
                    case "PRIVATEMATCH":
                        //パスワード入力画面に遷移
                        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("InputPasswordScene");
                        break;
                }
                break;

            //上記以外のボタン（アルファベットA～Z）
            default:
                //上限文字以上だったら反映させない
                if(m_playerNameText.text.Length > m_maxCharacterNum-1)
                {
                    return;
                }

                m_playerNameText.text += character;
                break;
        }
    }
}
