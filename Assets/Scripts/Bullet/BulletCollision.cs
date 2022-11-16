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
    [SerializeField, TooltipAttribute("スパークエフェクト")] GameObject m_sparkEffectPrefab = null;
    [SerializeField, TooltipAttribute("爆発エフェクトプレファブ(タンク)")] GameObject m_explosionTankEffectPrefab = null;
    [SerializeField, TooltipAttribute("爆発エフェクトプレファブ(弾)")] GameObject m_explosionBulletEffectPrefab = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    //発射したプレイヤー番号
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;
    SoundManager m_soundManager = null;

    //自身を発射したタンクのオブジェクトデータ
    GameObject m_tankObject = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

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

                    //接触した壁にスパークエフェクトを生成する。
                    GameObject explosionBulletEffect = Instantiate(
                    m_explosionBulletEffectPrefab,
                    transform.position,
                    Quaternion.identity
                    );
                    explosionBulletEffect.name = "ExplosionBulletEffect";

                    //両方消滅させる
                    Destroy(gameObject);
                Destroy(collision.gameObject);
                    //弾消滅SE再生
                    m_soundManager.PlaySE("BulletDestroySE");
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
                //弾消滅SE再生
                m_soundManager.PlaySE("BulletDestroySE");

                //接触した壁にスパークエフェクトを生成する。
                GameObject explosionBulletEffect = Instantiate(
                m_explosionBulletEffectPrefab,
                transform.position,
                Quaternion.identity
                );
                explosionBulletEffect.name = "ExplosionBulletEffect";

                //弾を消滅させる
                Destroy(gameObject);
        }
        else
            {
                //接触した壁にスパークエフェクトを生成する。
                GameObject sparkEffect = Instantiate(
                m_sparkEffectPrefab,
                transform.position,
                Quaternion.identity
                );
                sparkEffect.name = "SparkEffect";

                //壁反射音再生
                m_soundManager.PlaySE("BulletRefrectionSE");
            }
    }

    //プレイヤーに衝突したときの処理
    void OnCollisitonPlayerOrEnemyAI(Collision collision)
    {
            //撃破音再生
            m_soundManager.PlaySE("DeathSE");

            //死んだ場所に×死亡マークオブジェクトを生成する。
            GameObject deathMark = Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                collision.gameObject.transform.position.x,
                -0.4f,
                collision.gameObject.transform.position.z
                ),
            Quaternion.identity
            );
        deathMark.name = "DeathMark";

            //死んだ場所に爆発エフェクトを生成する。
            GameObject m_explosionEffect = Instantiate(
            m_explosionTankEffectPrefab,
            transform.position,
            Quaternion.identity
            );
            m_explosionEffect.name = "ExplosionTankEffect";

            if (collision.gameObject.tag == "Player")
            {
                //プレイヤーの体力を減少させる
                m_saveData.GetSetHitPoint--;
            }

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
            //敵AIの弾
            if(gameObject.name.Contains("EnemyBullet"))
            {
                if (m_tankObject.ToString() != "null")
                {
                    //フィールド上に生成されている弾の数データを減らす
                    m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
                }
            }
            //プレイヤーの弾
            if (gameObject.name.Contains("PlayerBullet"))
            {
                if (m_tankObject.ToString() != "null")
                {
                    //フィールド上に生成されている弾の数データを減らす
                    m_tankObject.gameObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
                }
            }
    }
}
}