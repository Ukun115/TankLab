using UnityEngine;
using TMPro;

/// <summary>
/// �p�X���[�h����ʂɕ\�������鏈��
/// </summary>
namespace nsTankLab
{
    public class PasswordTextDisplay : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TextMeshProUGUI>().text = $"PassWord:{GameObject.Find("SaveData").GetComponent<SaveData>().GetSetInputPassword}";
        }
    }
}