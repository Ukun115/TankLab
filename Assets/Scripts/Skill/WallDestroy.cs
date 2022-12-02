using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("ひび割れマテリアル")] MeshRenderer m_meshRenderer = null;

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
				//弾に衝突したときの処理
				case "Bullet":
					OnCollisitonBullet();
					break;
            }
		}

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "Bomb":
                    OnCollisitonBomb();
                    break;
            }
        }

        void OnCollisitonBullet()
        {
            m_bulletDestroyCount++;

            //ひび割れを入れていく
            m_meshRenderer.material.SetFloat("_CrackProgress", m_bulletDestroyCount/5.0f);

            //耐久値限界まで来たら、
            if (m_bulletDestroyCount >= 5)
            {
                //壁破壊
                Destroy(gameObject);
            }
        }

        void OnCollisitonBomb()
        {

             Destroy(gameObject);

        }
    }
}
