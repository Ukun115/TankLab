using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

        //星画像
        [SerializeField] Sprite[] m_starSprite = null;

        [SerializeField] List<StarList> m_starList = new List<StarList>();

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            //勝利プレイヤー表示
            //チャレンジモードで敵AIが勝利した場合
            if (m_winPlayer == 5)
            {
                m_winText.text = "Game Over!!";

                //３秒後にタイトル画面に戻る
                Invoke(nameof(BackTitleScene), 3.0f);
            }
            //チャレンジモードでチャレンジをすべてクリアした場合
            else if (m_winPlayer == 6)
            {
                m_winText.text = "Challenge Clear!!";

                //３秒後にタイトル画面に戻る
                Invoke(nameof(BackTitleScene), 3.0f);
            }
            //いずれかのプレイヤーが勝利した場合
            else
            {
                m_winText.text = $"{m_winPlayer}P Win!!";
                //勝利プレイヤーによってカラーチェンジ
                m_winText.color = m_winTextColor[m_winPlayer - 1];

                //スター画像を更新
                UpdateStarImage();

                //スターを２つ取ったら、
                if (m_saveData.GetStar(m_winPlayer - 1, 1))
                {
                    //３秒後にタイトル画面に戻る
                    Invoke(nameof(BackTitleScene), 3.0f);
                }
                else
                {
                    //3秒後に再度ローカルゲームシーンに遷移する。
                    Invoke(nameof(BackLocalGameScene), 3.0f);
                }
            }
        }

        //スター画像を更新する処理
        void UpdateStarImage()
        {
            for (int starNum = 0; starNum < 2; starNum++)
            {
                //スターを取得していなかったら、
                if (!m_saveData.GetStar(m_winPlayer - 1, starNum))
                {
                    //スター取得済みにする
                    m_saveData.ActiveStar(m_winPlayer - 1, starNum);
                    //スター画像をつける
                    m_starList[m_winPlayer - 1].GetStarUiList(starNum).sprite = m_starSprite[m_winPlayer-1];

                    return;
                }
            }
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
            m_sceneSwitcher.StartTransition(SceneName.TitleScene);
        }

        //再度ローカルゲームシーンに戻る処理
        void BackLocalGameScene()
        {
            m_sceneSwitcher.StartTransition(SceneName.LocalGameScene);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        }
    }

    [System.Serializable]
    public class StarList
    {
        [SerializeField] List<Image> m_starUI = new List<Image>();

        public Image GetStarUiList(int starNum)
        {
            return m_starUI[starNum];
        }
    }
}