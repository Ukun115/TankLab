using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//リザルト画面の初期化処理
public class ResultInit : MonoBehaviour
{
    int m_winPlayer = 0;
    TextMeshProUGUI m_winText = null;

    void Start()
    {
        //勝利プレイヤー表示
        m_winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        m_winText.text = m_winPlayer + "P Win!!";
        //勝利プレイヤーによってカラーチェンジ
        switch(m_winPlayer)
        {
            //1Pの勝利時
            case 0:
                m_winText.color = new Color(1.0f,0.0f,0.5f,1.0f);
                break;
                //2Pの勝利時
            case 1:
                m_winText.color = new Color(0.0f,0.5f,1.0f,1.0f);
                break;
        }
    }

    //勝利プレイヤーを設定するセッター
    public void SetWinPlayer(int winPlayer)
    {
        m_winPlayer = winPlayer;
    }
}
