using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DecidePassword : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_numberText = null;
    [SerializeField] int m_maxCharacterNum = 0;

    public void SetCharacter(string character)
    {
        switch (character)
        {
            case "BACK":
                //何も入力されていなかったら末尾を削除しない
                if(m_numberText.text.Length == 0)
                {
                    return;
                }
                //名前の末尾を削除する。
                m_numberText.text = m_numberText.text[..^1];
                break;

            case "OK":
                //何も入力されていなかったらokさせない
                if (m_numberText.text.Length == 0)
                {
                    return;
                }
                //タンク選択シーンに遷移
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                break;

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
