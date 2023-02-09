using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// オンラインでコントローラープレイする際、２Pになったときカーソルの色を２Pカラーにする処理
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

            //操作するプレイヤーが2Pの場合、
            if(m_saveData.GetSetPlayerNum == 1)
            {
                m_cursorImage.sprite = m_player2CursorSprite;
                m_cursorImage.color = m_player2Color;
            }

            Destroy(this);
        }
    }
}