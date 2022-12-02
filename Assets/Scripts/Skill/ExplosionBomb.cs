using UnityEngine;

namespace nsTankLab
{
    public class ExplosionBomb : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("Rigidbody")] Rigidbody m_rigidbody = null;
        [SerializeField, TooltipAttribute("爆発の当たり判定")] SphereCollider m_sphereCollider = null;
        [SerializeField, TooltipAttribute("死亡マーカープレファブオブジェクト")] GameObject m_deathMarkPrefab = null;
        [SerializeField, TooltipAttribute("爆発エフェクトプレファブ")] GameObject m_explosionEffectPrefab = null;
        [SerializeField, TooltipAttribute("爆弾のマテリアル")] Material[] m_bombMaterial = null;

        SoundManager m_soundManager = null;

        GameObject m_dropPlayer = null;

        //点滅するかどうか
        bool m_isFlashing = false;

        Renderer m_renderer = null;

        int m_timer = 0;

        //バーチャルカメラ
        Cinemachine.CinemachineImpulseSource m_virtualCamera = null;

        void Start()
        {
            Invoke(nameof(ActiveFlashing), 3.5f);
            Invoke(nameof(ActiveCollision), 5.0f);

            //コンポーネント取得まとめ
            GetComponents();
        }

        void Update()
        {
            //点滅するかどうか
            if(m_isFlashing)
            {
                m_timer++;

                switch (m_timer)
                {
                    case 1:
                    //赤色にする
                    m_renderer.material = m_bombMaterial[0];
                        break;

                    case 6:
                        //通常色にする
                        m_renderer.material = m_bombMaterial[1];
                        break;

                    case 11:
                        //タイマーを初期化する
                        m_timer = 0;
                        break;
                }
            }
        }

        //点滅をアクティブにする
        public void ActiveFlashing()
        {
            m_isFlashing = true;
        }

        public void ActiveCollision()
        {
            //爆発音再生
            m_soundManager.PlaySE("BombExplosionSE");

            //爆発エフェクト再生
            GameObject fireEffect = Instantiate(
            m_explosionEffectPrefab,
            transform.position,
            transform.rotation
            );
            fireEffect.name = "BombExplosionEffect";

            m_sphereCollider.enabled = true;
            m_rigidbody.useGravity = true;
            Destroy(gameObject,0.05f);
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //プレイヤーor敵AIに衝突したときの処理
                case "Player":
                case "Enemy":
                    if(m_dropPlayer == other.gameObject)
                    {
                        return;
                    }
                    //タンクを撃破する
                    OnCollisitonPlayerOrEnemyAI(other);
                    //爆発コリジョンをオンにする
                    ActiveCollision();

                    //カメラを振動させる
                    m_virtualCamera.GenerateImpulse();

                    break;
            }
        }

        public void SetDropPlayer(GameObject dropPlayer)
        {
            m_dropPlayer = dropPlayer;
        }

        //プレイヤーに衝突したときの処理
        void OnCollisitonPlayerOrEnemyAI(Collider other)
        {
            //死んだ場所に×死亡マークオブジェクトを生成する。
            GameObject deathMark = Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                other.gameObject.transform.position.x,
                -0.4f,
                other.gameObject.transform.position.z
                ),
            Quaternion.identity
            );
            deathMark.name = "DeathMark";

            //爆発に巻き込まれたプレイヤーを消滅させる
            Destroy(other.gameObject);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_renderer = GetComponent<Renderer>();
            //バーチャルカメラ
            m_virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineImpulseSource>();
        }
    }
}
