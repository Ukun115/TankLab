using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody m_rigidbody = null;
    [SerializeField] float m_bulletSpeed = 0.0f;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_rigidbody.velocity = transform.forward * m_bulletSpeed;
    }
}
