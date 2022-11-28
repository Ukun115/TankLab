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
    //剛体
    Rigidbody m_rigidbody = null;

    ControllerData m_controllerData = null;

    //移動方向
    Vector3 m_moveDirection = Vector3.zero;

    [SerializeField, TooltipAttribute("プレイヤーのトランスフォーム")] Transform m_playerTransform = null;

    //左右矢印キーの値(-1.0f～1.0f)
    float m_horizontal = 0.0f;
    //上下矢印キーの値(-1.0f～1.0f)
    float m_vertical = 0.0f;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    //自身のプレイヤー番号
    int m_myPlayerNum = 0;

    float m_skillSpeed = 1.0f;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        m_rigidbody = GetComponent<Rigidbody>();

        //自身のプレイヤー番号を取得
        m_myPlayerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", ""));

        //選択されたタンク番号デバック
        Debug.Log($"<color=yellow>{name}のタンク : Tank{m_saveData.GetSelectTankNum(m_myPlayerNum-1)}</color>");
        //選択されたスキル番号デバック
        Debug.Log($"<color=yellow>{name}のスキル : Skill{m_saveData.GetSelectSkillNum(m_myPlayerNum-1)}</color>");
    }

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (SceneManager.GetActiveScene().name == "OnlineGameScene" && !photonView.IsMine)
        {
            return;
        }

        //入力されたキーの値を保存
        m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
        m_moveDirection = m_playerTransform.TransformDirection(m_moveDirection);
        //斜めの距離が長くなる(√2倍になる)のを防ぐ。
        m_moveDirection.Normalize();

        //移動方向に速度を掛ける(通常移動)
        m_moveDirection *= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum-1)].GetTankSpeed() * m_skillSpeed;
    }

    void FixedUpdate()
    {
            //ゲーム進行が止まっているときは移動速度を0にする。
            if (!m_saveData.GetSetmActiveGameTime)
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

        /// <summary>
        /// 移動操作（上下左右キーなど）を取得
        /// </summary>
        /// <param name="movementValue"></param>
        void OnMove(InputValue movementValue)
        {
            //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
            if (SceneManager.GetActiveScene().name == "OnlineGameScene" && !photonView.IsMine)
            {
                return;
            }

            // Moveアクションの入力値を取得
            Vector2 movementVector = movementValue.Get<Vector2>();

            // x,y軸方向の入力値を変数に代入
            m_horizontal = movementVector.x;
            m_vertical = movementVector.y;
        }
    }
}