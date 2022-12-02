using UnityEngine;
using TMPro;

/// <summary>
/// パスワードを画面に表示させる処理
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