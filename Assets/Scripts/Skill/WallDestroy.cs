using UnityEngine;

namespace nsTankLab
{
    public class WallDestroy : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�Ђъ���}�e���A��")] MeshRenderer m_meshRenderer = null;

        int m_bulletDestroyCount = 0;

        void Start()
        {
            //�Ђъ��ꏉ����
            m_meshRenderer.material.SetFloat("_CrackProgress", 0.0f);
        }

        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
			{
				//�e�ɏՓ˂����Ƃ��̏���
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

            //�Ђъ�������Ă���
            m_meshRenderer.material.SetFloat("_CrackProgress", m_bulletDestroyCount/5.0f);

            //�ϋv�l���E�܂ŗ�����A
            if (m_bulletDestroyCount >= 5)
            {
                //�ǔj��
                Destroy(gameObject);
            }
        }

        void OnCollisitonBomb()
        {

             Destroy(gameObject);

        }
    }
}
