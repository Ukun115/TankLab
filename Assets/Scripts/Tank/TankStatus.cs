using UnityEngine;

/// <summary>
/// �^���N�̃X�e�[�^�X
/// </summary>
[CreateAssetMenu]
public class TankStatus : ScriptableObject
{
    [SerializeField, TooltipAttribute("�^���N�ړ����x")] float m_tankSpeed = 0.0f;

    [SerializeField, TooltipAttribute("�e�̈ړ����x")] float m_bulletSpeed = 0.0f;

    [SerializeField, TooltipAttribute("�e�̔��ˉ�")] int m_bulletRefrectionNum = 0;

    [SerializeField, TooltipAttribute("�A�ː�")] int m_rapidFireNum = 0;

    [SerializeField, TooltipAttribute("�������˒e��"),Range(1,3)] int m_sameTimeBulletNum = 0;


    //�^���N�ړ����x
    public float GetTankSpeed()
    {
        return m_tankSpeed;
    }

    //�e�̈ړ����x
    public float GetBulletSpeed()
    {
        return m_bulletSpeed;
    }

    //�e�̔��ˉ�
    public int GetBulletRefrectionNum()
    {
        return m_bulletRefrectionNum;
    }

    //�A�ː�
    public int GetRapidFireNum()
    {
        return m_rapidFireNum;
    }

    //�������˒e��
    public int GetSameTimeBulletNum()
    {
        return m_sameTimeBulletNum;
    }
}