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
            //�G����������Ă���Q�[���I�u�W�F�N�g��T�������̂�Start�֐��̌Ă΂��ق�̏������Ƃɏ������s���B
            Invoke(nameof(CountEnemyNum),0.1f);
        }

        void CountEnemyNum()
        {
            GameObject[] m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
            m_enemyNumText.text = $"x{m_enemies.Length}";
        }
    }
}