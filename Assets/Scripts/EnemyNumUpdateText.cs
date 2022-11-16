using UnityEngine;
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
            //敵が生成されてからゲームオブジェクトを探したいのでStart関数の呼ばれるほんの少しあとに処理を行う。
            Invoke(nameof(CountEnemyNum),0.1f);
        }

        void CountEnemyNum()
        {
            GameObject[] m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
            m_enemyNumText.text = $"x{m_enemies.Length}";
        }
    }
}