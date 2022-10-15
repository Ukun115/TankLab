using UnityEngine;
using TMPro;

/// <summary>
/// Matching now...�́u...�v���A�j���[�V���������鏈��
/// </summary>
public class MatchingNowTextMove : MonoBehaviour
{
    //MatchingNow...�̃e�L�X�g
    TextMeshProUGUI m_matchingNowText = null;

    //�^�C�}�[
    int m_timer = 0;

    //�_���X�V������Ԋu
    [SerializeField] int m_interval = 0;

    void Start()
    {
        m_matchingNowText = this.GetComponent<TextMeshProUGUI>();

        m_matchingNowText.text = "Matching Now.";
    }

    void Update()
    {
        m_timer++;

        //�e�L�X�g���X�V������Ԋu��������A
        if (m_timer == m_interval)
        {
            //�e�L�X�g�X�V����
            UpdateText();
        }
    }

    //�e�L�X�g�X�V����
    void UpdateText()
    {
        //���݂̃e�L�X�g�ɂ���ăe�L�X�g�ύX
        switch (m_matchingNowText.text)
        {
            case "Matching Now":
                m_matchingNowText.text = "Matching Now.";
                break;
            case "Matching Now.":
                m_matchingNowText.text = "Matching Now..";
                break;
            case "Matching Now..":
                m_matchingNowText.text = "Matching Now...";
                break;
            case "Matching Now...":
                m_matchingNowText.text = "Matching Now";
                break;
        }
        m_timer = 0;
    }
}
