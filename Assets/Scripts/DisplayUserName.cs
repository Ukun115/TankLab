using UnityEngine;
using TMPro;

/// <summary>
/// �^�C�g����ʂ̃��[�U�[���\������
/// </summary>
namespace nsTankLab
{
    public class DisplayUserName : MonoBehaviour
    {
        TextMeshProUGUI m_userNameTMPro = null;

        void Start()
        {
            m_userNameTMPro = GetComponent<TextMeshProUGUI>();

            //�ȑO�̃v���C�Ŗ��O���o�^����Ă����ꍇ�A
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                //�o�^����Ă������O��\��������
                m_userNameTMPro.text = PlayerPrefs.GetString("PlayerName");

                Debug.Log($"<color=yellow>���[�U�[��:{m_userNameTMPro.text}</color>");
            }
            else
            {
                m_userNameTMPro.text = "NoName";
            }
        }
    }
}