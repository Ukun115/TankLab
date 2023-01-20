using UnityEngine;

namespace nsTankLab
{
    public class EnemyAIBomb : MonoBehaviour
    {
        SoundManager m_soundManager = null;

        bool m_skillFlg = false;

        SaveData m_saveData = null;

        SkillCool m_skillCoolScript = null;
        int m_coolTime = 5;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            Invoke(nameof(Ct), m_coolTime);
        }

        void Update()
        {
            //�Q�[���i�s���~�܂��Ă���Ƃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            if (m_skillFlg)
            {
                m_skillFlg = false;

                BombInstantiate();

                //�ݒu���Đ�
                m_soundManager.PlaySE("DropBombSE");

                Invoke(nameof(Ct), m_coolTime);
            }
        }

        void BombInstantiate()
        {
            GameObject bombObject = Instantiate(
                Resources.Load("Bomb") as GameObject,
                new Vector3(transform.position.x,-0.4f, transform.position.z),
                transform.rotation
            );
            bombObject.GetComponent<ExplosionBomb>().SetDropPlayer(gameObject);
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Ct()
        {
            m_skillFlg = true;
        }
    }
}
