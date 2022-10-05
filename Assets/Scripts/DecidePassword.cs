using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DecidePassword : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_numberText = null;
    [SerializeField] int m_maxCharacterNum = 0;

    public void SetCharacter(string character)
    {
        switch (character)
        {
            case "BACK":
                //�������͂���Ă��Ȃ������疖�����폜���Ȃ�
                if(m_numberText.text.Length == 0)
                {
                    return;
                }
                //���O�̖������폜����B
                m_numberText.text = m_numberText.text[..^1];
                break;

            case "OK":
                //�������͂���Ă��Ȃ�������ok�����Ȃ�
                if (m_numberText.text.Length == 0)
                {
                    return;
                }
                //�^���N�I���V�[���ɑJ��
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                break;

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
