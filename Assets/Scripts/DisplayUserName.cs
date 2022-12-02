using UnityEngine;
using TMPro;

/// <summary>
/// タイトル画面のユーザー名表示処理
/// </summary>
namespace nsTankLab
{
    public class DisplayUserName : MonoBehaviour
    {
        TextMeshProUGUI m_userNameTMPro = null;

        void Start()
        {
            m_userNameTMPro = GetComponent<TextMeshProUGUI>();

            //以前のプレイで名前が登録されていた場合、
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                //登録されていた名前を表示させる
                m_userNameTMPro.text = PlayerPrefs.GetString("PlayerName");

                Debug.Log($"<color=yellow>ユーザー名:{m_userNameTMPro.text}</color>");
            }
            else
            {
                m_userNameTMPro.text = "NoName";
            }
        }
    }
}