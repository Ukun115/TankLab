using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody m_rigidbody = null;
    [SerializeField] float m_bulletSpeed = 0.0f;
    bool m_isRefrectionBefore = true;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(transform.forward.x * m_bulletSpeed, 0, transform.forward.z * m_bulletSpeed, ForceMode.VelocityChange);
		//m_rigidbody.velocity = transform.forward;
	}

    void Update()
    {
        if (m_isRefrectionBefore)
        {
            //m_rigidbody.velocity = transform.forward * m_bulletSpeed;

        }
        else
        {
            //m_rigidbody.velocity = transform.forward * m_bulletSpeed;
        }
    }

    public void SetIsRefrectionBefore(bool isRefrectionBefore)
    {
        m_isRefrectionBefore = isRefrectionBefore;
    }
}
