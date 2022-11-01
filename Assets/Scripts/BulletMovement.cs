using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody m_rigidbody = null;
    [SerializeField] float m_bulletSpeed = 0.0f;

    //ボールが当たった物体の法線ベクトル
    private Vector3 objNomalVector = Vector3.zero;
    // 跳ね返った後のverocity
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

		// 当たった物体の法線ベクトルを取得
		objNomalVector = collision.contacts[0].normal;
		Vector3 reflectVec = Vector3.Reflect(afterReflectVero, objNomalVector);

		// 計算した反射ベクトルを保存
		afterReflectVero = reflectVec;
		m_rigidbody.AddForce(afterReflectVero.x * m_bulletSpeed * 1.5f, 0, afterReflectVero.z * m_bulletSpeed * 1.5f, ForceMode.VelocityChange);

	}

	public void RefrectionBeforeMove()
    {
        //速度1.5倍
        m_rigidbody.AddForce(m_rigidbody.velocity.normalized.x * m_bulletSpeed * 1.5f, 0, m_rigidbody.velocity.normalized.z * m_bulletSpeed * 1.5f, ForceMode.VelocityChange);
    }
}
