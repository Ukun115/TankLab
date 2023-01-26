using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Text.RegularExpressions;

/// <summary>
/// �^���N�����肷�鏈��
/// </summary>
namespace nsTankLab
{
    public class DecideTank : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("�v���C���[�{�[�h")] GameObject[] m_playerBoard = null;
        [SerializeField, TooltipAttribute("�v���C���[���e�L�X�g")] TextMeshProUGUI m_playerNameText = null;
        [SerializeField, TooltipAttribute("�Q�[���p�b�h���쎞�̃J�[�\���摜�I�u�W�F�N�g")] GameObject[] m_playerCursor = null;
        [SerializeField, TooltipAttribute("�^���N�I�������摜�I�u�W�F�N�g")] GameObject[] m_playerTankAlreadyDecide = null;
        [SerializeField, TooltipAttribute("�X�L���I�������摜�I�u�W�F�N�g")] GameObject[] m_playerSkillAlreadyDecide = null;
        [SerializeField, TooltipAttribute("�^���Nor�X�L�����e�L�X�g")] TextMeshProUGUI[] m_tankSkillNameText = null;
        [SerializeField, TooltipAttribute("�^���Nor�X�L���������e�L�X�g")] TextMeshProUGUI[] m_tankSkillInfoText = null;
        [SerializeField, TooltipAttribute("�ǂލ��ރX�L�������e�L�X�g���������܂�Ă���.txt�t�@�C��")] TextAsset[] m_skillTextAsset = { null };

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //�I�����Ă���^���N�ƃX�L�������Z�b�g
            m_saveData.InitSelectTankAndSkill();

            //���[�J���}�b�`�̏ꍇ
            if (m_saveData.GetSetSelectGameMode == "LOCALMATCH")
            {
                //2�`4P�̃v���C���[UI(�J�[�\����)��\������B
                DisplayPlayerUi(true);
            }
            else
            {
                //2�`4P�̃v���C���[UI(�J�[�\����)��\�����Ȃ��B
                DisplayPlayerUi(false);

                //�v���C���[�������[�U�[���ɂ���B
                m_playerNameText.text = PlayerPrefs.GetString("PlayerName");

                // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h�ł̑���
                if (Gamepad.current is not null)
                {
                    m_playerCursor[0].SetActive(true);
                }
            }
        }

        public void UpdateTankSkillInfo(int playerNum, string character)
        {
            //�^�C�g���X�V
            m_tankSkillNameText[playerNum - 1].text = character;

            //�������X�V

            //�^���N
            if (character.Contains("TANK"))
            {
                //�I�����Ă���^���N�̃X�e�[�^�X
                TankStatus tankStatus = m_tankDataBase.GetTankLists()[int.Parse(Regex.Replace(character, @"[^1-4]", string.Empty)) - 1];
                m_tankSkillInfoText[playerNum - 1].text = $"Tank Speed : {tankStatus.GetTankSpeed()}\nFire Speed : {tankStatus.GetBulletSpeed()}\nRapid Fire : {tankStatus.GetRapidFireNum()}\nSame Time Fire : {tankStatus.GetSameTimeBulletNum()}\nBullet Refrection:{tankStatus.GetBulletRefrectionNum()}";
            }
            //�X�L��
            else if (character.Contains("SKILL"))
            {
                m_tankSkillInfoText[playerNum - 1].text = $"Info :\n{ m_skillTextAsset[int.Parse(Regex.Replace(character, @"[^0-9]", string.Empty)) - 1].text}";
            }
        }

        public void SetCharacter(int playerNum,string character)
        {
            //�^���N��������Ă����ꍇ
            if(character.Contains("TANK"))
            {
                //�I�������^���N��ۑ����Ă���
                m_saveData.SetSelectTankName(playerNum, character);
                //UI�\��
                m_playerTankAlreadyDecide[playerNum - 1].SetActive(true);
            }
            //�X�L����������Ă����ꍇ
            if(character.Contains("SKILL"))
            {
                //�I�������X�L����ۑ����Ă���
                m_saveData.SetSelectSkillName(playerNum, character);
                //UI�\��
                m_playerSkillAlreadyDecide[playerNum - 1].SetActive(true);
            }

            if (m_saveData.GetSetSelectGameMode == "LOCALMATCH")
            {
                //�S�����^���N���X�L�����I�����Ă�����A
                for (int player = 0; player < 4; player++)
                {
                    //�N����l�ł��I�����Ă��Ȃ������ꍇ�̓V�[���J�ڂ��Ȃ��B
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
                //2P�̕��ɂ��o�^���Ēu��
                //(�}�b�`���O���������Ƃ��Ƀv���C���[�ԍ����QP�ɂȂ鋰�ꂪ���邽�߁B)
                //�I�������^���N��ۑ����Ă���
                m_saveData.SetSelectTankName(2, m_saveData.GetSelectTankName(0));
                //�I�������X�L����ۑ����Ă���
                m_saveData.SetSelectSkillName(2, m_saveData.GetSelectSkillName(0));

                //�}�b�`���O�V�[���ɑJ��
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(SceneName.MatchingScene, true);
            }
            else
            {
                //�X�e�[�W�I���V�[���ɑJ��
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(SceneName.SelectStageScene, true);
            }
        }

        //�v���C���[UI�̕\����\������
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