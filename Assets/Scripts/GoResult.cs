using UnityEngine;
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
    [SerializeField, TooltipAttribute("リザルト処理が内包されているプレファブオブジェクト")] GameObject m_resultPrefab = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    void Update()
    {
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
                //リザルト突入
                InstantiateResultObject(1);
                Debug.Log("敵が全機死亡しました。");
                Destroy(this);
            }
            //全機死んでいないとき、
            //(つまりプレイヤーが死んでいるとき、)
            else if (playerObject is null)
            {
                //リザルト突入
                InstantiateResultObject(2);
                Debug.Log("プレイヤーが死亡しました。");
                Destroy(this);
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
                Destroy(this);
            }
        }
    }

    void InstantiateResultObject(int winPlayer)
    {
        //リザルトに突入
        //リザルト処理は毎シーンごとに１度のみしか実行しない
        resultObject = GameObject.Find("Result");
        if (resultObject is null)
        {
            //リザルト処理をまとめているゲームオブジェクトを生成し、
            //リザルト処理を実行していく。
            resultObject = Instantiate(m_resultPrefab);
            resultObject.name = "Result";
            //勝利表示
            resultObject.GetComponent<ResultInit>().SetWinPlayer(winPlayer);
        }
    }
}
}