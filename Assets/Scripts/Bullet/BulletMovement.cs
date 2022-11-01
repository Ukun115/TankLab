using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 弾の移動処理
/// </summary>
public class BulletMovement : MonoBehaviour
{
    //剛体
    Rigidbody m_rigidbody = null;

    //反射する前かどうか
    bool m_refrectionBefore = false;

    //発射したプレイヤー番号
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    void Start()
    {
        //発射したプレイヤー番号を取得
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

        //速度1.5倍
		m_rigidbody.AddForce(m_rigidbody.velocity.normalized.x * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed() * 1.5f,0, m_rigidbody.velocity.normalized.z * m_tankDataBase.GetTankLists()[0].GetBulletSpeed() * 1.5f, ForceMode.VelocityChange);
        m_refrectionBefore = false;
	}

    //反射する前かどうかのセッター
    public void SetIsRefrectionBefore(bool isRefrectionBefore)
    {
        m_refrectionBefore = isRefrectionBefore;
    }
}
