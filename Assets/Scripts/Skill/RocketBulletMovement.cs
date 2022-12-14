using UnityEngine;

/// <summary>
/// 弾の移動処理
/// </summary>
namespace nsTankLab
{
    public class RocketBulletMovement : MonoBehaviour
    {
        //剛体
        Rigidbody m_rigidbody = null;

        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            AddForce();
        }

		void AddForce()
        {
            m_rigidbody.AddForce(
            transform.forward.x * 6.0f,
            0.0f,
            transform.forward.z * 6.0f,
            ForceMode.VelocityChange
            );

            m_rigidbody.velocity = transform.forward;
        }
    }
}