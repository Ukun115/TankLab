using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// �e�̈ړ�����
/// </summary>
public class BulletMovement : MonoBehaviour
{
    //����
    Rigidbody m_rigidbody = null;

    //���˂���O���ǂ���
    bool m_refrectionBefore = false;

    //���˂����v���C���[�ԍ�
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

    void Start()
    {
        //���˂����v���C���[�ԍ����擾
        m_myPlayerNum = int.Parse(Regex.Replace(this.transform.name, @"[^1-4]", "")) - 1;

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        m_rigidbody = GetComponent<Rigidbody>();

        m_rigidbody.AddForce(
            transform.forward.x * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed(),
            0,
            transform.forward.z * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed(),
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
		m_rigidbody.AddForce(m_rigidbody.velocity.normalized.x * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed() * 1.5f,0, m_rigidbody.velocity.normalized.z * m_tankDataBase.GetTankLists()[0].GetBulletSpeed() * 1.5f, ForceMode.VelocityChange);
        m_refrectionBefore = false;
	}

    //���˂���O���ǂ����̃Z�b�^�[
    public void SetIsRefrectionBefore(bool isRefrectionBefore)
    {
        m_refrectionBefore = isRefrectionBefore;
    }
}
