using UnityEngine;

/// <summary>
/// �e�̈ړ�����
/// </summary>
namespace nsTankLab
{
    public class RocketBulletMovement : MonoBehaviour
    {
        //����
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