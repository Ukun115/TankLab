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
                //シールドにあたったオブジェクトが弾の場合
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
            //親が敵AIのとき、
            if (m_isParentEnemy)
            {
                transform.parent.gameObject.transform.parent.GetComponent<EnemyAICreateBackShield>().GoInstantiate();
            }
            //親がプレイヤーのとき、
            else
            {
                transform.parent.gameObject.transform.parent.GetComponent<CreateBackShield>().GoInstantiate();
            }
        }

        void Damage(Collider other,int damageValue)
        {
            //弾を削除
            Destroy(other.gameObject);

            m_nowDamage += damageValue;

            //耐久値までダメージが入ったら、
            if (m_nowDamage >= DEFENCE_VALUE)
            {
                //撃破音再生
                m_soundManager.PlaySE("BrokenWallDestroySE");

                //シールドを削除
                Destroy(gameObject);
            }
        }

        public void SetIsEnemy()
        {
            m_isParentEnemy = true;
        }
    }
}