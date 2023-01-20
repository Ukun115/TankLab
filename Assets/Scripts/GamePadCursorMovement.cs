using UnityEngine;

/// <summary>
/// ゲームパッド用のカーソル移動処理
/// </summary>
namespace nsTankLab
{
    public class GamePadCursorMovement : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("プレイヤー番号"), Range(1,4)]int m_playerNum = 0;
        [SerializeField, TooltipAttribute("インゲームかどうか")] bool m_isInGame = false;

        //移動方向
        Vector2 m_moveDirection = Vector2.zero;

        //カーソルの移動速度
        const float CURSOR_SPEED = 800.0f;

        Rigidbody2D m_rigidbody2d = null;

        Vector2 m_stickValue = Vector2.zero;

        ControllerData m_controllerData = null;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();
        }

        void Update()
        {
            if (m_isInGame)
            {
                m_stickValue = m_controllerData.GetGamepad(m_playerNum).rightStick.ReadValue();
            }
            else
            {
                m_stickValue = m_controllerData.GetGamepad(m_playerNum).leftStick.ReadValue();
            }
            m_moveDirection = m_stickValue;
            //斜めの距離が長くなる√2倍になるのを防ぐ。
            m_moveDirection.Normalize();
            //移動方向に速度を掛ける(通常移動)
            m_moveDirection *= CURSOR_SPEED;

            //移動方向を更新
            m_rigidbody2d.velocity = m_moveDirection;
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_rigidbody2d = GetComponent<Rigidbody2D>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}