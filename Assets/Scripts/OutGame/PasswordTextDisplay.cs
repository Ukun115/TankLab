using UnityEngine;
using TMPro;

//�p�X���[�h����ʂɕ\�������鏈��
public class PasswordTextDisplay : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = $"PassWord:{GameObject.Find("SaveData").GetComponent<SaveData>().GetSetInputPassword}";
    }
}
