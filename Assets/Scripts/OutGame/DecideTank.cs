using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Text.RegularExpressions;

/// <summary>
/// タンクを決定する処理
/// </summary>
namespace nsTankLab
{
    public class DecideTank : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("プレイヤーボード")] GameObject[] m_playerBoard = null;
        [SerializeField, TooltipAttribute("プレイヤー名テキスト")] TextMeshProUGUI m_playerNameText = null;
        [SerializeField, TooltipAttribute("ゲームパッド操作時のカーソル画像オブジェクト")] GameObject[] m_playerCursor = null;
        [SerializeField, TooltipAttribute("タンク選択完了画像オブジェクト")] GameObject[] m_playerTankAlreadyDecide = null;
        [SerializeField, TooltipAttribute("スキル選択完了画像オブジェクト")] GameObject[] m_playerSkillAlreadyDecide = null;
        [SerializeField, TooltipAttribute("タンクorスキル名テキスト")] TextMeshProUGUI[] m_tankSkillNameText = null;
        [SerializeField, TooltipAttribute("タンクorスキル説明文テキスト")] TextMeshProUGUI[] m_tankSkillInfoText = null;
        [SerializeField, TooltipAttribute("読む込むスキル説明テキストが書き込まれている.txtファイル")] TextAsset[] m_skillTextAsset = { null };

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //選択しているタンクとスキルをリセット
            m_saveData.InitSelectTankAndSkill();

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
                if (Gamepad.current is not null)
                {
                    m_playerCursor[0].SetActive(true);
                }
            }
        }

        public void UpdateTankSkillInfo(int playerNum, string character)
        {
            //タイトル更新
            m_tankSkillNameText[playerNum - 1].text = character;

            //説明文更新

            //タンク
            if (character.Contains("TANK"))
            {
                //選択しているタンクのステータス
                TankStatus tankStatus = m_tankDataBase.GetTankLists()[int.Parse(Regex.Replace(character, @"[^1-4]", string.Empty)) - 1];
                m_tankSkillInfoText[playerNum - 1].text = $"Tank Speed : {tankStatus.GetTankSpeed()}\nFire Speed : {tankStatus.GetBulletSpeed()}\nRapid Fire : {tankStatus.GetRapidFireNum()}\nSame Time Fire : {tankStatus.GetSameTimeBulletNum()}\nBullet Refrection:{tankStatus.GetBulletRefrectionNum()}";
            }
            //スキル
            else if (character.Contains("SKILL"))
            {
                m_tankSkillInfoText[playerNum - 1].text = $"Info :\n{ m_skillTextAsset[int.Parse(Regex.Replace(character, @"[^0-9]", string.Empty)) - 1].text}";
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

            if (m_saveData.GetSetSelectGameMode == "RANDOMMATCH" || m_saveData.GetSetSelectGameMode == "PRIVATEMATCH")
            {
                //2Pの方にも登録して置く
                //(マッチング完了したときにプレイヤー番号が２Pになる恐れがあるため。)
                //選択したタンクを保存しておく
                m_saveData.SetSelectTankName(2, m_saveData.GetSelectTankName(0));
                //選択したスキルを保存しておく
                m_saveData.SetSelectSkillName(2, m_saveData.GetSelectSkillName(0));

                //マッチングシーンに遷移
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(SceneName.MatchingScene, true);
            }
            else
            {
                //ステージ選択シーンに遷移
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(SceneName.SelectStageScene, true);
            }
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
}