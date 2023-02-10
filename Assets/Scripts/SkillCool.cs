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
            //�N�[����
            if(m_isCooling)
            {
                if (!m_wait)
                {
                    //1�b��Ƀe�L�X�g�X�V
                    Invoke(nameof(UpdateText), 1.0f);

                    m_wait = true;
                }
            }
        }

        //�e�L�X�g�X�V����
        void UpdateText()
        {
            m_wait = false;

            m_coolText.text = $"{m_coolTime}";

            //�N�[���^�C�����I�������A
            if (m_coolTime <= 0)
            {
                //�e�L�X�g��\��
                m_coolText.text = "";
                //�N�[���^�C�������甲����
                m_isCooling = false;
            }
            m_coolTime--;
        }

        //�N�[�����J�n�����鏈��
        public void CoolStart(int coolTime)
        {
            m_isCooling = true;
            m_coolTime = coolTime;

            m_coolText.text = $"{m_coolTime}";
            m_coolTime--;
        }
    }
}