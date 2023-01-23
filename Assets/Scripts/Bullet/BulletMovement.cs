using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 弾の移動処理
/// </summary>
namespace nsTankLab
{
    public class BulletMovement : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("煙エフェクト")] GameObject m_smokeEffectPrefab = null;
        [SerializeField, TooltipAttribute("弾メッシュトランスフォーム")] Transform m_bulletMeshTransform = null;

        //剛体
        Rigidbody m_rigidbody = null;

        //発射したプレイヤー番号
        int m_myPlayerNum = 0;

        //弾が当たった物体の法線ベクトル
        Vector3 m_objectNormalVector = Vector3.zero;
        //跳ね返った後のベクトル
        Vector3 m_afterReflectVector = Vector3.zero;

        SaveData m_saveData = null;

        Vector3 m_debugLineDirection = Vector3.zero;

        //前フレームの弾の位置
        Vector3 m_previousFlamePosition = Vector3.zero;
        //移動距離
        float m_movingDistance = 0.0f;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponens();

            //敵AIの弾じゃないときは実行
            if (gameObject.name != "EnemyBullet")
            {
                //発射したプレイヤー番号を取得
                m_myPlayerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", string.Empty)) - 1;
            }

            AddForce();
        }

        void Update()
        {
            //Rayのデバック表示
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

                //移動距離を求める
                m_movingDistance = ((transform.position - m_previousFlamePosition) / Time.deltaTime).magnitude;
                //ゲーム時間進行中に移動していなかったらおかしい。
                //その場合は弾を削除する。
                if(m_movingDistance<0.01f)
                {
                    Destroy(this.gameObject);
                }
                //前フレームの位置を取得
                m_previousFlamePosition = transform.position;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            //あたった物体の法線ベクトルを取得
            m_objectNormalVector = collision.contacts[0].normal;
            Vector3 reflectVector = Vector3.Reflect(m_afterReflectVector, m_objectNormalVector);

            //計算した反射ベクトルを保存
            m_afterReflectVector = reflectVector;
            m_rigidbody.AddForce(
                m_afterReflectVector.x * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed() * 1.5f,
                0.0f,
                m_afterReflectVector.z * m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletSpeed() * 1.5f,
                ForceMode.VelocityChange
            );

            m_debugLineDirection = m_afterReflectVector;

            //弾の煙の向きを壁反射後の向きに変更
            m_smokeEffectPrefab.transform.LookAt(transform.position - m_debugLineDirection);
            //メッシュの向きを壁反射後の向きに変更
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

        //コンポーネント取得
        void GetComponens()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_rigidbody = GetComponent<Rigidbody>();
        }
    }
}