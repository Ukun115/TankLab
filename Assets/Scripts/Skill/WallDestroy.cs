using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        int m_bulletDestroyCount = 0;

        //‚Ð‚ÑŠ„‚êƒ}ƒeƒŠƒAƒ‹
        [SerializeField] MeshRenderer m_meshRenderer = null;

        void Start()
        {
            //‚Ð‚ÑŠ„‚ê‰Šú‰»
            m_meshRenderer.material.SetFloat("_CrackProgress", 0.0f);
        }

        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
			{
				//’e‚ÉÕ“Ë‚µ‚½‚Æ‚«‚Ìˆ—
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

            //‚Ð‚ÑŠ„‚ê‚ð“ü‚ê‚Ä‚¢‚­
            m_meshRenderer.material.SetFloat("_CrackProgress", m_bulletDestroyCount/5.0f);

            //‘Ï‹v’lŒÀŠE‚Ü‚Å—ˆ‚½‚çA
            if (m_bulletDestroyCount >= 5)
            {
                //•Ç”j‰ó
                Destroy(gameObject);
            }
        }

        void OnCollisitonBomb()
        {

             Destroy(gameObject);

        }
    }
}
