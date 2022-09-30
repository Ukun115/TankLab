using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DecidePlayerName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_playerNameText = null;
    [SerializeField] int m_maxCharacterNum = 0;

    public void SetNameText(string character)
    {
        switch (character)
        {
            case "BACK":
                //何も入力されていなかったら末尾を削除しない
                if(m_playerNameText.text.Length == 0)
                {
                    return;
                }
                //名前の末尾を削除する。
                m_playerNameText.text = m_playerNameText.text[..^1];
                break;

            case "OK":
                //何も入力されていなかったらokさせない
                if (m_playerNameText.text.Length == 0)
                {
                    return;
                }
                //登録された名前をセーブ
                PlayerPrefs.SetString("PlayerName", m_playerNameText.text);
                PlayerPrefs.Save();
                //タンク選択シーンに遷移
                SceneManager.LoadScene("SelectTankScene");
                break;

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
