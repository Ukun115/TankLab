using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// 弾が何かにぶつかったときに起こる処理
/// </summary>
namespace nsTankLab
{
    public class RocketBulletCollision : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("死亡マーカープレファブオブジェクト")] GameObject m_deathMarkPrefab = null;
        [SerializeField, TooltipAttribute("爆発エフェクトプレファブ(タンク)")] GameObject m_explosionTankEffectPrefab = null;
        [SerializeField, TooltipAttribute("爆発エフェクトプレファブ(弾)")] GameObject m_explosionBulletEffectPrefab = null;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        //バーチャルカメラ
        Cinemachine.CinemachineImpulseSource m_virtualCamera = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            //バーチャルカメラ
            m_virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineImpulseSource>();
        }

        //衝突処理
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                //壁に衝突したときの処理
                case TagName.Wall:
                    OnCollisitonWall();
                    break;
                //プレイヤーor敵AIに衝突したときの処理
                case TagName.Player:
                case TagName.Enemy:
                    OnCollisitonPlayerOrEnemyAI(collision);
                    break;
                //弾に衝突したときの処理
                case TagName.Bullet:
                    //接触した壁にスパークエフェクトを生成する。
                    GameObject explosionBulletEffect1 = Instantiate(
                    m_explosionBulletEffectPrefab,
                    transform.position,
                    Quaternion.identity
                    );
                    explosionBulletEffect1.name = "ExplosionBulletEffect";

                    //両方消滅させる
                    Destroy(gameObject);
                    Destroy(collision.gameObject);

                    //弾消滅SE再生
                    m_soundManager.PlaySE("BulletDestroySE");
                    break;
                case TagName.RocketBullet:

                    //接触した壁にスパークエフェクトを生成する。
                    GameObject explosionBulletEffect2 = Instantiate(
                    m_explosionBulletEffectPrefab,
                    transform.position,
                    Quaternion.identity
                    );
                    explosionBulletEffect2.name = "ExplosionBulletEffect";

                    //両方消滅させる
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    //弾消滅SE再生
                    m_soundManager.PlaySE("BulletDestroySE");
                    break;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //爆弾の爆発にあたったとき
                case TagName.Bomb:
                    //接触した爆弾にスパークエフェクトを生成する。
                    GameObject explosionBulletEffect = Instantiate(
                    m_explosionBulletEffectPrefab,
                    transform.position,
                    Quaternion.identity
                    );
                    explosionBulletEffect.name = "ExplosionBulletEffect";

                    Destroy(gameObject);
                    //弾消滅SE再生
                    m_soundManager.PlaySE("BulletDestroySE");

                    //爆弾を爆発させる
                    other.gameObject.GetComponent<ExplosionBomb>().ActiveCollision();
                    break;
            }
        }

        //壁に衝突したときの処理
        void OnCollisitonWall()
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

            if (collision.gameObject.tag == TagName.Player)
            {
                //プレイヤーの体力を減少させる
                m_saveData.GetSetHitPoint--;
            }

            //弾を消滅させる
            Destroy(gameObject);

            //衝突したプレイヤーを消滅させる
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                PhotonNetwork.Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }

            //カメラを振動させる
            m_virtualCamera.GenerateImpulse();
        }
    }
}