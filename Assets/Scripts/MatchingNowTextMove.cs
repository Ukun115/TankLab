using UnityEngine;
using TMPro;

/// <summary>
/// Matching now...の「...」をアニメーションさせる処理
/// </summary>
public class MatchingNowTextMove : MonoBehaviour
{
    //MatchingNow...のテキスト
    TextMeshProUGUI m_matchingNowText = null;

    //タイマー
    int m_timer = 0;

    //点を更新させる間隔
    [SerializeField] int m_interval = 0;

    void Start()
    {
        m_matchingNowText = this.GetComponent<TextMeshProUGUI>();

        m_matchingNowText.text = "Matching Now.";
    }

    void Update()
    {
        m_timer++;

        //テキストを更新させる間隔が来たら、
        if (m_timer == m_interval)
        {
            //テキスト更新処理
            UpdateText();
        }
    }

    //テキスト更新処理
    void UpdateText()
    {
        //現在のテキストによってテキスト変更
        switch (m_matchingNowText.text)
        {
            case "Matching Now":
                m_matchingNowText.text = "Matching Now.";
                break;
            case "Matching Now.":
                m_matchingNowText.text = "Matching Now..";
                break;
            case "Matching Now..":
                m_matchingNowText.text = "Matching Now...";
                break;
            case "Matching Now...":
                m_matchingNowText.text = "Matching Now";
                break;
        }
        m_timer = 0;
    }
}
