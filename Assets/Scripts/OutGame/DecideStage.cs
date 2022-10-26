using UnityEngine;

/// <summary>
/// �X�e�[�W�����肷�鏈��
/// </summary>
public class DecideStage : MonoBehaviour
{
    [SerializeField, TooltipAttribute("�X�e�[�W�I���J�[�\���I�u�W�F�N�g�̃g�����X�t�H�[��")] Transform m_cursorObject = null;

    [SerializeField, TooltipAttribute("�X�e�[�W�I���J�[�\���I�u�W�F�N�g�̈ʒu")] Transform[] m_cursorPosition = null;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    public void SetCursorPosition(string character)
    {
        //�J�[�\�����������Ă���X�e�[�W�ɂ���ăJ�[�\���I�u�W�F�N�g�̈ʒu��ύX
        m_cursorObject.position = character switch
        {
            "STAGE1" => m_cursorPosition[0].position,
            "STAGE2" => m_cursorPosition[1].position,
            _ => m_cursorObject.position
        };
    }

    public void SetCharacter(string character)
    {
        //�I�������X�e�[�W��ۑ����Ă���
        m_saveData.GetSetSelectStageName = character;

        switch (m_saveData.GetSetSelectGameMode)
        {
            //�I�����C���ΐ�̍ہA
            case "RANDOMMATCH":
            case "PRIVATEMATCH":
                //�}�b�`���O�ɑJ��
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("MatchingScene");

                break;

            //���[�J���ΐ�̍ہA
            case "LOCALMATCH":
                //���[�J���Q�[���ɑJ��
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("LocalGameScene");

                break;
        }
    }
}