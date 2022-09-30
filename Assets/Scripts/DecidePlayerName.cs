using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DecidePlayerName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_playerNameText = null;
    [SerializeField] int m_maxCharacterNum = 0;

    public void SetNameText(string character)
    {
        switch (character)
        {
            case "BACK":
                //�������͂���Ă��Ȃ������疖�����폜���Ȃ�
                if(m_playerNameText.text.Length == 0)
                {
                    return;
                }
                //���O�̖������폜����B
                m_playerNameText.text = m_playerNameText.text[..^1];
                break;

            case "OK":
                //�������͂���Ă��Ȃ�������ok�����Ȃ�
                if (m_playerNameText.text.Length == 0)
                {
                    return;
                }
                //�o�^���ꂽ���O���Z�[�u
                PlayerPrefs.SetString("PlayerName", m_playerNameText.text);
                PlayerPrefs.Save();
                //�^���N�I���V�[���ɑJ��
                SceneManager.LoadScene("SelectTankScene");
                break;

            default:
                //��������ȏゾ�����甽�f�����Ȃ�
                if(m_playerNameText.text.Length > m_maxCharacterNum-1)
                {
                    return;
                }

                m_playerNameText.text += character;
                break;
    }
    }
}
