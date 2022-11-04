using UnityEngine;

/// <summary>
/// �_�b�V�����C���𐶐����鏈��
/// </summary>
public class DashLine : MonoBehaviour
{
    [SerializeField] GameObject m_dashLinePrefab = null;

    void Start()
    {
        GameObject m_dashLineObject = Instantiate(
            m_dashLinePrefab,
            transform.position,
            new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
            );
        m_dashLineObject.transform.parent = this.transform;
    }
}