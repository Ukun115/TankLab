using UnityEngine;
using TMPro;

namespace nsTankLab
{
    public class SkillCool : MonoBehaviour
    {
        int m_coolTime = 0;
        bool m_isCooling = false;
        bool m_wait = false;
        [SerializeField]TextMeshProUGUI m_coolText = null;

        void Update()
        {
            //クール中
            if(m_isCooling)
            {
                if (!m_wait)
                {
                    //1秒後にテキスト更新
                    Invoke(nameof(UpdateText), 1.0f);

                    m_wait = true;
                }
            }
        }

        //テキスト更新処理
        void UpdateText()
        {
            m_wait = false;

            m_coolText.text = $"{m_coolTime}";

            //クールタイムが終わったら、
            if (m_coolTime <= 0)
            {
                //テキスト非表示
                m_coolText.text = "";
                //クールタイム中から抜ける
                m_isCooling = false;
            }
            m_coolTime--;
        }

        //クールを開始させる処理
        public void CoolStart(int coolTime)
        {
            m_isCooling = true;
            m_coolTime = coolTime;

            m_coolText.text = $"{m_coolTime}";
            m_coolTime--;
        }
    }
}