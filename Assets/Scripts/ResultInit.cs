using UnityEngine;
using TMPro;

/// <summary>
/// リザルト画面の初期化処理
/// </summary>
namespace nsTankLab
{
    public class ResultInit : MonoBehaviour
    {
        //勝利プレイヤー
        int m_winPlayer = 0;
        //勝利テキスト
        TextMeshProUGUI m_winText = null;
        //勝利テキストカラー(1:1P赤,2:2P青,3:3P橙,4:4P緑)
        Color[] m_winTextColor = { new Color(0.0f, 0.5f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.5f, 1.0f), new Color(1.0f, 0.5f, 0.15f, 1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f) };

        SaveData m_saveData = null;

        SceneSwitcher m_sceneSwitcher = null;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            //勝利プレイヤー表示
            //チャレンジモードで敵AIが勝利した場合
            if (m_winPlayer == 5)
            {
                m_winText.text = "Game Over!!";
            }
            //チャレンジモードでチャレンジをすべてクリアした場合
            else if (m_winPlayer == 6)
            {
                m_winText.text = "Challenge Clear!!";
            }
            //いずれかのプレイヤーが勝利した場合
            else
            {
                m_winText.text = $"{m_winPlayer}P Win!!";
                //勝利プレイヤーによってカラーチェンジ
                m_winText.color = m_winTextColor[m_winPlayer - 1];
            }

            //３秒後にタイトル画面に戻る
            Invoke(nameof(BackTitleScene), 3f);
        }

        //勝利プレイヤーを設定するセッター
        public void SetWinPlayer(int winPlayer)
        {
            m_winPlayer = winPlayer;
        }

        //タイトルシーンに戻る処理
        void BackTitleScene()
        {
            m_saveData.SaveDataInit();
            m_sceneSwitcher.StartTransition(SceneName.TitleScene, true);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        }
    }
}