using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody m_rigidbody = null;
    [SerializeField] float m_bulletSpeed = 0.0f;
    //int m_refrectionCount = 0;
    bool m_refrectionBefore = false;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(transform.forward.x * m_bulletSpeed, 0, transform.forward.z * m_bulletSpeed, ForceMode.VelocityChange);
		//m_rigidbody.velocity = transform.forward;
	}

    void Update()
    {
		if (m_refrectionBefore == true)
		{
            //‘¬“x1.5”{
			m_rigidbody.AddForce(m_rigidbody.velocity.normalized.x * m_bulletSpeed * 1.5f,0, m_rigidbody.velocity.normalized.z * m_bulletSpeed * 1.5f, ForceMode.VelocityChange);
            m_refrectionBefore = false;
        }
		//if (m_refrectionCount == 2)
		//{
		//    m_rigidbody.AddForce(m_rigidbody.velocity, ForceMode.Impulse);
		//}
	}

    public void SetIsRefrectionBefore(bool isRefrectionBefore)
    {
        m_refrectionBefore = isRefrectionBefore;
    }
}
