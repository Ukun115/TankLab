using UnityEngine;
using TMPro;

/// <summary>
/// タンクを決定する処理
/// </summary>
public class DecideTank : MonoBehaviour
{
    SaveData m_saveData = null;
    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("プレイヤーボード")] GameObject[] m_playerBoard = null;
    [SerializeField, TooltipAttribute("プレイヤー名テキスト")] TextMeshProUGUI m_playerNameText = null;
    [SerializeField, TooltipAttribute("ゲームパッド操作時のカーソル画像オブジェクト")] GameObject[] m_playerCursor = null;
    [SerializeField, TooltipAttribute("タンク選択完了画像オブジェクト")] GameObject[] m_playerTankAlreadyDecide = null;
    [SerializeField, TooltipAttribute("スキル選択完了画像オブジェクト")] GameObject[] m_playerSkillAlreadyDecide = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

        //ローカルマッチの場合
        if (m_saveData.GetSetSelectGameMode == "LOCALMATCH")
        {
            //2～4PのプレイヤーUI(カーソル等)を表示する。
            DisplayPlayerUi(true);
        }
        else
        {
            //2～4PのプレイヤーUI(カーソル等)を表示しない。
            DisplayPlayerUi(false);

            //プレイヤー名をユーザー名にする。
            m_playerNameText.text = PlayerPrefs.GetString("PlayerName");

            // ゲームパッドが接続されていたらゲームパッドでの操作
            if (m_controllerData.GetIsConnectedController(1))
            {
                m_playerCursor[0].SetActive(true);
            }
        }
    }

    public void SetCharacter(int playerNum,string character)
    {
        //タンクが押されていた場合
        if(character.Contains("TANK"))
        {
            //選択したタンクを保存しておく
            m_saveData.SetSelectTankName(playerNum, character);
            //UI表示
            m_playerTankAlreadyDecide[playerNum - 1].SetActive(true);
        }
        //スキルが押されていた場合
        if(character.Contains("SKILL"))
        {
            //選択したスキルを保存しておく
            m_saveData.SetSelectSkillName(playerNum, character);
            //UI表示
            m_playerSkillAlreadyDecide[playerNum - 1].SetActive(true);
        }

        if (m_saveData.GetSetSelectGameMode == "LOCALMATCH")
        {
            //全員がタンクもスキルも選択していたら、
            for (int player = 0; player < 4; player++)
            {
                //誰か一人でも選択していなかった場合はシーン遷移しない。
                if (m_saveData.GetSelectTankName(player) is null)
                {
                    return;
                }
                if (m_saveData.GetSelectSkillName(player) is null)
                {
                    return;
                }
            }
        }
        else
        {
            if (m_saveData.GetSelectTankName(0) is null)
            {
                return;
            }
            if (m_saveData.GetSelectSkillName(0) is null)
            {
                return;
            }
        }

        //ステージ選択シーンに遷移
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectStageScene");
    }

    //プレイヤーUIの表示非表示処理
    void DisplayPlayerUi(bool doDisplay)
    {
        for (int playerNum = 1; playerNum < 4; playerNum++)
        {
            m_playerBoard[playerNum].SetActive(doDisplay);
            m_playerCursor[playerNum].SetActive(doDisplay);
        }
    }
}