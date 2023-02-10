using UnityEngine;

/// <summary>
/// トレーニングモードの時、敵AIが死んだら復活するようにする処理
/// </summary>
namespace nsTankLab
{
    public class EnemyAIRepop : MonoBehaviour
    {
        SceneSwitcher m_sceneSwitcher = null;

        bool m_isRepop = false;

        void Start()
        {
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        }

        //衝突処理
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case TagName.Bullet:
                case TagName.RocketBullet:
                    m_isRepop = true;
                    break;
            }
        }

        //死亡したとき
        void OnDestroy()
        {
            if (m_isRepop)
            {
                m_sceneSwitcher.StartTransition(SceneName.TrainingScene, false);
            }
        }
    }
}