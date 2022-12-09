using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤーリポップ処理
/// </summary>
namespace nsTankLab
{
    public class PlayerRepop : MonoBehaviour
    {
        SaveData m_saveData = null;
        SceneSwitcher m_sceneSwitcher = null;

        bool m_isRepop = false;

        void Start()
        {
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_saveData.GetSetmActiveGameTime = true;
        }

        //衝突処理
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case TagName.Bullet:
                    m_isRepop = true;
                    break;
            }
        }

        //プレイヤーが死亡したとき
        void OnDestroy()
        {
            if (m_isRepop)
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    //トレーニングシーン
                    case SceneName.TrainingScene:
                        m_sceneSwitcher.StartTransition(SceneName.TrainingScene, false);
                        break;
                    //マッチングシーン
                    case SceneName.MatchingScene:
                        m_sceneSwitcher.StartTransition(SceneName.MatchingScene, false);
                        break;
                }
            }
        }
    }
}