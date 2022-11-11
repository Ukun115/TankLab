using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;

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

    //水平入力の名前(初期設定はキーボード操作)
    string m_inputHorizontalName = "Horizontal";
    //垂直入力の名前(初期設定はキーボード操作)
    string m_inputVerticalName = "Vertical";

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    //自身のプレイヤー番号
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        m_rigidbody = GetComponent<Rigidbody>();

        //自身のプレイヤー番号を取得
        m_myPlayerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", "")) - 1;

        // ゲームパッドが接続されていたらゲームパッドでの操作
        if (m_controllerData.GetIsConnectedController(m_saveData.GetSelectTankNum(m_myPlayerNum)))
        {
            m_inputHorizontalName = $"{name}JoystickHorizontal_L";
            m_inputVerticalName = $"{name}JoystickVertical_L";
        }

        //選択されたタンク番号デバック
        Debug.Log($"<color=yellow>{name}のタンク : Tank{m_saveData.GetSelectTankNum(m_myPlayerNum)+1}</color>");
        //選択されたスキル番号デバック
        Debug.Log($"<color=yellow>{name}のスキル : Skill{m_saveData.GetSelectSkillNum(m_myPlayerNum)+1}</color>");
    }

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (SceneManager.GetActiveScene().name == "OnlineGameScene" && !photonView.IsMine)
        {
            return;
        }

        //Input.GetAxisRaw()は、入力された軸に補正をかけず、-1か0か1でをfloat型で返します。
        //Input.GetAxis()は、入力された軸に補正をかけて、-1から1の範囲をfloat型で返します。

        //左右矢印キーの値(-1.0f～1.0f)を取得する
        m_horizontal = Input.GetAxisRaw(m_inputHorizontalName);
        //上下矢印キーの値(-1.0f～1.0f)を取得する
        m_vertical = Input.GetAxisRaw(m_inputVerticalName);

        //入力されたキーの値を保存
        m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
        m_moveDirection = m_playerTransform.TransformDirection(m_moveDirection);
        //斜めの距離が長くなる√2倍になるのを防ぐ。
        m_moveDirection.Normalize();

        //移動方向に速度を掛ける(通常移動)
        m_moveDirection *= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetTankSpeed();
    }

    void FixedUpdate()
    {
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
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
}
}