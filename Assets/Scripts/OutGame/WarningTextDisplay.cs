using UnityEngine;
using TMPro;

/// <summary>
/// 警告メッセージ表示処理
/// </summary>
public class WarningTextDisplay : MonoBehaviour
{
    [SerializeField, TooltipAttribute("警告メッセージテキスト")] TextMeshProUGUI m_warningText = null;

    //α値
    float alphaValue = 0.0f;

    int m_displayTimer = 0;

    enum EnState
    {
        enWait,
        enDisplay,
        enFade,
    }
    EnState m_enState = EnState.enWait;

    void Update()
    {
        switch(m_enState)
        {
            case EnState.enWait:
                //待機状態。なにもしない。
                break;
            case EnState.enDisplay:
                //一定時間表示する
                m_displayTimer++;
                if(m_displayTimer > 180)
                {
                    //フェード状態にする
                    m_enState = EnState.enFade;

                    //タイマーをリセット
                    m_displayTimer = 0;
                }
                break;
            case EnState.enFade:
                //フェードしていく
                alphaValue -= 0.05f;
                m_warningText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);
                if(alphaValue < 0.0f)
                {
                    //待機状態にする
                    m_enState = EnState.enWait;
                }
                break;
        }
    }

    public void Display(string text)
    {
        m_warningText.text = text;

        //α値を初期化する
        alphaValue = 1.0f;

        //タイマーをリセット
        m_displayTimer = 0;

        //α値を更新
        m_warningText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);

        m_enState = EnState.enDisplay;
    }
}
