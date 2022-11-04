using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 弾の移動処理
/// </summary>
public class BulletMovement : MonoBehaviour
{
    //剛体
    Rigidbody m_rigidbody = null;

    //発射したプレイヤー番号
    int m_myPlayerNum = 0;

    //弾が当たった物体の法線ベクトル
    Vector3 m_objectNormalVector = Vector3.zero;
    //跳ね返った後のベクトル
    Vector3 m_afterReflectVector = Vector3.zero;

    SaveData m_saveData = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    Vector3 m_debugLineDirection = Vector3.zero;

    void Start()
    {
        //敵AIの弾じゃないときは実行
        if (this.gameObject.name != "EnemyBullet")
        {
            //発射したプレイヤー番号を取得
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
        //Rayのデバック表示
        Debug.DrawRay(this.transform.position, m_debugLineDirection * 5.0f, Color.red);
    }

    void OnCollisionEnter(Collision collision)
    {
        //あたった物体の法線ベクトルを取得
        m_objectNormalVector = collision.contacts[0].normal;
        Vector3 reflectVector = Vector3.Reflect(m_afterReflectVector, m_objectNormalVector);

        //計算した反射ベクトルを保存
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
