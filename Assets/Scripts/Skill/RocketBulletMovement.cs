using UnityEngine;

/// <summary>
/// ’e‚ÌˆÚ“®ˆ—
/// </summary>
namespace nsTankLab
{
    public class RocketBulletMovement : MonoBehaviour
    {
        //„‘Ì
        Rigidbody m_rigidbody = null;

        void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            AddForce();
        }

		void AddForce()
        {
            m_rigidbody.AddForce(
            transform.forward.x * 400.0f*Time.deltaTime,
            0.0f,
            transform.forward.z * 400.0f*Time.deltaTime,
            ForceMode.VelocityChange
            );

            m_rigidbody.velocity = transform.forward;
        }
    }
}