using UnityEngine;

/// <summary>
/// �e�̈ړ�����
/// </summary>
public class BulletMovement : MonoBehaviour
{
    //����
    Rigidbody m_rigidbody = null;
    //�e�̈ړ����x
    [SerializeField] float m_bulletSpeed = 0.0f;
    //���˂���O���ǂ���
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
        //���x1.5�{
		m_rigidbody.AddForce(m_rigidbody.velocity.normalized.x * m_bulletSpeed * 1.5f,0, m_rigidbody.velocity.normalized.z * m_bulletSpeed * 1.5f, ForceMode.VelocityChange);
        m_refrectionBefore = false;
	}

    //���˂���O���ǂ����̃Z�b�^�[
    public void SetIsRefrectionBefore(bool isRefrectionBefore)
    {
        m_refrectionBefore = isRefrectionBefore;
    }
}
