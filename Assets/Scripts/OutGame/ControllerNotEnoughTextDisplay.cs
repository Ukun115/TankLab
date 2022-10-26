using UnityEngine;
using TMPro;

/// <summary>
/// 接続されているコントローラー数が不足しているときの警告メッセージ表示処理
/// </summary>
public class ControllerNotEnoughTextDisplay : MonoBehaviour
{
    [SerializeField, TooltipAttribute("接続コントローラー数不足警告メッセージテキスト")] TextMeshProUGUI m_controllerNotEnoughText = null;

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
                m_controllerNotEnoughText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);
                if(alphaValue < 0.0f)
                {
                    //待機状態にする
                    m_enState = EnState.enWait;
                }
                break;
        }
    }

    public void Display()
    {
        //α値を初期化する
        alphaValue = 1.0f;

        //タイマーをリセット
        m_displayTimer = 0;

        //α値を更新
        m_controllerNotEnoughText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);

        m_enState = EnState.enDisplay;
    }
}
