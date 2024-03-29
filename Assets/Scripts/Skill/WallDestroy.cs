using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("ひび割れマテリアル")] MeshRenderer m_meshRenderer = null;
        [SerializeField, TooltipAttribute("耐久値")] float m_durableValue = 5.0f;


        SoundManager m_soundManager = null;

        float m_bulletDestroyCount = 0.0f;

        void Start()
        {
            //ひび割れ初期化
            m_meshRenderer.material.SetFloat("_CrackProgress", 0.0f);

            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        }

        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
			{
				//弾(通常弾&ロケット弾)に衝突したときの処理
				case TagName.Bullet:
					OnCollisitonBullet(1.0f);
                    break;
                case TagName.RocketBullet:
					OnCollisitonBullet(m_durableValue);
					break;
            }
		}

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case TagName.Bomb:
                    OnCollisitonBullet(m_durableValue);
                    break;
            }
        }

        void OnCollisitonBullet(float addDestroyCountNum)
        {
            m_bulletDestroyCount += addDestroyCountNum;

            //ひび割れを入れていく
            m_meshRenderer.material.SetFloat("_CrackProgress", m_bulletDestroyCount/ m_durableValue);

            //耐久値限界まで来たら、
            if (m_bulletDestroyCount >= m_durableValue)
            {
                //壁破壊
                Destroy(gameObject);

                //破壊音を再生する
                m_soundManager.PlaySE("BrokenWallDestroySE");
            }
        }
    }
}