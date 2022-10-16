using UnityEngine;

/// <summary>
/// �Q�[�����[�h�����肷�鏈��
/// </summary>
public class DecideGameMode : MonoBehaviour
{
    //�V�[���X�C�b�`���[�X�N���v�g
    SceneSwitcher m_sceneSwitcher = null;

    //�Z�[�u�f�[�^
    SaveData m_saveData = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    //�����ꂽ�{�^���̎�ނɂ���ď����𕪊�
    public void SetCharacter(string character)
    {
        m_saveData.GetSetSelectGameMode = character;

        switch (character)
        {
            //�`�������W���[�h�̏ꍇ�A
            case "CHALLENGE":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //�ʏ�J��
                else
                {
                    //�`�������W�Q�[���ɑJ��
                    m_sceneSwitcher.StartTransition("ChallengeGameScene");
                }

                break;

            //���[�J���}�b�`���[�h�̏ꍇ�A
            case "LOCALMATCH":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //�ʏ�J��
                else
                {
                    //�Q�[���p�b�h��o�^����V�[���ɑJ��
                    m_sceneSwitcher.StartTransition("GamePadRegister");
                }

                break;

            //�����_���}�b�`�̏ꍇ�A
            case "RANDOMMATCH":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //�ʏ�J��
                else
                {
                    //�^���N�V�[���ɑJ��
                    m_sceneSwitcher.StartTransition("SelectTankScene");
                }

                break;

            //�v���C�x�[�g�}�b�`�̏ꍇ�A
            case "PRIVATEMATCH":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    m_sceneSwitcher.StartTransition("DecideNameScene");
                }
                //�ʏ�J��
                else
                {
                    //�p�X���[�h���͉�ʂɑJ��
                    m_sceneSwitcher.StartTransition("InputPasswordScene");
                }

                break;

            //�ݒ�̏ꍇ�A
            case "SETTING":


                break;

            //�I���̏ꍇ�A
            case "EXIT":
                //�Q�[���I��
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

                break;
        }
    }
}
