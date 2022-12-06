using UnityEngine;
using TMPro;

/// <summary>
/// �x�����b�Z�[�W�\������
/// </summary>
namespace nsTankLab
{
    public class WarningTextDisplay : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�x�����b�Z�[�W�e�L�X�g")] TextMeshProUGUI m_warningText = null;

        //���l
        float alphaValue = 0.0f;

        int m_displayTimer = 0;

        const float FADE_VALUE = 0.05f;
        const float ALPHA_ZERO = 0.0f;

        enum EnState
        {
            enWait,     //�ҋ@
            enDisplay,  //�\��
            enFade,     //�t�F�[�h
        }
        EnState m_enState = EnState.enWait;

        void Update()
        {
            switch(m_enState)
            {
                //�ҋ@��ԁB�Ȃɂ����Ȃ��B
                case EnState.enWait:
                    break;

                //��莞�ԕ\��
                case EnState.enDisplay:
                    m_displayTimer++;
                    if(m_displayTimer > 180)
                    {
                        //�t�F�[�h��Ԃɂ���
                        m_enState = EnState.enFade;

                        //�^�C�}�[�����Z�b�g
                        m_displayTimer = 0;
                    }
                    break;

                //�t�F�[�h
                case EnState.enFade:
                    alphaValue -= FADE_VALUE;
                    m_warningText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);
                    if(alphaValue < ALPHA_ZERO)
                    {
                        //�ҋ@��Ԃɂ���
                        m_enState = EnState.enWait;
                    }
                    break;
            }
        }

        public void Display(string text)
        {
            m_warningText.text = text;

            //���l������������
            alphaValue = 1.0f;

            //�^�C�}�[�����Z�b�g
            m_displayTimer = 0;

            //���l���X�V
            m_warningText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);

            m_enState = EnState.enDisplay;
        }
    }
}