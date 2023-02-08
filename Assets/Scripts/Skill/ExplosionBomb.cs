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

        bool m_normalColor = true;

        Renderer m_renderer = null;

        int m_timer = 0;

        SaveData m_saveData = null;

        //バーチャルカメラ
        Cinemachine.CinemachineImpulseSource m_virtualCamera = null;

        enum enColor
        {
            enNormalColor,
            enRed
        }

        void Start()
        {
            Invoke(nameof(ActiveFlashing), 20.0f);

            //コンポーネント取得まとめ
            GetComponents();
        }

        void Update()
        {
            //点滅するかどうか
            if(m_isFlashing)
            {
                m_timer++;

                if(m_timer > 5)
                {
                    m_timer = 0;
                    m_normalColor = !m_normalColor;
                }

                if(m_normalColor)
                {
                    //通常色にする
                    m_renderer.material = m_bombMaterial[(int)enColor.enNormalColor];
                }
                else
                {
                    //赤色にする
                    m_renderer.material = m_bombMaterial[(int)enColor.enRed];
                }
            }
        }

        //点滅をアクティブにする
        public void ActiveFlashing()
        {
            m_isFlashing = true;

            Invoke(nameof(ActiveCollision), 1.5f);
        }

        public void ActiveCollision()
        {
            //クールタイム開始
            if (m_dropPlayer != null && m_dropPlayer.tag != TagName.Enemy)
            {
                m_dropPlayer.GetComponent<DropBomb>().CoolStart();
            }

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

            Destroy(gameObject, 0.05f);
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //プレイヤーor敵AIに衝突したときの処理
                case TagName.Player:
                    if (m_dropPlayer == other.gameObject)
                    {
                        return;
                    }
                    //タンクを撃破する
                    OnCollisitonPlayerOrEnemyAI(other);
                    //爆発コリジョンをオンにする
                    ActiveCollision();

                    //カメラを振動させる
                    m_virtualCamera.GenerateImpulse();

                    //プレイヤーの体力を減少させる
                    m_saveData.GetSetHitPoint--;

                    break;
                case TagName.Enemy:
                    if(m_dropPlayer == other.gameObject || m_dropPlayer.tag == TagName.Enemy)
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
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_renderer = GetComponent<Renderer>();
            //バーチャルカメラ
            m_virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineImpulseSource>();
        }
    }
}
