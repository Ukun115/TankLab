using UnityEngine;

/// <summary>
/// プレイヤーの回転処理
/// </summary>
public class PlayerRotation : MonoBehaviour
{
    [SerializeField, TooltipAttribute("プレイヤーのトランスフォーム")] Transform m_playerTransform = null;

    //前フレームのプレイヤーの位置
    Vector3 beforeFramePosition = Vector3.zero;

    PlayerMovement m_playerMovement = null;

    //剛体
    Rigidbody m_rigidbody = null;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        //プレイヤーの回転処理
        UpdateRotation();
    }

    //プレイヤーの回転処理
    void UpdateRotation()
    {
        //前フレームとの位置の差から進行方向を割り出してその方向に回転します。
        Vector3 differenceDis = new Vector3(m_playerTransform.position.x, 0, m_playerTransform.position.z) - new Vector3(beforeFramePosition.x, 0, beforeFramePosition.z);
        beforeFramePosition = m_playerTransform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (m_playerMovement.GetMoveDirection() == Vector3.zero)
            {
                return;
            }
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(m_rigidbody.transform.rotation, rot, 0.2f);
            m_playerTransform.rotation = rot;
        }
    }
}
