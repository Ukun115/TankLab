using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// �e�̈ړ�����
/// </summary>
namespace nsTankLab
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("���G�t�F�N�g")] GameObject m_smokeEffectPrefab = null;
        [SerializeField, TooltipAttribute("�e���b�V���g�����X�t�H�[��")] Transform m_bulletMeshTransform = null;

        //����
        Rigidbody m_rigidbody = null;

        //���˂����v���C���[�ԍ�
        int m_myPlayerNum = 0;

        //�e�������������̖̂@���x�N�g��
        Vector3 m_objectNormalVector = Vector3.zero;
        //���˕Ԃ�����̃x�N�g��
        Vector3 m_afterReflectVector = Vector3.zero;

        SaveData m_saveData = null;

        Vector3 m_debugLineDirection = Vector3.zero;

        //�O�t���[���̒e�̈ʒu
        Vector3 m_previousFlamePosition = Vector3.zero;
        //�ړ�����
        float m_movingDistance = 0.0f;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponens();

            //�GAI�̒e����Ȃ��Ƃ��͎��s
            if (gameObject.name != "EnemyBullet")
            {
                //���˂����v���C���[�ԍ����擾
                m_myPlayerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", string.Empty)) - 1;
            }

            AddForce();
        }

        void Update()
        {
            //Ray�̃f�o�b�N�\��
            Debug.DrawRay(transform.position, m_debugLineDirection * 5.0f, Color.red);
        }

        void FixedUpdate()
        {
            if (!m_saveData.GetSetmActiveGameTime)
            {
                m_rigidbody.velocity = Vector3.zero;
            }
            else
            {
                m_rigidbody.velocity = m_debugLineDirection * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed();

                //�ړ����������߂�
                m_movingDistance = ((transform.position - m_previousFlamePosition) / Time.deltaTime).magnitude;
                //�Q�[�����Ԑi�s���Ɉړ����Ă��Ȃ������炨�������B
                //���̏ꍇ�͒e���폜����B
                if(m_movingDistance<0.01f)
                {
                    Destroy(this.gameObject);
                }
                //�O�t���[���̈ʒu���擾
                m_previousFlamePosition = transform.position;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            //�����������̖̂@���x�N�g�����擾
            m_objectNormalVector = collision.contacts[0].normal;
            Vector3 reflectVector = Vector3.Reflect(m_afterReflectVector, m_objectNormalVector);

            //�v�Z�������˃x�N�g����ۑ�
            m_afterReflectVector = reflectVector;
            m_rigidbody.AddForce(
                m_afterReflectVector.x * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed() * 1.5f,
                0.0f,
                m_afterReflectVector.z * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed() * 1.5f,
                ForceMode.VelocityChange
            );

            m_debugLineDirection = m_afterReflectVector;

            //�e�̉��̌�����ǔ��ˌ�̌����ɕύX
            m_smokeEffectPrefab.transform.LookAt(transform.position - m_debugLineDirection);
            //���b�V���̌�����ǔ��ˌ�̌����ɕύX
            m_bulletMeshTransform.LookAt(transform.position - m_debugLineDirection);
        }

        void AddForce()
        {
            m_rigidbody.AddForce(
            transform.forward.x * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed(),
            0,
            transform.forward.z * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed(),
            ForceMode.VelocityChange
            );

            m_rigidbody.velocity = transform.forward;

            m_afterReflectVector = m_rigidbody.velocity;

            m_debugLineDirection = transform.forward;
        }

        //�R���|�[�l���g�擾
        void GetComponens()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_rigidbody = GetComponent<Rigidbody>();
        }
    }
}