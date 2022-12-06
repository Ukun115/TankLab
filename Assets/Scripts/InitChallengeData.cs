using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    /// <summary>
    /// �`�������W���[�h�̍ۂ̕\���f�[�^�̍X�V����
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
        [SerializeField, TooltipAttribute("�^���N�������e�L�X�g")] TextMeshProUGUI m_tankInfoText = null;
        [SerializeField, TooltipAttribute("�X�L���������e�L�X�g")] TextMeshProUGUI m_skillInfoText = null;
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("�ǂލ��ރX�L�������e�L�X�g���������܂�Ă���.txt�t�@�C��")] TextAsset[] m_skillTextAsset = { null };

        SaveData m_saveData = null;

        const int PLAYER1_NUM = 1;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //���O�����[�U�[���ɂ���
            m_playerNameText.text = PlayerPrefs.GetString("PlayerName");

            //�`�������W���[�h��i�߂Ă���Œ���Back�{�^���Ń^�C�g���ɖ߂�Ȃ��悤�ɂ��Ă���
            if (m_saveData.GetSetSelectStageNum != 1 || m_saveData.GetSetHitPoint != 3)
            {
                //Back�{�^���폜
                m_backButtonText.SetActive(false);
                m_backButtonObject.SetActive(false);

                //GO�{�^���̈ʒu����ʉ������ɏC��
                m_goButtonText.anchoredPosition = new Vector3(0.0f, m_goButtonText.anchoredPosition.y);
                m_goButtonObjectTransform.position = new Vector3(5.6f, m_goButtonObjectTransform.position.y, m_goButtonObjectTransform.position.z);
            }

            //�X�e�[�W�ɂ���ă^���N�ƃX�L���̎�ޕύX
            m_saveData.SetSelectTankName(PLAYER1_NUM, m_stageData[m_saveData.GetSetSelectStageNum - 1].GetTankType());
            m_saveData.SetSelectSkillName(PLAYER1_NUM, m_stageData[m_saveData.GetSetSelectStageNum - 1].GetSkillType());

            m_challengeNumText.text = $"{m_saveData.GetSetSelectStageNum}";
            m_enemyNumText.text = $"x{m_stageData[m_saveData.GetSetSelectStageNum - 1].GetEnemiesNum()}";
            m_tankNumText.text = m_saveData.GetSelectTankName(PLAYER1_NUM-1);
            m_skillNumText.text = m_saveData.GetSelectSkillName(PLAYER1_NUM-1);

            //�I�����Ă���^���N�̃X�e�[�^�X
            TankStatus tankStatus = m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(PLAYER1_NUM-1)];
            //�^���N�������e�L�X�g�X�V
            m_tankInfoText.text = $"Tank Speed : {tankStatus.GetTankSpeed()}\nFire Speed : {tankStatus.GetBulletSpeed()}\nRapid Fire : {tankStatus.GetRapidFireNum()}\nSame Time Fire : {tankStatus.GetSameTimeBulletNum()}\nBullet Refrection:{tankStatus.GetBulletRefrectionNum()}";
            //�X�L���������e�L�X�g�X�V
            m_skillInfoText.text = $"Info :\n{ m_skillTextAsset[int.Parse(Regex.Replace(m_stageData[m_saveData.GetSetSelectStageNum - 1].GetSkillType(), @"[^0-9]", string.Empty))-1].text}";
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
        [SerializeField] string m_tankType = string.Empty;
        //�X�e�[�W���Ƃ̃X�L���̎��
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