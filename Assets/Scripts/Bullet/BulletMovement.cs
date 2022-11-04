using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// �e�̈ړ�����
/// </summary>
public class BulletMovement : MonoBehaviour
{
    //����
    Rigidbody m_rigidbody = null;

    //���˂����v���C���[�ԍ�
    int m_myPlayerNum = 0;

    //�e�������������̖̂@���x�N�g��
    Vector3 m_objectNormalVector = Vector3.zero;
    //���˕Ԃ�����̃x�N�g��
    Vector3 m_afterReflectVector = Vector3.zero;

    SaveData m_saveData = null;

    [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

    Vector3 m_debugLineDirection = Vector3.zero;

    void Start()
    {
        //�GAI�̒e����Ȃ��Ƃ��͎��s
        if (this.gameObject.name != "EnemyBullet")
        {
            //���˂����v���C���[�ԍ����擾
            m_myPlayerNum = int.Parse(Regex.Replace(this.transform.name, @"[^1-4]", "")) - 1;
        }

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        m_rigidbody = GetComponent<Rigidbody>();

        m_rigidbody.AddForce(
            transform.forward.x * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed(),
            0,
            transform.forward.z * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed(),
            ForceMode.VelocityChange
            );

        m_rigidbody.velocity = transform.forward;

        m_afterReflectVector = m_rigidbody.velocity;

        m_debugLineDirection = this.transform.forward;

    }

    void Update()
    {
        //Ray�̃f�o�b�N�\��
        Debug.DrawRay(this.transform.position, m_debugLineDirection * 5.0f, Color.red);
    }

    void OnCollisionEnter(Collision collision)
    {
        //�����������̖̂@���x�N�g�����擾
        m_objectNormalVector = collision.contacts[0].normal;
        Vector3 reflectVector = Vector3.Reflect(m_afterReflectVector, m_objectNormalVector);

        //�v�Z�������˃x�N�g����ۑ�
        m_afterReflectVector = reflectVector;
        m_rigidbody.AddForce(
            m_afterReflectVector.x* m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed()*1.5f,
            0.0f,
            m_afterReflectVector.z* m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed() * 1.5f,
            ForceMode.VelocityChange
            );

        m_debugLineDirection = m_afterReflectVector;
    }
}
