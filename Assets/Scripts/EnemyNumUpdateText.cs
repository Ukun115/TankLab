using UnityEngine;
using UnityEngine.SceneManagement;
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

        void Update()
        {
            //�}�b�`���O�V�[���A�I�����C���V�[���A���[�J���Q�[���V�[���̂Ƃ��A
            if(SceneManager.GetActiveScene().name == SceneName.MatchingScene|| SceneManager.GetActiveScene().name == SceneName.OnlineGameScene|| SceneManager.GetActiveScene().name == SceneName.LocalGameScene)
            {
                //�e�L�X�g�폜
                Destroy(this.gameObject);
            }
        }
    }
}