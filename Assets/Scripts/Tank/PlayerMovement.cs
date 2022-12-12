using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの移動スクリプト
/// </summary>
namespace nsTankLab
{
    public class PlayerMovement : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("プレイヤーのトランスフォーム")] Transform m_playerTransform = null;
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

        //剛体
        Rigidbody m_rigidbody = null;

        ControllerData m_controllerData = null;

        //移動方向
        Vector3 m_moveDirection = Vector3.zero;

        //左右矢印キーの値(-1.0f～1.0f)
        float m_horizontal = 0.0f;
        //上下矢印キーの値(-1.0f～1.0f)
        float m_vertical = 0.0f;

        //自身のプレイヤー番号
        int m_playerNum = 0;

        float m_skillSpeed = 1.0f;

        SaveData m_saveData = null;

        Vector2 m_stickValue = Vector2.zero;

        bool m_isTeleportStopping = false;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            //自身のプレイヤー番号を取得
            m_playerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", string.Empty));

            //選択されたタンク番号デバック
            Debug.Log($"<color=yellow>{name}のタンク : {m_saveData.GetSelectTankName(m_playerNum-1)}</color>");
            //選択されたスキル番号デバック
            Debug.Log($"<color=yellow>{name}のスキル : {m_saveData.GetSelectSkillName(m_playerNum-1)}</color>");
        }

        void Update()
        {
            //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            //ゲーム進行が止まっているときは実行しない
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            // ゲームパッドが接続されていたらゲームパッド操作
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                m_stickValue = m_controllerData.GetGamepad(m_playerNum).leftStick.ReadValue();
                m_horizontal = m_stickValue.x;
                m_vertical = m_stickValue.y;
            }
            //キーボード操作
            else
            {
                //Dキーが押された
                if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    m_horizontal = 1.0f;
                }
                //Aキーが押された
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    m_horizontal = -1.0f;
                }
                //Wキーが押された
                if (Keyboard.current.wKey.wasPressedThisFrame)
                {
                    m_vertical = 1.0f;
                }
                //Sキーが押された
                if (Keyboard.current.sKey.wasPressedThisFrame)
                {
                    m_vertical = -1.0f;
                }

                //Dキーが離された
                if(Keyboard.current.dKey.wasReleasedThisFrame)
                {
                    m_horizontal = 0.0f;
                }
                //Aキーが離された
                if (Keyboard.current.aKey.wasReleasedThisFrame)
                {
                    m_horizontal = 0.0f;
                }

                //Wキーが離された
                if (Keyboard.current.wKey.wasReleasedThisFrame)
                {
                    m_vertical = 0.0f;
                }
                //Sキーが離された
                if (Keyboard.current.sKey.wasReleasedThisFrame)
                {
                    m_vertical = 0.0f;
                }
            }

            //入力されたキーの値を保存
            m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
            m_moveDirection = m_playerTransform.TransformDirection(m_moveDirection);
            //斜めの距離が長くなる(√2倍になる)のを防ぐ。
            m_moveDirection.Normalize();

            //移動方向に速度を掛ける(通常移動)
            m_moveDirection *= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_playerNum-1)].GetTankSpeed() * m_skillSpeed;
        }

        void FixedUpdate()
        {
            //ゲーム進行が止まっているときは移動速度を0にする。
            if (!m_saveData.GetSetmActiveGameTime || m_isTeleportStopping)
            {
                m_moveDirection = Vector3.zero;
            }
            //剛体に移動を割り当て(一緒に重力も割り当て)
            m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
        }

        public Vector3 GetMoveDirection()
        {
            return m_moveDirection;
        }

        public float GetVertical()
        {
            return m_vertical;
        }
        public float SetSkillSpeed(float skillspeed)
        {
            return m_skillSpeed = skillspeed;
        }

        public Vector3 GetSetPlayerPosition
        {
            get { return m_playerTransform.position; }
            set { m_playerTransform.position = value; }
        }

        public void SetTeleportStopping(bool isStop)
        {
            m_isTeleportStopping = isStop;
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_rigidbody = GetComponent<Rigidbody>();
        }
    }
}