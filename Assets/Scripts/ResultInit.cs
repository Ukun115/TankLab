using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
        [SerializeField]TextMeshProUGUI m_winText = null;
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

                //6秒後にタイトル画面に戻る
                Invoke(nameof(BackTitleScene), 6.0f);
            }
            //チャレンジモードでチャレンジをすべてクリアした場合
            else if (m_winPlayer == 6)
            {
                m_winText.text = "Clear All Challenge!!";

                //チャレンジクリアしたことを保存する
                PlayerPrefs.SetInt("ChallengeClear",1);
                PlayerPrefs.Save();

                //5秒後にタイトル画面に戻る
                Invoke(nameof(BackTitleScene), 5.0f);
            }
            //いずれかのプレイヤーが勝利した場合(ローカル)
            else if(SceneManager.GetActiveScene().name == SceneName.LocalGameScene || SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
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
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case SceneName.LocalGameScene:
                            //3秒後に再度ローカルゲームシーンに遷移する。
                            Invoke(nameof(BackLocalGameScene), 3.0f);
                            break;
                        case SceneName.OnlineGameScene:
                            //3秒後に再度オンラインゲームシーンに遷移する。
                            Invoke(nameof(BackOnlineGameScene), 3.0f);
                            break;
                    }
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

        //再度オンラインゲームシーンに戻る処理
        void BackOnlineGameScene()
        {
            m_sceneSwitcher.StartTransition(SceneName.OnlineGameScene);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
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