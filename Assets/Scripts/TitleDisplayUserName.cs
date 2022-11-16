using UnityEngine;
using TMPro;

/// <summary>
/// タイトル画面のユーザー名表示処理
/// </summary>
namespace nsTankLab
{
    public class TitleDisplayUserName : MonoBehaviour
    {
        void Start()
        {
            //以前のプレイで名前が登録されていた場合、
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                //登録されていた名前を表示させる
                GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("PlayerName");

                Debug.Log($"<color=yellow>ユーザー名:{GetComponent<TextMeshProUGUI>().text}</color>");
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = "NoName";
            }
        }
    }
}