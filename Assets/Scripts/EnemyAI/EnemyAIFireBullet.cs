using UnityEngine;

/// <summary>
/// 敵AIが弾を発射する処理
/// </summary>
namespace nsTankLab
{
    public class EnemyAIFireBullet : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("通常弾プレファブオブジェクト")] GameObject m_normalBulletPrefab = null;
        [SerializeField, TooltipAttribute("ロケット弾プレファブオブジェクト")]GameObject m_rocketBulletPrefab = null;
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("発射位置Transformコンポーネント")] Transform m_firePositionTransform = null;

        //ロケット弾を撃つときはインスペクターでfalseに設定する
        [SerializeField]bool m_addFireRocketBulletSkill = false;

        //弾オブジェクトを格納するオブジェクト
        Transform m_bulletsBox = null;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        //発射したタンク番号
        int m_myTankNum = 0;

        //フィールド上に生成されている弾の数
        int m_bulletNum = 0;

        float m_timer = 0.0f;
        [SerializeField]float m_bulletFireDelay = 1.5f;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            //初手ゲーム開始と同時に撃ってしまわないように初めにディレイをかけてからスタートさせる
            m_timer = m_bulletFireDelay;
        }

        void Update()
        {
            //ゲーム進行が止まっているときは実行しない
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //Rayを作成
            Ray ray = new Ray(m_firePositionTransform.root.position, m_firePositionTransform.forward);
            //Rayのデバック表示
            Debug.DrawRay(m_firePositionTransform.root.position, m_firePositionTransform.forward * 5.0f, Color.red);

            //プレイヤーに衝突したときの処理
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                switch(hit.collider.tag)
                {
                    case TagName.Player:
                    //弾発射
                    FireBullet();
                        break;
                }
            }

            //弾を発射した後のディレイをかける
            //タイマーが作動し0以下になったら自動でタイマー作動終了
            if (m_timer >= 0)
            {
                m_timer-=Time.deltaTime;
            }
        }

        //弾発射処理
        void FireBullet()
        {
            if(!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //連射できる回数以上は発射しないようにする
            if (m_bulletNum >= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myTankNum)].GetRapidFireNum())
            {
                return;
            }

            //発射準備ができていないと発射しないようにする
            if (m_timer > 0)
            {
                return;
            }
            //タイマー作動
            m_timer = m_bulletFireDelay;

            //弾発射SE再生
            m_soundManager.PlaySE("FireSE");

            //弾を生成
            BulletInstantiate();
        }

        void BulletInstantiate()
        {
            //通常弾
            if (!m_addFireRocketBulletSkill)
            {
                //弾を生成
                GameObject m_bulletObject = Instantiate(
                m_normalBulletPrefab,
                m_firePositionTransform.position,
                new Quaternion(0.0f, m_firePositionTransform.rotation.y, 0.0f, m_firePositionTransform.rotation.w)
                );
                //生成される弾の名前変更
                m_bulletObject.name = "EnemyBullet";
                //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                m_bulletObject.transform.parent = m_bulletsBox;
                //生成される弾はタンクと切り離すため、発射したタンクオブジェクトデータを弾スクリプトに渡しておく。
                m_bulletObject.GetComponent<BulletCollision>().SetFireTankObject(gameObject);
            }
            //ロケット弾
            else
            {
                //弾を生成
                GameObject m_bulletObject = Instantiate(
                m_rocketBulletPrefab,
                m_firePositionTransform.position,
                new Quaternion(0.0f, m_firePositionTransform.rotation.y, 0.0f, m_firePositionTransform.rotation.w)
                );
                //生成される弾の名前変更
                m_bulletObject.name = "EnemyBullet";
                //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                m_bulletObject.transform.parent = m_bulletsBox;
            }
        }

        //フィールド上に生成されている弾の数を減らす処理
        public void ReduceBulletNum()
        {
            m_bulletNum = Mathf.Clamp(m_bulletNum-1,0,m_bulletNum);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_bulletsBox = GameObject.Find("Bullets").GetComponent<Transform>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        }
    }
}