using UnityEngine;
using TMPro;

/// <summary>
/// �^���N�����肷�鏈��
/// </summary>
public class DecideTank : MonoBehaviour
{
    SaveData m_saveData = null;
    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("�v���C���[�{�[�h")] GameObject[] m_playerBoard = null;
    [SerializeField, TooltipAttribute("�v���C���[���e�L�X�g")] TextMeshProUGUI m_playerNameText = null;
    [SerializeField, TooltipAttribute("�Q�[���p�b�h���쎞�̃J�[�\���摜�I�u�W�F�N�g")] GameObject[] m_playerCursor = null;
    [SerializeField, TooltipAttribute("�^���N�I�������摜�I�u�W�F�N�g")] GameObject[] m_playerTankAlreadyDecide = null;
    [SerializeField, TooltipAttribute("�X�L���I�������摜�I�u�W�F�N�g")] GameObject[] m_playerSkillAlreadyDecide = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

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
            if (m_controllerData.GetIsConnectedController(1))
            {
                m_playerCursor[0].SetActive(true);
            }
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

        //�X�e�[�W�I���V�[���ɑJ��
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectStageScene");
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