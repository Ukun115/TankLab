using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody m_rigidbody = null;
    [SerializeField] float m_bulletSpeed = 0.0f;

    //�{�[���������������̖̂@���x�N�g��
    private Vector3 objNomalVector = Vector3.zero;
    // ���˕Ԃ������verocity
    [HideInInspector] public Vector3 afterReflectVero = Vector3.zero;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(transform.forward.x * m_bulletSpeed, 0, transform.forward.z * m_bulletSpeed, ForceMode.VelocityChange);
		m_rigidbody.velocity = transform.forward;
		afterReflectVero = m_rigidbody.velocity;
	}

    void Update()
    {
        
	}

	void OnCollisionEnter(Collision collision)
	{

		// �����������̖̂@���x�N�g�����擾
		objNomalVector = collision.contacts[0].normal;
		Vector3 reflectVec = Vector3.Reflect(afterReflectVero, objNomalVector);

		// �v�Z�������˃x�N�g����ۑ�
		afterReflectVero = reflectVec;
		m_rigidbody.AddForce(afterReflectVero.x * m_bulletSpeed * 1.5f, 0, afterReflectVero.z * m_bulletSpeed * 1.5f, ForceMode.VelocityChange);

	}

	public void RefrectionBeforeMove()
    {
        //���x1.5�{
        m_rigidbody.AddForce(m_rigidbody.velocity.normalized.x * m_bulletSpeed * 1.5f, 0, m_rigidbody.velocity.normalized.z * m_bulletSpeed * 1.5f, ForceMode.VelocityChange);
    }
}
