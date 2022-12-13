using UnityEngine;

namespace nsTankLab
{
    /// <summary>
    /// バックシールドスキル
    /// </summary>
    public class EnemyAICreateBackShield : MonoBehaviour
    {
        GameObject m_backShieldObject = null;

        bool m_isInstantiate = false;

        int m_coolTime = 5;

        void Start()
        {
            BackShieldInstantiate();
        }

        void Update()
        {
            //バックシールドが消えたら、
            if(m_isInstantiate)
            {
                //5秒後にバックシールドを自動生成
                Invoke(nameof(BackShieldInstantiate), m_coolTime);

                m_isInstantiate = false;
            }
        }

        void BackShieldInstantiate()
        {
            m_backShieldObject = Instantiate(
                Resources.Load("EnemyAIBackShield") as GameObject,
                transform.position,
                transform.rotation,
                transform
            );
            m_backShieldObject.name = "Shield";
            m_backShieldObject.transform.Find("FrontWall").GetComponent<BulletBackShield>().SetIsEnemy();
        }

        public void GoInstantiate()
        {
            m_backShieldObject = null;
            m_isInstantiate = true;
        }
    }
}