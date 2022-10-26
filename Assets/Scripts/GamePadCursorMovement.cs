using UnityEngine;

/// <summary>
/// ゲームパッド用のカーソル移動処理
/// </summary>
public class GamePadCursorMovement : MonoBehaviour
{
    //左右矢印キーの値(-1.0f～1.0f)
    float m_horizontal = 0.0f;
    //上下矢印キーの値(-1.0f～1.0f)
    float m_vertical = 0.0f;

    //移動方向
    Vector2 m_moveDirection = Vector2.zero;

    [SerializeField, TooltipAttribute("プレイヤー番号"), Range(1,4)]int m_playerNum = 0;

    //カーソルの移動速度
    const float CURSOR_SPEED = 800.0f;

    Rigidbody2D m_rigidbody2d = null;

    void Start()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //左右矢印キーの値(-1.0f～1.0f)を取得する
        m_horizontal = Input.GetAxisRaw($"{m_playerNum}PJoystickHorizontal_R");
        //上下矢印キーの値(-1.0f～1.0f)を取得する
        m_vertical = Input.GetAxisRaw($"{m_playerNum}PJoystickVertical_R");

        m_moveDirection = new Vector2(m_horizontal,m_vertical);
        //斜めの距離が長くなる√2倍になるのを防ぐ。
        m_moveDirection.Normalize();
        //移動方向に速度を掛ける(通常移動)
        m_moveDirection *= CURSOR_SPEED;

        //移動方向を更新
        m_rigidbody2d.velocity = m_moveDirection;
    }
}