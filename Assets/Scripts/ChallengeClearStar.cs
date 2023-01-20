using UnityEngine;
using UnityEngine.UI;

namespace nsTankLab
{
    public class ChallengeClearStar : MonoBehaviour
    {
        [SerializeField] Image m_challengeClearStarImage = null;

        void Start()
        {
            //�`�������W�N���A�������Ƃ�����Ƃ��A
            if (PlayerPrefs.GetInt("ChallengeClear") == 1)
            {
                //���摜��\������
                m_challengeClearStarImage.enabled = true;
            }
            //�`�������W�N���A�������Ƃ��Ȃ��Ƃ��A
            else
            {
                //���摜���\���ɂ���
                m_challengeClearStarImage.enabled = false;
            }
        }
    }
}