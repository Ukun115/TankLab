using UnityEngine;

namespace nsTankLab
{
    public class BulletBackShield : MonoBehaviour
    {
        bool m_isParentEnemy = false;

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //�V�[���h�ɂ��������I�u�W�F�N�g���e�̏ꍇ�̂ݎ��s
                case TagName.Bullet:
                case TagName.RocketBullet:
                    //�e���폜
                    Destroy(other.gameObject);
                    //�V�[���h���폜
                    Destroy(gameObject);
                    break;
            }
        }

        void OnDestroy()
        {
            //�e���GAI�̂Ƃ��A
            if (m_isParentEnemy)
            {
                transform.parent.gameObject.transform.parent.GetComponent<EnemyAICreateBackShield>().GoInstantiate();
            }
            //�e���v���C���[�̂Ƃ��A
            else
            {
                transform.parent.gameObject.transform.parent.GetComponent<CreateBackShield>().GoInstantiate();
            }
        }

        public void SetIsEnemy()
        {
            m_isParentEnemy = true;
        }
    }
}