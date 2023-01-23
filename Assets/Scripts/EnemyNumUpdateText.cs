using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// “GAI‚Ì”‚ğXV‚³‚¹‚éˆ—
    /// </summary>
    public class EnemyNumUpdateText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_enemyNumText = null;

        void Start()
        {
            GameObject[] m_enemies = GameObject.FindGameObjectsWithTag(TagName.Enemy);
            m_enemyNumText.text = $"x{m_enemies.Length}";
        }
    }
}