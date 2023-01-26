using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// 敵AIの数を更新させる処理
    /// </summary>
    public class EnemyNumUpdateText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_enemyNumText = null;

        void Start()
        {
            GameObject[] m_enemies = GameObject.FindGameObjectsWithTag(TagName.Enemy);
            m_enemyNumText.text = $"x{m_enemies.Length}";
        }

        void Update()
        {
            //マッチングシーン、オンラインシーンのとき、
            if(SceneManager.GetActiveScene().name == SceneName.MatchingScene|| SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                //テキスト削除
                Destroy(this.gameObject);
            }
        }
    }
}