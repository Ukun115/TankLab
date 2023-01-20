using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// 接続されているコントローラーのデータを保存している場所
/// </summary>
namespace nsTankLab
{
    public class ControllerData : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("マウスカーソル画像オブジェクト")] Texture2D[] m_mouseCursorTexture = null;

        int m_controllerNum = 0;

        //1=1P,2=2P
        int m_cursorColorNum = 1;

        void Start()
        {
            SearchConnectedController();

            Debug.Log($"<color=yellow>接続されているコントローラー数:{m_controllerNum}</color>");
        }

        void Update()
        {
            SearchConnectedController();

            if(Gamepad.current is not null)
            {
                //マウスカーソルの画像を無くす。
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                //マウスカーソルの画像をレティクルにする
                Cursor.SetCursor(m_mouseCursorTexture[m_cursorColorNum-1], new Vector2(m_mouseCursorTexture[m_cursorColorNum - 1].width/2, m_mouseCursorTexture[m_cursorColorNum - 1].height/2), CursorMode.Auto);
            }
        }

        void SearchConnectedController()
        {
            if(m_controllerNum != Gamepad.all.Count)
            {
                Debug.Log($"接続されているコントローラー数 : {m_controllerNum}→{Gamepad.all.Count}");
            }

                // 接続されているコントローラー数を調べる
                m_controllerNum = Gamepad.all.Count;
        }

        public void SetCursorColor(int playerNum)
        {
            m_cursorColorNum = playerNum;
        }

        public Gamepad GetGamepad(int playerNum)
        {
            if(m_controllerNum < playerNum)
            {
                return null;
            }

            return Gamepad.all[playerNum-1];
        }

        public int GetConnectGamepad()
        {
            return m_controllerNum;
        }
    }
}