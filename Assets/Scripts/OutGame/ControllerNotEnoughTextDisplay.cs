using UnityEngine;
using TMPro;

/// <summary>
/// �ڑ�����Ă���R���g���[���[�����s�����Ă���Ƃ��̌x�����b�Z�[�W�\������
/// </summary>
public class ControllerNotEnoughTextDisplay : MonoBehaviour
{
    [SerializeField, TooltipAttribute("�ڑ��R���g���[���[���s���x�����b�Z�[�W�e�L�X�g")] TextMeshProUGUI m_controllerNotEnoughText = null;

    //���l
    float alphaValue = 0.0f;

    int m_displayTimer = 0;

    enum EnState
    {
        enWait,
        enDisplay,
        enFade,
    }
    EnState m_enState = EnState.enWait;

    void Update()
    {
        switch(m_enState)
        {
            case EnState.enWait:
                //�ҋ@��ԁB�Ȃɂ����Ȃ��B
                break;
            case EnState.enDisplay:
                //��莞�ԕ\������
                m_displayTimer++;
                if(m_displayTimer > 180)
                {
                    //�t�F�[�h��Ԃɂ���
                    m_enState = EnState.enFade;

                    //�^�C�}�[�����Z�b�g
                    m_displayTimer = 0;
                }
                break;
            case EnState.enFade:
                //�t�F�[�h���Ă���
                alphaValue -= 0.05f;
                m_controllerNotEnoughText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);
                if(alphaValue < 0.0f)
                {
                    //�ҋ@��Ԃɂ���
                    m_enState = EnState.enWait;
                }
                break;
        }
    }

    public void Display()
    {
        //���l������������
        alphaValue = 1.0f;

        //�^�C�}�[�����Z�b�g
        m_displayTimer = 0;

        //���l���X�V
        m_controllerNotEnoughText.color = new Color(1.0f, 0.0f, 0.0f, alphaValue);

        m_enState = EnState.enDisplay;
    }
}
