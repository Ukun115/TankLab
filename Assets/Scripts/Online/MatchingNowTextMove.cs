using UnityEngine;
using TMPro;

/// <summary>
/// Matching now...の「...」をアニメーションさせる処理
/// </summary>
namespace nsTankLab
{
    public class MatchingNowTextMove : MonoBehaviour
    {
        //MatchingNow...のテキスト
        TextMeshProUGUI m_matchingNowText = null;

        //点表示間隔
        const int INTERVAL = 60;

        int m_timer = 0;

        void Start()
        {
            m_matchingNowText = GetComponent<TextMeshProUGUI>();

            m_matchingNowText.text = "Matching Now.";
        }

        void Update()
        {
            m_timer++;

            if (m_timer > INTERVAL)
            {
                //テキスト更新
                UpdateText();

                m_timer = 0;
            }
        }

        //テキスト更新処理
        void UpdateText()
        {
            m_matchingNowText.text += ".";
            if (m_matchingNowText.text == "Matching Now....")
            {
                m_matchingNowText.text = "Matching Now";
            }
        }
    }
}