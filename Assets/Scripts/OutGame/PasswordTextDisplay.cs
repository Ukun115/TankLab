using UnityEngine;
using TMPro;

//パスワードを画面に表示させる処理
public class PasswordTextDisplay : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = $"PassWord:{GameObject.Find("SaveData").GetComponent<SaveData>().GetSetInputPassword}";
    }
}
