using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    /// <summary>
    /// チャレンジモードの際の表示データの更新処理
    /// </summary>
    public class InitChallengeData : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_challengeNumText = null;
        [SerializeField] TextMeshProUGUI m_enemyNumText = null;
        [SerializeField] TextMeshProUGUI m_tankNumText = null;
        [SerializeField] TextMeshProUGUI m_skillNumText = null;
        [SerializeField] GameObject m_backButtonText = null;
        [SerializeField] GameObject m_backButtonObject = null;
        [SerializeField] RectTransform m_goButtonText = null;
        [SerializeField] Transform m_goButtonObjectTransform = null;
        [SerializeField] List<StageData> m_stageData = null;
        [SerializeField] TextMeshProUGUI m_playerNameText = null;
        [SerializeField, TooltipAttribute("タンク説明文テキスト")] TextMeshProUGUI m_tankInfoText = null;
        [SerializeField, TooltipAttribute("スキル説明文テキスト")] TextMeshProUGUI m_skillInfoText = null;
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("読む込むスキル説明テキストが書き込まれている.txtファイル")] TextAsset[] m_skillTextAsset = { null };

        SaveData m_saveData = null;

        const int PLAYER1_NUM = 1;
        const int MAX_PLAYER_HITPOINT = 3;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //名前をユーザー名にする
            m_playerNameText.text = PlayerPrefs.GetString("PlayerName");

            //チャレンジモードを進めている最中はBackボタンでタイトルに戻れないようにしておく
            if (m_saveData.GetSetSelectStageNum != 1 || m_saveData.GetSetHitPoint != MAX_PLAYER_HITPOINT)
            {
                //Backボタン削除
                m_backButtonText.SetActive(false);
                m_backButtonObject.SetActive(false);

                //GOボタンの位置を画面下中央に修正
                m_goButtonText.anchoredPosition = new Vector3(0.0f, m_goButtonText.anchoredPosition.y);
                m_goButtonObjectTransform.position = new Vector3(5.6f, m_goButtonObjectTransform.position.y, m_goButtonObjectTransform.position.z);
            }

            //ステージによってタンクとスキルの種類変更
            m_saveData.SetSelectTankName(PLAYER1_NUM, m_stageData[m_saveData.GetSetSelectStageNum - 1].GetTankType());
            m_saveData.SetSelectSkillName(PLAYER1_NUM, m_stageData[m_saveData.GetSetSelectStageNum - 1].GetSkillType());

            m_challengeNumText.text = $"{m_saveData.GetSetSelectStageNum}";
            m_enemyNumText.text = $"x{m_stageData[m_saveData.GetSetSelectStageNum - 1].GetEnemiesNum()}";

            //タンク名更新
            Debug.Log(m_saveData.GetSelectTankName(PLAYER1_NUM - 1));
            switch (m_saveData.GetSelectTankName(PLAYER1_NUM - 1))
            {
                case "Tank1":
                    m_tankNumText.text = "AR";
                    break;
                case "Tank2":
                    m_tankNumText.text = "SMG";
                    break;
                case "Tank3":
                    m_tankNumText.text = "SG";
                    break;
                case "Tank4":
                    m_tankNumText.text = "LMG";
                    break;
            }
            //スキル名更新
            Debug.Log(m_saveData.GetSelectSkillName(PLAYER1_NUM - 1));
            switch (m_saveData.GetSelectSkillName(PLAYER1_NUM - 1))
            {
                case "Skill1":
                    m_skillNumText.text = "JET";
                    break;
                case "Skill2":
                    m_skillNumText.text = "WARP";
                    break;
                case "Skill3":
                    m_skillNumText.text = "BOMB";
                    break;
                case "Skill4":
                    m_skillNumText.text = "F-SHIELD";
                    break;
                case "Skill5":
                    m_skillNumText.text = "B-SHIELD";
                    break;
                case "Skill6":
                    m_skillNumText.text = "ROCKET";
                    break;
            }

            //選択しているタンクのステータス
            TankStatus tankStatus = m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(PLAYER1_NUM-1)];
            //タンク説明文テキスト更新
            m_tankInfoText.text = $"Tank Speed : {tankStatus.GetTankSpeed()}\nFire Speed : {tankStatus.GetBulletSpeed()}\nRapid Fire : {tankStatus.GetRapidFireNum()}\nSame Time Fire : {tankStatus.GetSameTimeBulletNum()}\nBullet Refrection:{tankStatus.GetBulletRefrectionNum()}";
            //スキル説明文テキスト更新
            m_skillInfoText.text = $"Info :\n{ m_skillTextAsset[int.Parse(Regex.Replace(m_stageData[m_saveData.GetSetSelectStageNum - 1].GetSkillType(), @"[^0-9]", string.Empty))-1].text}";
        }
    }

    /// <summary>
    /// ステージごとの詳細なデータ
    /// </summary>
    [System.Serializable]
    public class StageData
    {
        //ステージごとの敵AIの数
        [SerializeField] int m_numberOfEnemiesOnStage = 0;
        //ステージごとのタンクの種類
        [SerializeField] string m_tankType = string.Empty;
        //ステージごとのスキルの種類
        [SerializeField] string m_skillType = string.Empty;

        public int GetEnemiesNum()
        {
            return m_numberOfEnemiesOnStage;
        }

        public string GetTankType()
        {
            return m_tankType;
        }

        public string GetSkillType()
        {
            return m_skillType;
        }
    }
}