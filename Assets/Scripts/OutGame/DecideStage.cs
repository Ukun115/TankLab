using UnityEngine;

/// <summary>
/// ステージを決定する処理
/// </summary>
namespace nsTankLab
{
    public class DecideStage : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("ステージ選択カーソルオブジェクトのトランスフォーム")] Transform m_cursorObject = null;
        [SerializeField, TooltipAttribute("ステージ選択カーソルオブジェクトの位置")] Transform[] m_cursorPosition = null;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        public void SetCursorPosition(string character)
        {
            //カーソルが当たっているステージによってカーソルオブジェクトの位置を変更
            m_cursorObject.position = character switch
            {
                "STAGE1" => m_cursorPosition[0].position,
                "STAGE2" => m_cursorPosition[1].position,
                "STAGE3" => m_cursorPosition[2].position,
                "STAGE4" => m_cursorPosition[3].position,
                "STAGE5" => m_cursorPosition[4].position,
                "STAGE6" => m_cursorPosition[5].position,
                _ => m_cursorObject.position
            };
        }

        public void SetStageNum(int stageNum)
        {
            //選択したステージを保存しておく
            m_saveData.GetSetSelectStageNum = stageNum;

            //デバック
            Debug.Log($"<color=yellow>ステージ:Stage{m_saveData.GetSetSelectStageNum}</color>");

            switch (m_saveData.GetSetSelectGameMode)
            {
                //オンライン対戦の際、
                case "RANDOMMATCH":
                case "PRIVATEMATCH":
                    //マッチングに遷移
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(SceneName.MatchingScene, true);

                    break;

                //ローカル対戦の際、
                case "LOCALMATCH":
                    //ローカルゲームに遷移
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(SceneName.LocalGameScene, true);

                    break;
            }
        }
    }
}