using UnityEngine;

/// <summary>
/// �v���C���[�̉�]����
/// </summary>
namespace nsTankLab
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�v���C���[�̃g�����X�t�H�[��")] Transform m_playerTransform = null;

        //�O�t���[���̃v���C���[�̈ʒu
        Vector3 m_beforeFramePosition = Vector3.zero;

        float m_beforeFrameVertical = 0.0f;

        PlayerMovement m_playerMovement = null;

        Quaternion m_rotation = Quaternion.identity;

        //����
        Rigidbody m_rigidbody = null;

        SaveData m_saveData = null;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();
        }

        void FixedUpdate()
        {
            //�Q�[���i�s���~�܂��Ă���Ƃ��͎��s���Ȃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //�v���C���[�̉�]����
            UpdateRotation();
        }

        //�v���C���[�̉�]����
        void UpdateRotation()
        {
            //�O�t���[���Ƃ̈ʒu�̍�����i�s����������o���Ă��̕����ɉ�]���܂��B
            Vector3 differenceDis = new Vector3(m_playerTransform.position.x, 0, m_playerTransform.position.z) - new Vector3(m_beforeFramePosition.x, 0, m_beforeFramePosition.z);
            m_beforeFramePosition = m_playerTransform.position;
            //�����ł������Ă�����A
            if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
            {
                if (m_playerMovement.GetMoveDirection() == Vector3.zero)
                {
                    return;
                }
                if(m_playerMovement.GetVertical()>0&& m_beforeFrameVertical>0)
                {
                    m_rotation = Quaternion.LookRotation(differenceDis);
                }
                if(m_playerMovement.GetVertical()<0&&m_beforeFrameVertical<0)
                {
                    m_rotation = Quaternion.LookRotation(-differenceDis);
                }
                m_rotation = Quaternion.Slerp(m_rigidbody.transform.rotation, m_rotation, 0.1f);
                m_playerTransform.rotation = m_rotation;
            }
            m_beforeFrameVertical = m_playerMovement.GetVertical();
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_playerMovement = GetComponent<PlayerMovement>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }
    }
}