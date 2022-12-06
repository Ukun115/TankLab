using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// カウントダウン処理
    /// </summary>
    public class CountDown : MonoBehaviour
    {
        TextMeshProUGUI m_countDownText = null;

        int m_countDownTime = 3;

        int m_timer = 0;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            m_countDownText = GetComponent<TextMeshProUGUI>();

            //タンクや弾が動けないようにする
            m_saveData.GetSetmActiveGameTime = false;
        }

        void Update()
        {
            switch(m_timer)
            {
                case 0:
                case 60:
                case 120:
                    Count();
                    break;
                case 180:
                    m_countDownText.text = "Start!!";
                    break;
                case 240:
                    //タンクや弾が動けるようにする
                    m_saveData.GetSetmActiveGameTime = true;
                    //カウントダウンスクリプトを削除
                    Destroy(gameObject);
                    break;
            }

            m_timer++;
        }

        void Count()
        {
            //カウントダウンの表示テキスト初期化
            m_countDownText.text = $"{m_countDownTime}";

            m_countDownTime--;
        }
    }
}