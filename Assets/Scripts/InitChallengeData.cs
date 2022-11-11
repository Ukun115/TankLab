using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// チャレンジモードの際の表示データの更新処理
    /// </summary>
    public class InitChallengeData : MonoBehaviour
    {
        [SerializeField]TextMeshProUGUI m_challengeNumText = null;
        [SerializeField]TextMeshProUGUI m_enemyNumText = null;
        [SerializeField]TextMeshProUGUI m_heartNumText = null;
        [SerializeField]TextMeshProUGUI m_tankNumText = null;
        [SerializeField]TextMeshProUGUI m_skillNumText = null;

        [SerializeField] GameObject m_backButtonText = null;
        [SerializeField] GameObject m_backButtonObject = null;
        [SerializeField]  RectTransform m_goButtonText = null;
        [SerializeField] GameObject m_goButtonObject = null;

        SaveData m_saveData = null;

        [SerializeField] List<StageData> m_stageData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //チャレンジモードを進めている最中はBackボタンでタイトルに戻れないようにしておく
            if(m_saveData.GetSetSelectStageNum != 1)
            {
                m_backButtonText.SetActive(false);
                m_backButtonObject.SetActive(false);

                //GOボタンの位置を画面下中央に修正
                m_goButtonText.anchoredPosition = new Vector3(0.0f, m_goButtonText.anchoredPosition.y);
                m_goButtonObject.transform.position = new Vector3(5.6f, m_goButtonObject.transform.position.y, m_goButtonObject.transform.position.z);
            }

            //ステージによってタンクとスキルの種類変更
            m_saveData.SetSelectTankName(1, m_stageData[m_saveData.GetSetSelectStageNum - 1].GetTankType());
            m_saveData.SetSelectSkillName(1, m_stageData[m_saveData.GetSetSelectStageNum - 1].GetSkillType());

            m_challengeNumText.text = $"{m_saveData.GetSetSelectStageNum}";
            m_enemyNumText.text = $"{m_stageData[m_saveData.GetSetSelectStageNum - 1].GetEnemiesNum()}";
            m_heartNumText.text = "";
            m_tankNumText.text = m_saveData.GetSelectTankName(0);
            m_skillNumText.text = m_saveData.GetSelectSkillName(0);
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
        [SerializeField] string m_tankType = "";
        //ステージごとのスキルの種類
        [SerializeField] string m_skillType = "";

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