using UnityEngine;
using TMPro;

/// <summary>
/// �p�X���[�h�����肷�鏈��
/// </summary>
public class DecidePassword : MonoBehaviour
{
    //�p�X���[�h���i�[����TMPro
    [SerializeField] TextMeshProUGUI m_numberText = null;
    //�p�X���[�h�ɂł���ő啶����
    [SerializeField] int m_maxCharacterNum = 0;

    //�����ꂽ�{�^���̎�ނɂ���ď����𕪊�
    public void SetCharacter(string character)
    {
        switch (character)
        {
            //�߂�{�^��
            case "BACK":
                //�������͂���Ă��Ȃ������疖�����폜���Ȃ�
                if(m_numberText.text.Length == 0)
                {
                    return;
                }
                //�����_�œ��͂���Ă���p�X���[�h�̖������폜����B
                m_numberText.text = m_numberText.text[..^1];
                break;

            //OK�{�^��
            case "OK":
                //4�������͂���Ă��Ȃ�������ok�����Ȃ�
                if (m_numberText.text.Length < 4)
                {
                    return;
                }

                //���͂��ꂽ�p�X���[�h��ۑ�
                GameObject.Find("SaveData").GetComponent<SaveData>().GetSetInputPassword = m_numberText.text;

                //�^���N�I���V�[���ɑJ��
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                break;

            //��L�ȊO�̃{�^��(0�`9)
            default:
                //��������ȏゾ�����甽�f�����Ȃ�
                if(m_numberText.text.Length > m_maxCharacterNum-1)
                {
                    return;
                }

                m_numberText.text += character;
                break;
        }
    }
}
