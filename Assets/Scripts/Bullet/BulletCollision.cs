using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 弾が何かにぶつかったときに起こる処理
/// </summary>
public class BulletCollision : MonoBehaviour
{
    //現在の弾の反射回数
    int m_refrectionCount = 0;

    [SerializeField, TooltipAttribute("死亡マーカープレファブオブジェクト")] GameObject m_deathMarkPrefab = null;

    [SerializeField, TooltipAttribute("リザルト処理が内包されているプレファブオブジェクト")] GameObject m_resultPrefab = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    //発射したプレイヤー番号
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    GameObject resultObject = null;

    //自身を発射したタンクのオブジェクトデータ
    GameObject m_tankObject = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //敵AIの弾じゃないときは実行
        if (this.gameObject.name != "EnemyBullet")
        {
            //発射したプレイヤー番号を取得
            m_myPlayerNum = int.Parse(Regex.Replace(this.transform.name, @"[^1-4]", "")) - 1;
        }
    }

    //衝突処理
    void OnCollisionEnter(Collision collision)
    {
        //壁に衝突した場合
        if (collision.gameObject.CompareTag("Wall"))
        {
            //壁に衝突したときの処理
            OnCollisitonWall();
        }

        //プレイヤーor敵AIに衝突した場合
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Enemy"))
        {
            //プレイヤーor敵AIに衝突したときの処理
            OnCollisitonPlayerOrEnemyAI(collision);
        }

        //弾に衝突した場合
        if(collision.gameObject.CompareTag("Bullet"))
        {
            //両方消滅させる
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    //壁に衝突したときの処理
    void OnCollisitonWall()
    {
        m_refrectionCount++;

        //指定されている反射回数分反射したら、
        if (m_refrectionCount > m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletRefrectionNum())
        {
            //弾を消滅させる
            Destroy(this.gameObject, 0.05f);
        }
    }

    //プレイヤーに衝突したときの処理
    void OnCollisitonPlayerOrEnemyAI(Collision collision)
    {
        //死んだ場所に×死亡マークオブジェクトを生成する。
        Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                collision.gameObject.transform.position.x,
                -0.4f,
                collision.gameObject.transform.position.z
                ),
            collision.gameObject.transform.rotation
            );

        //弾を消滅させる
        Destroy(this.gameObject);

        //衝突したプレイヤーを消滅させる
        Destroy(collision.gameObject, 0.05f);

        //チャレンジモードの時
        if (m_saveData.GetSetSelectGameMode == "CHALLENGE")
        {
            //Enemyタグを持つGameObjectを 全て 取得する。
            GameObject[] enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
            //Playerタグを持つGameObjectを取得する。
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            //敵AIが全機死んでいたら、
            if (enemyObject.Length == 0)
            {
                //リザルトに突入
                //リザルト処理は毎シーンごとに１度のみしか実行しない
                if (resultObject is null)
                {
                    //リザルト処理をまとめているゲームオブジェクトを生成し、
                    //リザルト処理を実行していく。
                    resultObject = Instantiate(m_resultPrefab);
                    //1P勝利表示
                    resultObject.GetComponent<ResultInit>().SetWinPlayer(1);
                }
            }
            //全機死んでいないとき、
            //(つまりプレイヤーが死んでいるとき、)
            else if(playerObject is null)
            {
                //リザルト処理をまとめているゲームオブジェクトを生成し、
                //リザルト処理を実行していく。
                resultObject = Instantiate(m_resultPrefab);
                //チャレンジ終了表示
                resultObject.GetComponent<ResultInit>().SetWinPlayer(2);
            }
        }
        //チャレンジモード以外のモードの時
        else
        {
            //Playerタグを持つGameObjectを全て取得する。
            GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player");

            //プレイヤーがフィールド上に一人だけになったら、
            if(playerObject.Length == 1)
            {
                //リザルト処理をまとめているゲームオブジェクトを生成し、
                //リザルト処理を実行していく。
                resultObject = Instantiate(m_resultPrefab);
                //?P勝利表示
                int winPlayerNum = int.Parse(Regex.Replace(playerObject[0].name, @"[^1-4]", ""));
                resultObject.GetComponent<ResultInit>().SetWinPlayer(winPlayerNum);
            }
        }
    }

    public void SetFireTankObject(GameObject tankObject)
    {
        m_tankObject = tankObject;
    }

    //弾が削除されたときに呼ばれる
    void OnDestroy()
    {
        //敵AIの弾
        if (this.gameObject.name == "EnemyBullet")
        {
            if (m_tankObject is not null)
            {
                //フィールド上に生成されている弾の数データを減らす
                m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
            }
        }
        //プレイヤーの弾
        else
        {
            if (m_tankObject is not null)
            {
                //フィールド上に生成されている弾の数データを減らす
                m_tankObject.gameObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
            }
        }
    }
}