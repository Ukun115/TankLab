using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �I�����C���ŃR���g���[���[�v���C����ہA�QP�ɂȂ����Ƃ��J�[�\���̐F���QP�J���[�ɂ��鏈��
/// </summary>
namespace nsTankLab
{
    public class CursorColorChange : MonoBehaviour
    {
        SaveData m_saveData = null;

        [SerializeField]Image m_cursorImage = null;
        [SerializeField] Sprite m_player2CursorSprite = null;
        Color m_player2Color = new Color( 255,0,125,255 );

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //���삷��v���C���[��2P�̏ꍇ�A
            if(m_saveData.GetSetPlayerNum == 1)
            {
                m_cursorImage.sprite = m_player2CursorSprite;
                m_cursorImage.color = m_player2Color;
            }

            Destroy(this);
        }
    }
}