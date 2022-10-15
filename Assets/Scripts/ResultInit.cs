using UnityEngine;
using TMPro;

/// <summary>
/// リザルト画面の初期化処理
/// </summary>
public class ResultInit : MonoBehaviour
{
    //勝利プレイヤー
    int m_winPlayer = 0;
    //勝利テキスト
    TextMeshProUGUI m_winText = null;
    //勝利テキストカラー(1:1P赤,2:2P青)
    Color[] m_winTextColor = { new Color(0.0f, 0.5f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.5f, 1.0f) };

    void Start()
    {
        //勝利プレイヤー表示
        m_winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        m_winText.text = m_winPlayer + "P Win!!";
        //勝利プレイヤーによってカラーチェンジ
        m_winText.color = m_winTextColor[m_winPlayer-1];
    }

    //勝利プレイヤーを設定するセッター
    public void SetWinPlayer(int winPlayer)
    {
        m_winPlayer = winPlayer;
    }
}
