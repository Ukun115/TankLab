using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// �GAI�̐����X�V�����鏈��
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