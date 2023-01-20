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
            //コンポーネント取得まとめ
            GetComponents();

            Invoke(nameof(Ct), m_coolTime);
        }

        void Update()
        {
            //ゲーム進行が止まっているとき
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            if (m_skillFlg)
            {
                m_skillFlg = false;

                BombInstantiate();

                //設置音再生
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

        //コンポーネント取得
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
