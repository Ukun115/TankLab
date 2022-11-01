using UnityEngine;

/// <summary>
/// タンクのステータス
/// </summary>
[CreateAssetMenu]
public class TankStatus : ScriptableObject
{
    [SerializeField, TooltipAttribute("タンク移動速度")] float m_tankSpeed = 0.0f;

    [SerializeField, TooltipAttribute("弾の移動速度")] float m_bulletSpeed = 0.0f;

    [SerializeField, TooltipAttribute("弾の反射回数")] int m_bulletRefrectionNum = 0;

    [SerializeField, TooltipAttribute("連射数")] int m_rapidFireNum = 0;

    [SerializeField, TooltipAttribute("同時発射弾数"),Range(1,3)] int m_sameTimeBulletNum = 0;


    //タンク移動速度
    public float GetTankSpeed()
    {
        return m_tankSpeed;
    }

    //弾の移動速度
    public float GetBulletSpeed()
    {
        return m_bulletSpeed;
    }

    //弾の反射回数
    public int GetBulletRefrectionNum()
    {
        return m_bulletRefrectionNum;
    }

    //連射数
    public int GetRapidFireNum()
    {
        return m_rapidFireNum;
    }

    //同時発射弾数
    public int GetSameTimeBulletNum()
    {
        return m_sameTimeBulletNum;
    }
}