using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 弾が何かにぶつかったときに起こる処理
/// </summary>
public class BulletCollision : MonoBehaviour
{
    //弾を発射する処理が書かれたスクリプト
    PlayerFireBullet m_fireBulletScript = null;
    BulletMovement m_bulletMovement = null;
    //剛体
    Rigidbody m_rigidbody = null;

    //現在の弾の反射回数
    int m_refrectionCount = 0;

    [SerializeField, TooltipAttribute("死亡マーカープレファブオブジェクト")] GameObject m_deathMarkPrefab = null;

    [SerializeField, TooltipAttribute("リザルト処理が内包されているプレファブオブジェクト")] GameObject m_resultPrefab = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    //弾を減少させたかどうか
    bool m_isNumReduce = true;

    //発射したプレイヤー番号
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //発射したプレイヤー番号を取得
        m_myPlayerNum = int.Parse(Regex.Replace(this.transform.name, @"[^1-4]", "")) - 1;

        m_fireBulletScript = GameObject.Find("FireBulletPos").GetComponent<PlayerFireBullet>();

        m_rigidbody = GetComponent<Rigidbody>();

        m_bulletMovement = GetComponent<BulletMovement>();
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

        //プレイヤーに衝突した場合
        if (collision.gameObject.CompareTag("Player"))
        {
            //プレイヤーに衝突したときの処理
            OnCollisitonPlayer(collision);
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

        m_bulletMovement.SetIsRefrectionBefore(true);

        //指定されている反射回数分反射したら、
        if (m_refrectionCount > m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletRefrectionNum())
        {
            m_bulletMovement.SetIsRefrectionBefore(false);

            if (m_isNumReduce)
            {
                //フィールド上に生成されている弾の数データを減らす
                m_fireBulletScript.ReduceBulletNum();

                m_isNumReduce = false;
            }

            //弾を消滅させる
            Destroy(this.gameObject, 0.05f);

        }

        transform.rotation = new Quaternion(0.0f, m_rigidbody.velocity.y, 0.0f, 1.0f);
    }

    //プレイヤーに衝突したときの処理
    void OnCollisitonPlayer(Collision collision)
    {
        if (m_isNumReduce)
        {
            //フィールド上に生成されている弾の数データを減らす
            m_fireBulletScript.ReduceBulletNum();

            m_isNumReduce = false;
        }

        //弾を消滅させる
        Destroy(this.gameObject);

        //衝突したプレイヤーを消滅させる
        Destroy(collision.gameObject, 0.05f);

        //死んだ場所に×死亡マークオブジェクトを生成する。
        Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                collision.gameObject.transform.position.x,
                collision.gameObject.transform.position.y - 0.49f,
                collision.gameObject.transform.position.z
                ),
            collision.gameObject.transform.rotation
            );

        //リザルト処理をまとめているゲームオブジェクトを生成し、
        //リザルト処理を実行していく。
        GameObject resultObject = Instantiate(m_resultPrefab);

        //死亡したプレイヤーによって分岐
        switch (collision.gameObject.name)
        {
            case "1P":
                //2P勝利表示
                resultObject.GetComponent<ResultInit>().SetWinPlayer(2);
                break;
            case "2P":
                //1P勝利表示
                resultObject.GetComponent<ResultInit>().SetWinPlayer(1);
                break;
        }
    }
}