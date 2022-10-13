using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Matching now...の「...」をアニメーションさせる処理
/// </summary>
public class MatchingNowTextMove : MonoBehaviour
{
    TextMeshProUGUI m_matchingNowText = null;

    int m_timer = 0;

    void Start()
    {
        m_matchingNowText = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        m_timer++;

        switch (m_timer)
        {
            case 60:
                m_matchingNowText.text = "Matching Now.";
                break;
            case 120:
                m_matchingNowText.text = "Matching Now..";
                break;
            case 180:
                m_matchingNowText.text = "Matching Now...";
                break;
            case 240:
                m_matchingNowText.text = "Matching Now";
                m_timer = 0;
                break;
        }
    }
}
