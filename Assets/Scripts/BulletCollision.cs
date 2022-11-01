using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾が何かにぶつかったときに起こる処理
/// </summary>
public class BulletCollision : MonoBehaviour
{
    FireBullet m_fireBulletScript = null;

    [SerializeField] GameObject m_deathMarkPrefab = null;
    [SerializeField] GameObject m_resultPrefab = null;
    [SerializeField] int m_refrectionNum = 0;

    Rigidbody m_rigidbody = null;
    BulletMovement m_bulletMovement = null;

    int m_refrectionCount = 0;


    void Start()
    {
        m_fireBulletScript = GameObject.Find("FireBulletPos").GetComponent<FireBullet>();

        m_rigidbody = GetComponent<Rigidbody>();

        m_bulletMovement = GetComponent<BulletMovement>();
    }

    //衝突処理
    void OnCollisionEnter(Collision collision)
    {
        //壁に衝突した場合
        if(collision.gameObject.tag == "Wall")
        {
            m_refrectionCount++;
            //m_bulletMovement.RefrectionBeforeMove();

            if (m_refrectionCount > m_refrectionNum)
            {
                //フィールド上に生成されている弾の数データを減らす
                m_fireBulletScript.ReduceBulletNum();

                //弾を消滅させる
                Destroy(this.gameObject, 0.05f);

            }

        }

        //プレイヤーに衝突した場合
        if (collision.gameObject.tag == "Player")
        {
            //フィールド上に生成されている弾の数データを減らす
            m_fireBulletScript.ReduceBulletNum();

            //弾を消滅させる
            Destroy(this.gameObject, 0.1f);

            //衝突したプレイヤーを消滅させる
            Destroy(collision.gameObject,0.1f);

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
            GameObject resultObject = Instantiate(m_resultPrefab, Vector3.zero, Quaternion.identity);

            //死亡したのが1Pだった場合
            if (collision.gameObject.name == "1P")
            {
                //2P勝利表示
                resultObject.GetComponent<ResultInit>().SetWinPlayer(2);
            }
            //死亡したのが2Pだった場合
            if (collision.gameObject.name == "2P")
            {
                //1P勝利表示
                resultObject.GetComponent<ResultInit>().SetWinPlayer(1);
            }
        }
    }
}