using UnityEngine;
using UnityEngine.UI;

namespace nsTankLab
{
    /// <summary>
    /// �X�^�[�摜�X�V����
    /// </summary>
    public class UpdateStarImage : MonoBehaviour
    {
        SaveData m_saveData = null;

        //���摜
        [SerializeField] Sprite[] m_starSprite = null;

        //�X�^�[�̎�v���C���[
        [SerializeField] int m_playerNum = 0;

        //�X�^�[�̔ԍ�
        [SerializeField] int m_starNum = 0;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //�X�^�[���擾���Ă�����A
            if (m_saveData.GetStar(m_playerNum - 1, m_starNum-1))
            {
                //�X�^�[�摜������
                GetComponent<Image>().sprite = m_starSprite[m_playerNum-1];

                return;
            }
        }
    }
}