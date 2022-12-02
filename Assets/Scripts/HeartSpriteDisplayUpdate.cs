using UnityEngine;
using UnityEngine.UI;

namespace nsTankLab
{
    /// <summary>
    /// �n�[�gUI�̕\���X�V����
    /// </summary>
    public class HeartSpriteDisplayUpdate : MonoBehaviour
    {
        [SerializeField] Image[] m_heartImage = { null };
        [SerializeField] Sprite m_heartAvailableSprite = null;
        [SerializeField] Sprite m_heartNotAvailableSprite = null;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //�n�[�gUI�\���X�V
            for (int heartNum = 0; heartNum < 3; heartNum++)
            {
                //�܂��Ƃ肠�����̗͂���n�[�g�摜�ɕύX
                m_heartImage[heartNum].sprite = m_heartAvailableSprite;
                //�̗͂̌���ɂ��������đ̗͂Ȃ��n�[�g�摜�ɕύX
                if (heartNum >= m_saveData.GetSetHitPoint)
                {
                    m_heartImage[heartNum].sprite = m_heartNotAvailableSprite;
                }
            }
        }
    }
}