using UnityEngine;
using TMPro;

/// <summary>
/// 警告メッセージ表示処理
/// </summary>
namespace nsTankLab
{
    public class WarningTextDisplay : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("警告メッセージテキスト")] TextMeshProUGUI m_warningText = null;

        //α値
        float alphaValue = 0.0f;

        int m_displayTimer = 0;

        const float FADE_VALUE = 0.05f;
        const float ALPHA_ZERO = 0.0f;

        enum EnState
        {
            enWait,     //待機
            enDisplay,  //表示
            enFade,     //フェード
        }
        EnState m_enState = EnState.enWait;

        void Update()
        {
            switch(m_enState)
            {
                //待機状態。なにもしない。
                case EnState.enWait:
                    break;

                //一定時間表示
                case EnState.enDisplay:
                    m_displayTimer++;
                    if(m_displayTimer > 180)
                    {
                        //フェード状態にする
                        m_enState = EnState.enFade;

                        //タイマーをリセット
                        m_displayTimer = 0;
                    }
                    break;

                //フェード
                case EnState.enFade:
                    alphaValue -= FADE_VALUE;
                    m_warningText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);
                    if(alphaValue < ALPHA_ZERO)
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
}