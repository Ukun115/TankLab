using UnityEngine;
using TMPro;

/// <summary>
/// パスワードを決定する処理
/// </summary>
public class DecidePassword : MonoBehaviour
{
    //パスワードを格納するTMPro
    [SerializeField] TextMeshProUGUI m_numberText = null;
    //パスワードにできる最大文字数
    [SerializeField] int m_maxCharacterNum = 0;

    //押されたボタンの種類によって処理を分岐
    public void SetCharacter(string character)
    {
        switch (character)
        {
            //戻るボタン
            case "BACK":
                //何も入力されていなかったら末尾を削除しない
                if(m_numberText.text.Length == 0)
                {
                    return;
                }
                //現時点で入力されているパスワードの末尾を削除する。
                m_numberText.text = m_numberText.text[..^1];
                break;

            //OKボタン
            case "OK":
                //4文字入力されていなかったらokさせない
                if (m_numberText.text.Length < 4)
                {
                    return;
                }

                //入力されたパスワードを保存
                GameObject.Find("SaveData").GetComponent<SaveData>().GetSetInputPassword = m_numberText.text;

                //タンク選択シーンに遷移
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                break;

            //上記以外のボタン(0～9)
            default:
                //上限文字以上だったら反映させない
                if(m_numberText.text.Length > m_maxCharacterNum-1)
                {
                    return;
                }

                m_numberText.text += character;
                break;
        }
    }
}
