using UnityEngine;

/// <summary>
/// 弾の移動処理
/// </summary>
public class BulletMovement : MonoBehaviour
{
    //剛体
    Rigidbody m_rigidbody = null;
    //弾の移動速度
    [SerializeField] float m_bulletSpeed = 0.0f;
    //反射する前かどうか
    bool m_refrectionBefore = false;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(
            transform.forward.x * m_bulletSpeed,
            0,
            transform.forward.z * m_bulletSpeed,
            ForceMode.VelocityChange
            );
	}

    void Update()
    {
        if (!m_refrectionBefore)
        {
            return;
        }
        //速度1.5倍
		m_rigidbody.AddForce(m_rigidbody.velocity.normalized.x * m_bulletSpeed * 1.5f,0, m_rigidbody.velocity.normalized.z * m_bulletSpeed * 1.5f, ForceMode.VelocityChange);
        m_refrectionBefore = false;
	}

    //反射する前かどうかのセッター
    public void SetIsRefrectionBefore(bool isRefrectionBefore)
    {
        m_refrectionBefore = isRefrectionBefore;
    }
}
