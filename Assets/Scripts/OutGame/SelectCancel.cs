using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///選択しているタンクをキャンセルする処理
/// </summary>
namespace nsTankLab
{
    public class SelectCancel : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("プレイヤー番号"), Range(1,4)]int m_playerNum = 0;
        [SerializeField, TooltipAttribute("タンクかスキルか")]string m_tankOrSkill = string.Empty;

        //セーブデータ
        SaveData m_saveData = null;

        bool m_isPressed = false;

        ControllerData m_controllerData = null;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();
        }

        void Update()
        {
            m_isPressed = false;

            // ゲームパッドが接続されていたらゲームパッドでの操作
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                switch (m_tankOrSkill)
                {
                    case "Tank":
                        //LBボタン
                         m_isPressed = m_controllerData.GetGamepad(m_playerNum).leftTrigger.wasPressedThisFrame;
                        break;
                    case "Skill":
                        //RBボタン
                        m_isPressed = m_controllerData.GetGamepad(m_playerNum).rightTrigger.wasPressedThisFrame;
                        break;
                }
            }
            else
            {
                switch (m_tankOrSkill)
                {
                    case "Tank":
                        //右クリック判定
                        m_isPressed = Mouse.current.rightButton.wasPressedThisFrame;
                        break;
                    case "Skill":
                        //右クリック判定
                        m_isPressed = Mouse.current.rightButton.wasPressedThisFrame;
                        break;
                }
            }

            //ボタンが押されたら、
            if (m_isPressed)
            {
                //選択をキャンセル
                Cancel();
            }
        }

        //選択キャンセル処理
        void Cancel()
        {
            switch (m_tankOrSkill)
            {
                case "Tank":
                    //保存されていたタンクをキャンセル
                    m_saveData.SetSelectTankName(m_playerNum, null);
                    break;
                case "Skill":
                    //保存されていたスキルをキャンセル
                    m_saveData.SetSelectSkillName(m_playerNum, null);
                    break;
            }
            //UI表示を非表示にする
            gameObject.SetActive(false);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}