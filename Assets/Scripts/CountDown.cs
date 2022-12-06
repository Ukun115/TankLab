using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// �J�E���g�_�E������
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

            //�^���N��e�������Ȃ��悤�ɂ���
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
                    //�^���N��e��������悤�ɂ���
                    m_saveData.GetSetmActiveGameTime = true;
                    //�J�E���g�_�E���X�N���v�g���폜
                    Destroy(gameObject);
                    break;
            }

            m_timer++;
        }

        void Count()
        {
            //�J�E���g�_�E���̕\���e�L�X�g������
            m_countDownText.text = $"{m_countDownTime}";

            m_countDownTime--;
        }
    }
}