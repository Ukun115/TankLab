using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

/// <summary>
/// 条件を満たすとリザルト画面に行く処理
/// </summary>
namespace nsTankLab
{
public class GoResult : MonoBehaviour
{
    GameObject resultObject = null;
    SaveData m_saveData = null;
    SoundManager m_soundManager = null;
    [SerializeField, TooltipAttribute("リザルト処理が内包されているプレファブオブジェクト")] GameObject m_resultPrefab = null;

        SceneSwitcher m_sceneSwitcher = null;

        bool m_canGoResult = true;

    void Start()
    {
            //マッチングシーンの場合は勝利処理は実行しないでいいので破棄しておく
            if (SceneManager.GetActiveScene().name == "MatchingScene")
            {
                Destroy(this);
            }

            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
    }

    void Update()
    {
            if(!m_canGoResult)
            {
                return;
            }

        //チャレンジモードの時
        if (m_saveData.GetSetSelectGameMode == "CHALLENGE")
        {
            //Enemyタグを持つGameObjectを 全て 取得する。
            GameObject[] enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
            //Playerタグを持つGameObjectを取得する。
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

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
                    }
                    else
                    {
                        //リザルト突入
                        InstantiateResultObject(5);
                    }
            }
        }
        //チャレンジモード以外のモードの時
        else
        {
            //Playerタグを持つGameObjectを全て取得する。
            GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player");

            //プレイヤーがフィールド上に一人だけになったら、
            if (playerObject.Length == 1)
            {
                //リザルト突入
                int winPlayerNum = int.Parse(Regex.Replace(playerObject[0].name, @"[^1-4]", ""));
                InstantiateResultObject(winPlayerNum);
                Debug.Log("勝敗がつきました。");
            }
        }
    }

        void ChangeChallengeNowNumCountScene()
        {
            //現在のチャレンジ数カウントシーンに遷移
            m_sceneSwitcher.StartTransition("ChallengeNowNumCountScene");
        }

        void ChangeChallengeNowNumCountSceneAndStageNum()
        {
            //現在のチャレンジ数カウントシーンに遷移
            m_sceneSwitcher.StartTransition("ChallengeNowNumCountScene");
                //次のステージ番号に進める
                m_saveData.NextStageNum();
        }

        void InstantiateResultObject(int winPlayer)
    {
        //リザルトに突入
        //リザルト処理は毎シーンごとに１度のみしか実行しない
        resultObject = GameObject.Find("Result");
            //リザルト処理をまとめているゲームオブジェクトを生成し、
            //リザルト処理を実行していく。
            resultObject = Instantiate(m_resultPrefab);
            resultObject.name = "Result";
            //勝利表示
            resultObject.GetComponent<ResultInit>().SetWinPlayer(winPlayer);

            StopGame();

            Destroy(this);
    }

    void StopGame()
        {
            //勝利SE再生
            m_soundManager.PlaySE("WinSE");
            //BGM止める
            m_soundManager.StopBGM();

            //タンクや弾の動きを止める
            m_saveData.GetSetmActiveGameTime = false;

            m_canGoResult = false;
        }
}
}