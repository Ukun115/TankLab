using UnityEngine;
using TMPro;

/// <summary>
/// Matching now...�́u...�v���A�j���[�V���������鏈��
/// </summary>
namespace nsTankLab
{
    public class MatchingNowTextMove : MonoBehaviour
    {
        //MatchingNow...�̃e�L�X�g
        TextMeshProUGUI m_matchingNowText = null;

        //�_�\���Ԋu
        const int INTERVAL = 60;

        int m_timer = 0;

        void Start()
        {
            m_matchingNowText = GetComponent<TextMeshProUGUI>();

            m_matchingNowText.text = "Matching Now.";
        }

        void Update()
        {
            m_timer++;

            if (m_timer > INTERVAL)
            {
                //�e�L�X�g�X�V
                UpdateText();

                m_timer = 0;
            }
        }

        //�e�L�X�g�X�V����
        void UpdateText()
        {
            m_matchingNowText.text += ".";
            if (m_matchingNowText.text == "Matching Now....")
            {
                m_matchingNowText.text = "Matching Now";
            }
        }
    }
}