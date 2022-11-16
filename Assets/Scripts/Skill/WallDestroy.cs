using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        int m_bulletDestroyCount = 0;

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

            if (m_bulletDestroyCount > 1)
            {
                Destroy(gameObject);
            }
        }

        void OnCollisitonBomb()
        {
            
             Destroy(gameObject);
 
        }
    }
}
