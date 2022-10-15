using UnityEngine;

/// <summary>
/// �X�e�[�W�����肷�鏈��
/// </summary>
public class DecideStage : MonoBehaviour
{
    //�X�e�[�W���̍��ׂɕ\������J�[�\���I�u�W�F�N�g
    [SerializeField] GameObject m_cursorObject = null;
    //�J�[�\���I�u�W�F�N�g�̈ʒu
    [SerializeField] GameObject[] m_cursorPosition = null;

    public void SetCursorPosition(string character)
    {
        switch (character)
        {
            //�X�e�[�W1
            case "STAGE1":
                //�J�[�\���ړ�
                m_cursorObject.transform.position = m_cursorPosition[0].transform.position;
                break;

            //�X�e�[�W2
            case "STAGE2":
                //�J�[�\���ړ�
                m_cursorObject.transform.position = m_cursorPosition[1].transform.position;
                break;
        }
    }

    public void SetCharacter(string character)
    {
        //�I�������X�e�[�W��ۑ����Ă���
        GameObject.Find("SaveData").GetComponent<SaveData>().GetSetSelectStageName = character;

        //�}�b�`���O�ɑJ��
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("MatchingScene");
    }
}