using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using Photon.Pun;

/// <summary>
/// 条件を満たすとリザルト画面に行く処理
/// </summary>
namespace nsTankLab
{
    public class GoResult : MonoBehaviourPun
    {
        [SerializeField]ResultInit m_resultInitScript = null;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        SceneSwitcher m_sceneSwitcher = null;

        bool m_canGoResult = true;

        string m_winPlayerName = string.Empty;

        void Start()
        {
            //マッチングシーンの場合は勝利処理は実行しないでいいので破棄しておく
            if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
            {
                Destroy(this);
            }

            //コンポーネント取得まとめ
            GetComponents();
        }

        void Update()
        {
            if(!m_canGoResult)
            {
                return;
            }

           //タンクや弾の動きが止まっているとき
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //チャレンジモードの時
            if (m_saveData.GetSetSelectGameMode == "CHALLENGE")
            {
                //Enemyタグを持つGameObjectを 全て 取得する。
                GameObject[] enemyObject = GameObject.FindGameObjectsWithTag(TagName.Enemy);
                //Playerタグを持つGameObjectを取得する。
                GameObject playerObject = GameObject.FindGameObjectWithTag(TagName.Player);

                //敵AIが全機死んでいたら、
                if (enemyObject.Length <= 0)
                {
                    Debug.Log("敵が全機死亡しました。");
                    //現在のステージが最後のステージの場合
                    if(m_saveData.GetSetSelectStageNum.Equals(m_saveData.GetTotalStageNum()))
                    {
                        //リザルト突入
                        InstantiateResultObject(6);
                    }
                    else
                    {
                        StopGame();
                        Invoke(nameof(ChangeChallengeNowNumCountSceneAndStageNum), 3.0f);
                    }

                    //勝利SE再生
                    m_soundManager.PlayBGM("WinBGM",false);
                }
                //全機死んでいないとき、
                //(つまりプレイヤーが死んでいるとき、)
                else if (playerObject is null)
                {
                    Debug.Log("プレイヤーが死亡しました。");

                    //体力がまだ残っている場合はやり直し
                    if (m_saveData.GetSetHitPoint != 0)
                    {
                        StopGame();
                        Invoke(nameof(ChangeChallengeNowNumCountScene),3.0f);
                        //敗北SE再生
                        m_soundManager.PlayBGM("LoseBGM",false);
                    }
                    else
                    {
                        //敗北SE再生
                        m_soundManager.PlayBGM("Lose2BGM",false);
                        //リザルト突入
                        InstantiateResultObject(5);
                    }
                }
            }
            //チャレンジモード以外のモードの時
            else
            {
                //Playerタグを持つGameObjectを全て取得する。
                GameObject[] playerObject = GameObject.FindGameObjectsWithTag(TagName.Player);

                //プレイヤーがフィールド上に一人だけになったら、
                if (playerObject.Length == 1)
                {
                    m_winPlayerName = playerObject[0].name;

                    switch (SceneManager.GetActiveScene().name)
                    {
                        case SceneName.LocalGameScene:
                            GoResultScene(m_winPlayerName);
                            break;
                        case SceneName.OnlineGameScene:
                            photonView.RPC(nameof(GoResultScene), RpcTarget.All, m_winPlayerName);
                            break;
                    }
                }
            }
        }

        [PunRPC]
        void GoResultScene(string winPlayerName)
        {
            //リザルト突入
            int winPlayerNum = int.Parse(Regex.Replace(winPlayerName, @"[^1-4]", string.Empty));
            InstantiateResultObject(winPlayerNum);
            Debug.Log("勝敗がつきました。");
        }

        void ChangeChallengeNowNumCountScene()
        {
            //現在のチャレンジ数カウントシーンに遷移
            m_sceneSwitcher.StartTransition(SceneName.ChallengeNowNumCountScene);
        }

        void ChangeChallengeNowNumCountSceneAndStageNum()
        {
            //現在のチャレンジ数カウントシーンに遷移
            m_sceneSwitcher.StartTransition(SceneName.ChallengeNowNumCountScene);
            //次のステージ番号に進める
            m_saveData.NextStageNum();
        }

        void InstantiateResultObject(int winPlayer)
        {
            //リザルト初期化処理開始
            m_resultInitScript.enabled = true;
            m_resultInitScript.SetWinPlayer(winPlayer);

            StopGame();

            Destroy(this);
        }

        void StopGame()
        {
            //タンクや弾の動きを止める
            m_saveData.GetSetmActiveGameTime = false;

            m_canGoResult = false;
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        }
    }
}