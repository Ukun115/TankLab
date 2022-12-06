using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("ひび割れマテリアル")] MeshRenderer m_meshRenderer = null;
        [SerializeField, TooltipAttribute("耐久値")] int m_durableValue = 5;

        int m_bulletDestroyCount = 0;

        void Start()
        {
            //ひび割れ初期化
            m_meshRenderer.material.SetFloat("_CrackProgress", 0.0f);
        }

        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
			{
				//弾(通常弾&ロケット弾)に衝突したときの処理
				case TagName.Bullet:
                case TagName.RocketBullet:
					OnCollisitonBullet();
					break;
            }
		}

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case TagName.Bomb:
                    Destroy(gameObject);
                    break;
            }
        }

        void OnCollisitonBullet()
        {
            m_bulletDestroyCount++;

            //ひび割れを入れていく
            m_meshRenderer.material.SetFloat("_CrackProgress", m_bulletDestroyCount/ m_durableValue);

            //耐久値限界まで来たら、
            if (m_bulletDestroyCount >= m_durableValue)
            {
                //壁破壊
                Destroy(gameObject);
            }
        }
    }
}