using UnityEngine;

namespace nsTankLab
{
    public class BulletBackShield : MonoBehaviour
    {
        bool m_isParentEnemy = false;

        const int DEFENCE_VALUE = 2;

        int m_nowDamage = 0;

        SoundManager m_soundManager = null;

        void Start()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //�V�[���h�ɂ��������I�u�W�F�N�g���e�̏ꍇ
                case TagName.Bullet:
                    Damage(other,1);
                    break;
                case TagName.RocketBullet:
                    Damage(other,2);
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

        void Damage(Collider other,int damageValue)
        {
            //�e���폜
            Destroy(other.gameObject);

            m_nowDamage += damageValue;

            //�ϋv�l�܂Ń_���[�W����������A
            if (m_nowDamage >= DEFENCE_VALUE)
            {
                //���j���Đ�
                m_soundManager.PlaySE("BrokenWallDestroySE");

                //�V�[���h���폜
                Destroy(gameObject);
            }
        }

        public void SetIsEnemy()
        {
            m_isParentEnemy = true;
        }
    }
}