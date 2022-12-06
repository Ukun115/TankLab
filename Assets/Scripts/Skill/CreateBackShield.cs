using UnityEngine;

namespace nsTankLab
{
    /// <summary>
    /// バックシールドスキル
    /// </summary>
    public class CreateBackShield : MonoBehaviour
    {
        GameObject m_backShieldObject = null;

        bool m_isInstantiate = false;

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
                Invoke(nameof(BackShieldInstantiate), 5.0f);

                m_isInstantiate = false;
            }
        }

        void BackShieldInstantiate()
        {
            m_backShieldObject = Instantiate(
                Resources.Load("BackShield") as GameObject,
                transform.position,
                transform.rotation,
                transform
            );
            m_backShieldObject.name = $"{name}Shield";
        }

        public void GoInstantiate()
        {
            m_backShieldObject = null;
            m_isInstantiate = true;
        }
    }
}