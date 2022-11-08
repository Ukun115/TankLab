using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 弾が何かにぶつかったときに起こる処理
/// </summary>
namespace nsTankLab
{
public class BulletCollision : MonoBehaviour
{
    //現在の弾の反射回数
    int m_refrectionCount = 0;

    [SerializeField, TooltipAttribute("死亡マーカープレファブオブジェクト")] GameObject m_deathMarkPrefab = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    //発射したプレイヤー番号
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    //自身を発射したタンクのオブジェクトデータ
    GameObject m_tankObject = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //敵AIの弾じゃないときは実行
        if (gameObject.name != "EnemyBullet")
        {
            //発射したプレイヤー番号を取得
            m_myPlayerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", "")) - 1;
        }
    }

    //衝突処理
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //壁に衝突したときの処理
            case "Wall":
                OnCollisitonWall();
                break;
            //プレイヤーor敵AIに衝突したときの処理
            case "Player":
            case "Enemy":
                OnCollisitonPlayerOrEnemyAI(collision);
                break;
            //弾に衝突したときの処理
            case "Bullet":
                //両方消滅させる
                Destroy(gameObject);
                Destroy(collision.gameObject);
                break;
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
            Destroy(gameObject);
        }
    }

    //プレイヤーに衝突したときの処理
    void OnCollisitonPlayerOrEnemyAI(Collision collision)
    {
        //死んだ場所に×死亡マークオブジェクトを生成する。
        GameObject deathMark = Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                collision.gameObject.transform.position.x,
                -0.4f,
                collision.gameObject.transform.position.z
                ),
            collision.gameObject.transform.rotation
            );
        deathMark.name = "DeathMark";

        //弾を消滅させる
        Destroy(gameObject);

        //衝突したプレイヤーを消滅させる
        Destroy(collision.gameObject);
    }

    public void SetFireTankObject(GameObject tankObject)
    {
        m_tankObject = tankObject;
    }

    //弾が削除されたときに呼ばれる
    void OnDestroy()
    {
        switch (gameObject.name)
        {
            //敵AIの弾
            case "EnemyBullet":
                if (m_tankObject is not null)
                {
                    //フィールド上に生成されている弾の数データを減らす
                    m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
                }
                break;

            //プレイヤーの弾
            default:
                if (m_tankObject is not null)
                {
                    //フィールド上に生成されている弾の数データを減らす
                    m_tankObject.gameObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
                }
                break;
        }
    }
}
}