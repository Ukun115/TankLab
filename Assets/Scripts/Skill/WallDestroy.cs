using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("‚Ð‚ÑŠ„‚êƒ}ƒeƒŠƒAƒ‹")] MeshRenderer m_meshRenderer = null;
        [SerializeField, TooltipAttribute("‘Ï‹v’l")] float m_durableValue = 5.0f;

        float m_bulletDestroyCount = 0.0f;

        void Start()
        {
            //‚Ð‚ÑŠ„‚ê‰Šú‰»
            m_meshRenderer.material.SetFloat("_CrackProgress", 0.0f);
        }

        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
			{
				//’e(’Êí’e&ƒƒPƒbƒg’e)‚ÉÕ“Ë‚µ‚½‚Æ‚«‚Ìˆ—
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

            Debug.Log(m_bulletDestroyCount / m_durableValue);

            //‚Ð‚ÑŠ„‚ê‚ð“ü‚ê‚Ä‚¢‚­
            m_meshRenderer.material.SetFloat("_CrackProgress", m_bulletDestroyCount/ m_durableValue);

            //‘Ï‹v’lŒÀŠE‚Ü‚Å—ˆ‚½‚çA
            if (m_bulletDestroyCount >= m_durableValue)
            {
                //•Ç”j‰ó
                Destroy(gameObject);
            }
        }
    }
}