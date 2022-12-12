using UnityEngine;

/// <summary>
/// プレイヤーの回転処理
/// </summary>
namespace nsTankLab
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("プレイヤーのトランスフォーム")] Transform m_playerTransform = null;

        //前フレームのプレイヤーの位置
        Vector3 m_beforeFramePosition = Vector3.zero;

        float m_beforeFrameVertical = 0.0f;

        PlayerMovement m_playerMovement = null;

        Quaternion m_rotation = Quaternion.identity;

        //剛体
        Rigidbody m_rigidbody = null;

        SaveData m_saveData = null;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();
        }

        void FixedUpdate()
        {
            //ゲーム進行が止まっているときは実行しない
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //プレイヤーの回転処理
            UpdateRotation();
        }

        //プレイヤーの回転処理
        void UpdateRotation()
        {
            //前フレームとの位置の差から進行方向を割り出してその方向に回転します。
            Vector3 differenceDis = new Vector3(m_playerTransform.position.x, 0, m_playerTransform.position.z) - new Vector3(m_beforeFramePosition.x, 0, m_beforeFramePosition.z);
            m_beforeFramePosition = m_playerTransform.position;
            //少しでも動いていたら、
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

        //コンポーネント取得
        void GetComponents()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_playerMovement = GetComponent<PlayerMovement>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }
    }
}