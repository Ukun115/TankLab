using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;

/// <summary>
/// ���[�U�[�̃Z�[�u�f�[�^���Ǘ�����X�N���v�g
/// </summary>
namespace nsTankLab
{
    public class SaveData : MonoBehaviour
    {
        [SerializeField] SoundManager m_soundManager = null;

        //�I�����C�����[�h���ǂ���
        bool m_isOnline = false;

        //�I�����ꂽ�Q�[�����[�h
        string m_selectGameMode = string.Empty;

        //�v���C���[�ԍ�
        int m_playerNum = 0;

        //���݂̃X�e�[�W�ԍ�
        int m_nowStageNum = 1;
        //���v�X�e�[�W��
        const int TOTAL_STAGE_NUM = 10;

        //���݂̗̑�(�`�������W���[�h�݂̂Ŏg�p)
        int m_hitPoint = 3;

        //�X�^�[
        bool[,] m_star = new bool[4,2];

        //�I���^���N��
        string[] m_selectTankName = new string[4];
        //�I���^���N�ԍ�
        int[] m_selectTankNum = new int[4];
        //�I���X�L����
        string[] m_selectSkillName = new string[4];
        //�I���X�L���ԍ�
        int[] m_selectSkillNum = new int[4];
        //���͂��ꂽ�p�X���[�h
        string m_inputPassword = "----";

        //�Q�[���̐i�ގ��Ԃ��A�N�e�B�u���ǂ���
        bool m_activeGameTime = true;

        public static GameObject m_instanceSaveData = null;

        void Awake()
        {
            CheckInstance();
        }

        void Start()
        {
            //60fps�ŌŒ肷��B
            Application.targetFrameRate = 60;

            //�X�^�[bool�ϐ��̏�����
            for(int playerNum = 0;playerNum < 4;playerNum++)
            {
                for (int starNum = 0; starNum < 2; starNum++)
                {
                    m_star[playerNum,starNum] = false;
                }
            }
        }

        //�Q�[���̐i�ގ��Ԃ��A�N�e�B�u���ǂ����v���p�e�B
        public bool GetSetmActiveGameTime
        {
            //�Q�b�^�[
            get { return m_activeGameTime; }
            //�Z�b�^�[
            set { m_activeGameTime = value; }
        }

        //�I�����ꂽ�Q�[�����[�h�v���p�e�B
        public string GetSetSelectGameMode
        {
            //�Q�b�^�[
            get { return m_selectGameMode; }
            //�Z�b�^�[
            set { m_selectGameMode = value; }
        }

        //�I�����ꂽ�X�e�[�W�ԍ��v���p�e�B
        public int GetSetSelectStageNum
        {
            //�Q�b�^�[
            get { return m_nowStageNum; }
            //�Z�b�^�[
            set { m_nowStageNum = value; }
        }

        //���v�X�e�[�W���Q�b�^�[
        public int GetTotalStageNum()
        {
            return TOTAL_STAGE_NUM;
        }

        //���̃X�e�[�W�ԍ��ɐݒ�
        public void NextStageNum()
        {
            m_nowStageNum++;
        }

        //�I�����ꂽ�^���N���Q�b�^�[
        public string GetSelectTankName(int playerNum)
        {
            return m_selectTankName[playerNum];
        }
        //�I�����ꂽ�^���N���Z�b�^�[
        public void SetSelectTankName(int playerNum, string tankNum)
        {
            m_selectTankName[playerNum-1] = $"Tank{(int.Parse(Regex.Replace(tankNum, @"[^0-9]", string.Empty)))}";

            //�I�����ꂽ�^���N�ԍ���ۑ�
            if (m_selectTankName[playerNum - 1] is not null)
            {
                m_selectTankNum[playerNum - 1] = int.Parse(Regex.Replace(tankNum, @"[^0-9]", string.Empty))-1;
            }
        }
        //�I�����ꂽ�X�L�����Q�b�^�[
        public string GetSelectSkillName(int playerNum)
        {
            return m_selectSkillName[playerNum];
        }
        //�I�����ꂽ�X�L�����Z�b�^�[
        public void SetSelectSkillName(int playerNum, string skillNum)
        {
            m_selectSkillName[playerNum - 1] = skillNum;

            //�I�����ꂽ�X�L���ԍ���ۑ�
            if (m_selectSkillName[playerNum - 1] is not null)
            {
                m_selectSkillNum[playerNum - 1] = int.Parse(Regex.Replace(skillNum, @"[^0-9]", string.Empty))-1;
            }
        }
        //�I�����ꂽ�^���N�ԍ��Q�b�^�[
        public int GetSelectTankNum(int playerNum)
        {
            return m_selectTankNum[playerNum];
        }
        //�I�����ꂽ�X�L���ԍ��Q�b�^�[
        public int GetSelectSkillNum(int playerNum)
        {
            return m_selectSkillNum[playerNum];
        }

        //�I�����C�����[�h�t���O�v���p�e�B
        public bool GetSetIsOnline
        {
            //�Q�b�^�[
            get { return m_isOnline; }
            //�Z�b�^�[
            set { m_isOnline = value; }
        }

        //���͂��ꂽ�p�X���[�h�v���p�e�B
        public string GetSetInputPassword
        {
            //�Q�b�^�[
            get { return m_inputPassword; }
            //�Z�b�^�[
            set { m_inputPassword = value; }
        }

        //�v���C���[�ԍ�
        public int GetSetPlayerNum
        {
            //�Q�b�^�[
            get { return m_playerNum; }
            //�Z�b�^�[
            set { m_playerNum = value; }
        }

        //�̗̓v���p�e�B
        public int GetSetHitPoint
        {
            //�Q�b�^�[
            get { return m_hitPoint; }
            //�Z�b�^�[
            set { m_hitPoint = value; }
        }

        //�X�^�[�Z�b�^�[
        public void ActiveStar(int playerNum,int starNum)
        {
            m_star[playerNum,starNum] = true;
        }
        //�X�^�[�Q�b�^�[
        public bool GetStar(int playerNum, int starNum)
        {
            return m_star[playerNum,starNum];
        }

        //�Z�[�u�f�[�^��������Ԃɖ߂�����
        public void SaveDataInit()
        {
            //�I�����C�����[�h���ǂ���
            m_isOnline = false;
            //�I�����ꂽ�Q�[�����[�h
            m_selectGameMode = string.Empty;
            //�v���C���[�ԍ�
            m_playerNum = 0;
            //���݂̃X�e�[�W�ԍ�
            m_nowStageNum = 1;
            //�I���^���N��
            m_selectTankName = new string[4];
            //�I���^���N�ԍ�
            m_selectTankNum = new int[4];
            //�I���X�L����
            m_selectSkillName = new string[4];
            //�I���X�L���ԍ�
            m_selectSkillNum = new int[4];
            //���͂��ꂽ�p�X���[�h
            m_inputPassword = "----";
            //�Q�[���̐i�ގ��Ԃ��A�N�e�B�u���ǂ���
            m_activeGameTime = false;
            //�̗͂�߂�
            m_hitPoint = 3;
            //�X�^�[bool�ϐ��̏�����
            for (int playerNum = 0; playerNum < 4; playerNum++)
            {
                for (int starNum = 0; starNum < 2; starNum++)
                {
                    m_star[playerNum,starNum] = false;
                }
            }

            //BGM��������
            m_soundManager.PlayBGM("OutGameSceneBGM");
        }

        public void InitSelectTankAndSkill()
        {
            //�I���^���N��
            m_selectTankName = new string[4];
            //�I���^���N�ԍ�
            m_selectTankNum = new int[4];
            //�I���X�L����
            m_selectSkillName = new string[4];
            //�I���X�L���ԍ�
            m_selectSkillNum = new int[4];
        }

        //�A�v���P�[�V�������I������O�ɌĂяo�����֐�
        //static�ϐ��̓A�v���P�[�V�������I������Ă����������ꂸ�A�����Ǝc�葱���邽�߁A�蓮�ŏ���������
        void OnApplicationQuit()
        {
            m_instanceSaveData = null;
        }

        //�V���O���g���p�^�[��
        //�C���X�^���X�`�F�b�N
        void CheckInstance()
        {
            if (m_instanceSaveData is null)
            {
                m_instanceSaveData = gameObject;

                //�V�[���J�ڂ��Ă����̃I�u�W�F�N�g�͔j�����ꂸ�ɕێ������܂܂ɂ���
                DontDestroyOnLoad(gameObject);

                //�f�o�b�N
                Debug.Log("SaveData�I�u�W�F�N�g�̃C���X�^���X�͖��o�^�ł��B�o�^���܂��B");

            }
            else
            {
                Destroy(gameObject);

                //�f�o�b�N
                Debug.Log("SaveData�I�u�W�F�N�g�̃C���X�^���X�͓o�^�ςł��B�폜���܂��B");
            }
        }
    }
}
//#error version
//��C#����o�[�W�����̊m�F