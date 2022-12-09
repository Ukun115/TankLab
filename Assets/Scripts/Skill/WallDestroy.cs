using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�Ђъ���}�e���A��")] MeshRenderer m_meshRenderer = null;
        [SerializeField, TooltipAttribute("�ϋv�l")] float m_durableValue = 5.0f;

        float m_bulletDestroyCount = 0.0f;

        void Start()
        {
            //�Ђъ��ꏉ����
            m_meshRenderer.material.SetFloat("_CrackProgress", 0.0f);
        }

        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
			{
				//�e(�ʏ�e&���P�b�g�e)�ɏՓ˂����Ƃ��̏���
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

            //�Ђъ�������Ă���
            m_meshRenderer.material.SetFloat("_CrackProgress", m_bulletDestroyCount/ m_durableValue);

            //�ϋv�l���E�܂ŗ�����A
            if (m_bulletDestroyCount >= m_durableValue)
            {
                //�ǔj��
                Destroy(gameObject);
            }
        }
    }
}