using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ゲームパッド用のカーソル移動処理
/// </summary>
namespace nsTankLab
{
public class GamePadCursorMovement : MonoBehaviour
{
    //移動方向
    Vector2 m_moveDirection = Vector2.zero;

    [SerializeField, TooltipAttribute("プレイヤー番号"), Range(1,4)]int m_playerNum = 0;

    //カーソルの移動速度
    const float CURSOR_SPEED = 800.0f;

    Rigidbody2D m_rigidbody2d = null;

        [SerializeField, TooltipAttribute("インゲームかどうか")] bool m_isInGame = false;

        Vector2 m_stickValue = Vector2.zero;

        void Start()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            if(m_isInGame)
            {
                m_stickValue = Gamepad.current.rightStick.ReadValue();
            }
            else
            {
                m_stickValue = Gamepad.current.leftStick.ReadValue();
            }
            m_moveDirection = m_stickValue;
        //斜めの距離が長くなる√2倍になるのを防ぐ。
        m_moveDirection.Normalize();
        //移動方向に速度を掛ける(通常移動)
        m_moveDirection *= CURSOR_SPEED;

        //移動方向を更新
        m_rigidbody2d.velocity = m_moveDirection;
    }
}
}