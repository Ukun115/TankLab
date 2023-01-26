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
                //シールドにあたったオブジェクトが弾の場合のみ実行
                case TagName.Bullet:
                case TagName.RocketBullet:
                    //弾を削除
                    Destroy(other.gameObject);
                    //シールドを削除
                    Destroy(gameObject);
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

        public void SetIsEnemy()
        {
            m_isParentEnemy = true;
        }
    }
}