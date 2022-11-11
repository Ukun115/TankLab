using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// �`�������W���[�h�̍ۂ̕\���f�[�^�̍X�V����
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

            //�`�������W���[�h��i�߂Ă���Œ���Back�{�^���Ń^�C�g���ɖ߂�Ȃ��悤�ɂ��Ă���
            if(m_saveData.GetSetSelectStageNum != 1)
            {
                m_backButtonText.SetActive(false);
                m_backButtonObject.SetActive(false);

                //GO�{�^���̈ʒu����ʉ������ɏC��
                m_goButtonText.anchoredPosition = new Vector3(0.0f, m_goButtonText.anchoredPosition.y);
                m_goButtonObject.transform.position = new Vector3(5.6f, m_goButtonObject.transform.position.y, m_goButtonObject.transform.position.z);
            }

            //�X�e�[�W�ɂ���ă^���N�ƃX�L���̎�ޕύX
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
    /// �X�e�[�W���Ƃ̏ڍׂȃf�[�^
    /// </summary>
    [System.Serializable]
    public class StageData
    {
        //�X�e�[�W���Ƃ̓GAI�̐�
        [SerializeField] int m_numberOfEnemiesOnStage = 0;
        //�X�e�[�W���Ƃ̃^���N�̎��
        [SerializeField] string m_tankType = "";
        //�X�e�[�W���Ƃ̃X�L���̎��
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