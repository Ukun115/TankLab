using UnityEngine;

/// <summary>
/// �v���C���[�̉�]����
/// </summary>
public class PlayerRotation : MonoBehaviour
{
    [SerializeField, TooltipAttribute("�v���C���[�̃g�����X�t�H�[��")] Transform m_playerTransform = null;

    //�O�t���[���̃v���C���[�̈ʒu
    Vector3 beforeFramePosition = Vector3.zero;

    PlayerMovement m_playerMovement = null;

    //����
    Rigidbody m_rigidbody = null;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        //�v���C���[�̉�]����
        UpdateRotation();
    }

    //�v���C���[�̉�]����
    void UpdateRotation()
    {
        //�O�t���[���Ƃ̈ʒu�̍�����i�s����������o���Ă��̕����ɉ�]���܂��B
        Vector3 differenceDis = new Vector3(m_playerTransform.position.x, 0, m_playerTransform.position.z) - new Vector3(beforeFramePosition.x, 0, beforeFramePosition.z);
        beforeFramePosition = m_playerTransform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (m_playerMovement.GetMoveDirection() == Vector3.zero)
            {
                return;
            }
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(m_rigidbody.transform.rotation, rot, 0.2f);
            m_playerTransform.rotation = rot;
        }
    }
}
