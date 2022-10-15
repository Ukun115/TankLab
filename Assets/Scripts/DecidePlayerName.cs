using UnityEngine;
using TMPro;

/// <summary>
/// �v���C���[�������肷�鏈��
/// </summary>
public class DecidePlayerName : MonoBehaviour
{
    //�v���C���[�����i�[����TMPro
    [SerializeField] TextMeshProUGUI m_playerNameText = null;
    //�v���C���[���ɂł���ő啶����
    [SerializeField] int m_maxCharacterNum = 0;

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
                        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("ChallengeGameScene");
                        break;

                        //�����_���}�b�`
                    case "RANDOMMATCH":
                        //�^���N�I���V�[���ɑJ��
                        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                        break;

                        //�v���C�x�[�g
                    case "PRIVATEMATCH":
                        //�p�X���[�h���͉�ʂɑJ��
                        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("InputPasswordScene");
                        break;
                }
                break;

            //��L�ȊO�̃{�^���i�A���t�@�x�b�gA�`Z�j
            default:
                //��������ȏゾ�����甽�f�����Ȃ�
                if(m_playerNameText.text.Length > m_maxCharacterNum-1)
                {
                    return;
                }

                m_playerNameText.text += character;
                break;
        }
    }
}
