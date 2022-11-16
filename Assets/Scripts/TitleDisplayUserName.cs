using UnityEngine;
using TMPro;

/// <summary>
/// �^�C�g����ʂ̃��[�U�[���\������
/// </summary>
namespace nsTankLab
{
    public class TitleDisplayUserName : MonoBehaviour
    {
        void Start()
        {
            //�ȑO�̃v���C�Ŗ��O���o�^����Ă����ꍇ�A
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                //�o�^����Ă������O��\��������
                GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerName");

                Debug.Log($"<color=yellow>���[�U�[��:{GetComponent<TextMeshProUGUI>().text}</color>");
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = "NoName";
            }
        }
    }
}