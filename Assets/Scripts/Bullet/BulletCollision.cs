using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// 弾が何かにぶつかったときに起こる処理
/// </summary>
namespace nsTankLab
{
    public class BulletCollision : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("死亡マーカープレファブオブジェクト")] GameObject m_deathMarkPrefab = null;
        [SerializeField, TooltipAttribute("スパークエフェクト")] GameObject m_sparkEffectPrefab = null;
        [SerializeField, TooltipAttribute("爆発エフェクトプレファブ(タンク)")] GameObject m_explosionTankEffectPrefab = null;
        [SerializeField, TooltipAttribute("爆発エフェクトプレファブ(弾)")] GameObject m_explosionBulletEffectPrefab = null;
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

        //現在の弾の反射回数
        int m_refrectionCount = 0;

        //発射したプレイヤー番号
        int m_playerNum = 0;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        //自身を発射したタンクのオブジェクトデータ
        GameObject m_tankObject = null;

        //バーチャルカメラ
        Cinemachine.CinemachineImpulseSource m_virtualCamera = null;

        ControllerData m_controllerData = null;

        void Start()
        {
            //コンポ―ネント取得まとめ
            GetComponens();

            //敵AIの弾じゃないときは実行
            if (gameObject.name != "EnemyBullet")
            {
                //オンラインで相手が生成した弾
                if (gameObject.name == "Bullet(Clone)")
                {
                    //自身がプレイヤー1
                    if (m_saveData.GetSetPlayerNum == 1)
                    {
                        gameObject.name = $"PlayerBullet{2}";
                        m_playerNum = 1;

                        m_tankObject = GameObject.Find("2P/PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition");
                    }
                    //自身がプレイヤー2
                    else if (m_saveData.GetSetPlayerNum == 2)
                    {
                        gameObject.name = $"PlayerBullet{1}";
                        m_playerNum = 0;

                        m_tankObject = GameObject.Find("1P/PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition");
                    }
                }
                else
                {
                    //発射したプレイヤー番号を取得
                    m_playerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", string.Empty)) - 1;
                }
            }
        }

        //衝突処理
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                //壁
                case TagName.Wall:
                    OnCollisitonWall();
                    break;
                //プレイヤーor敵AI
                case TagName.Player:
                case TagName.Enemy:
                    OnCollisitonPlayerOrEnemyAI(collision);
                    break;
                //弾
                case TagName.Bullet:
                    //弾爆発エフェクト生成
                    ExplosionBulletEffectInstantiate();

                    Destroy(collision.gameObject);
                    break;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //爆弾
                case TagName.Bomb:
                    //弾爆発エフェクト生成
                    ExplosionBulletEffectInstantiate();

                    //爆弾を爆発させる
                    other.gameObject.GetComponent<ExplosionBomb>().ActiveCollision();
                    break;
            }
        }

        //弾爆発エフェクト生成処理
        void ExplosionBulletEffectInstantiate()
        {
            //接触した爆弾にスパークエフェクトを生成する。
            GameObject explosionBulletEffect = Instantiate(
            m_explosionBulletEffectPrefab,
            transform.position,
            Quaternion.identity
            );
            explosionBulletEffect.name = "ExplosionBulletEffect";

            Destroy(gameObject);

            //弾消滅SE再生
            if (m_soundManager is null)
            {
                m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            }
            m_soundManager.PlaySE("BulletDestroySE");
        }

        //壁に衝突したときの処理
        void OnCollisitonWall()
        {
            m_refrectionCount++;

            //指定されている反射回数分反射したら、
            if (m_refrectionCount > m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_playerNum)].GetBulletRefrectionNum())
            {
                //弾爆発エフェクト生成
                ExplosionBulletEffectInstantiate();
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
            DeathMarkInstantiate(collision);

            //死んだ場所に爆発エフェクトを生成する。
            ExplosionEffectInstantiate();

            //プレイヤーの場合
            if (collision.gameObject.tag == TagName.Player)
            {
                //プレイヤーの体力を減少させる
                m_saveData.GetSetHitPoint--;
            }

            //弾を消滅させる
            Destroy(gameObject);

            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                PhotonNetwork.Destroy(collision.gameObject);
            }
            else
            {
                //衝突したプレイヤーを消滅させる
                Destroy(collision.gameObject);
            }

            //カメラ&ゲームパッド振動処理
            Vibration(collision);
        }

        //死亡マーカー生成処理
        void DeathMarkInstantiate(Collision collision)
        {
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
        }

        //爆発エフェクト生成処理
        void ExplosionEffectInstantiate()
        {
            GameObject m_explosionEffect = Instantiate(
            m_explosionTankEffectPrefab,
            transform.position,
            Quaternion.identity
            );
            m_explosionEffect.name = "ExplosionTankEffect";
        }

        //コントローラーと画面を振動させる処理
        void Vibration(Collision collision)
        {
            //カメラを振動させる
            m_virtualCamera.GenerateImpulse();

            if (collision.gameObject.tag == TagName.Player)
            {
                //ゲームパッドが接続されていたら、
                if (Gamepad.current is not null)
                {
                    if (m_controllerData.GetGamepad(int.Parse(Regex.Replace(collision.gameObject.name, @"[^1-4]", string.Empty))) is not null)
                    {
                        //撃破されたゲームパッドを振動させる
                        m_controllerData.GetGamepad(int.Parse(Regex.Replace(collision.gameObject.name, @"[^1-4]", string.Empty))).SetMotorSpeeds(0.0f, 1.0f);
                    }
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
            if(gameObject.name.Contains("EnemyBullet"))
            {
                if (m_tankObject != null)
                {
                    //フィールド上に生成されている弾の数データを減らす
                    m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
                }
            }
            //プレイヤーの弾
            if (gameObject.name.Contains("PlayerBullet"))
            {
                if (m_tankObject != null)
                {
                    //フィールド上に生成されている弾の数データを減らす
                    m_tankObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
                }
            }
        }

        //コンポーネント取得
        void GetComponens()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

            //バーチャルカメラ
            m_virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineImpulseSource>();
        }
    }
}