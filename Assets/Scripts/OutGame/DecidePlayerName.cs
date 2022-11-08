using UnityEngine;
using TMPro;

/// <summary>
/// �v���C���[�������肷�鏈��
/// </summary>
namespace nsTankLab
{
public class DecidePlayerName : MonoBehaviour
{
    [SerializeField, TooltipAttribute("�v���C���[��TMPro")] TextMeshProUGUI m_playerNameText = null;
    //�v���C���[���ɂł���ő啶����
    const int MAX_CHARACTER_NUM = 8;

    //�Z�[�u�f�[�^
    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    //�����ꂽ�{�^���̎�ނɂ���ď����𕪊�
    public void SetCharacter(string character)
    {
        switch (character)
        {
            //�߂�{�^��
            case "BACK":
                //�������͂���Ă��Ȃ������疖�����폜���Ȃ�
                if (m_playerNameText.text.Length == 0)
                {
                    return;
                }
                //���O�̖������폜����B
                m_playerNameText.text = m_playerNameText.text[..^1];
                break;

            //OK�{�^��
            case "OK":
                //�������͂���Ă��Ȃ�������ok�����Ȃ�
                if (m_playerNameText.text.Length == 0)
                {
                    return;
                }

                //�o�^���ꂽ���O���Z�[�u
                PlayerPrefs.SetString("PlayerName", m_playerNameText.text);
                PlayerPrefs.Save();

                switch (m_saveData.GetSetSelectGameMode)
                {
                    //�`�������W���[�h
                    case "CHALLENGE":
                        //�`�������W�Q�[���ɑJ��
                        ChangeScene("ChallengeGameScene");
                        break;

                        //�����_���}�b�`
                    case "RANDOMMATCH":
                        //�^���N�I���V�[���ɑJ��
                        ChangeScene("SelectTankScene");
                        break;

                        //�v���C�x�[�g
                    case "PRIVATEMATCH":
                        //�p�X���[�h���͉�ʂɑJ��
                        ChangeScene("InputPasswordScene");
                        break;

                        //�v���C���[�l�[������
                    case "PLAYERNAME":
                        //�^�C�g����ʂɑJ��
                        ChangeScene("TitleScene");
                        break;
                }
                break;

            //��L�ȊO�̃{�^���i�A���t�@�x�b�gA�`Z�j
            default:
                //��������ȏゾ�����甽�f�����Ȃ�
                if(m_playerNameText.text.Length > MAX_CHARACTER_NUM-1)
                {
                    return;
                }

                m_playerNameText.text += character;
                break;
        }
    }

    //�V�[���J�ڂ��鏈��
    void ChangeScene(string nextScene)
    {
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(nextScene);
    }
}
}