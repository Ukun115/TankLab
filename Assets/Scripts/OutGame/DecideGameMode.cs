using UnityEngine;

/// <summary>
/// �Q�[�����[�h�����肷�鏈��
/// </summary>
namespace nsTankLab
{
public class DecideGameMode : MonoBehaviour
{
    //�V�[���X�C�b�`���[�X�N���v�g
    SceneSwitcher m_sceneSwitcher = null;

    //�Z�[�u�f�[�^
    SaveData m_saveData = null;

    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("�x�����b�Z�[�W�\�������X�N���v�g")] WarningTextDisplay m_warningTextDisplay = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
    }

    //�����ꂽ�{�^���̎�ނɂ���ď����𕪊�
    public void SetCharacter(string character)
    {
        var netWorkState = Application.internetReachability;

        //�I�����ꂽ�Q�[�����[�h��ۑ�
        m_saveData.GetSetSelectGameMode = character;

        switch (character)
        {
            //�`�������W���[�h�̏ꍇ�A
            case "CHALLENGE":
                //�`�������W�Q�[���V�[���ɑJ��
                ChangeScene("ChallengeGameScene");

                break;

            //���[�J���}�b�`���[�h�̏ꍇ�A
            case "LOCALMATCH":
                //�q����Ă���R���g���[���[�̐������肽��A
                if (m_controllerData.GetConnectedControllerNum() >= 2)
                {
                    //�^���N�V�[���ɑJ��
                    m_sceneSwitcher.StartTransition("SelectTankScene");
                }
                //�q����Ă���R���g���[���[�̐�������Ȃ�������A
                else
                {
                    Debug.Log($"<color=yellow>�s���R���g���[���[��:{4- m_controllerData.GetConnectedControllerNum()}</color>");

                    //�x�����b�Z�[�W����ʕ\��
                    m_warningTextDisplay.Display("Not enough "+"\n"+"controllers connected."+"\n"+"Required number: 4");
                }

                break;

            //�����_���}�b�`�̏ꍇ�A
            case "RANDOMMATCH":
                //�C���^�[�l�b�g�ɐڑ����Ă��Ȃ��ꍇ
                if (netWorkState == NetworkReachability.NotReachable)
                {
                    //�C���^�[�l�b�g�ʐM�̂��߁A�V�[���J�ڂ��Ȃ��悤�ɂ���B
                    //�x�����b�Z�[�W����ʕ\��
                    Debug.Log("�C���^�[�l�b�g�ɐڑ����Ă��������B");
                    m_warningTextDisplay.Display("Please connect to"+"\n"+"the internet.");

                }
                //�C���^�[�l�b�g�ɐڑ�����Ă���ꍇ
                else
                {
                    //�^���N�I���V�[���ɑJ��
                    ChangeScene("SelectTankScene");
                }

                break;

            //�v���C�x�[�g�}�b�`�̏ꍇ�A
            case "PRIVATEMATCH":
                //�C���^�[�l�b�g�ɐڑ����Ă��Ȃ��ꍇ
                if (netWorkState == NetworkReachability.NotReachable)
                {
                    //�C���^�[�l�b�g�ʐM�̂��߁A�V�[���J�ڂ��Ȃ��悤�ɂ���B
                    //�x�����b�Z�[�W����ʕ\��
                    Debug.Log("�C���^�[�l�b�g�ɐڑ����Ă��������B");
                    m_warningTextDisplay.Display("Please connect to " + "\n" + "the internet.");

                }
                //�C���^�[�l�b�g�ɐڑ�����Ă���ꍇ
                else
                {
                    //�p�X���[�h���̓V�[���ɑJ��
                    ChangeScene("InputPasswordScene");
                }

                break;

            //�ݒ�̏ꍇ�A
            case "SETTING":


                break;

            //�I���̏ꍇ�A
            case "EXIT":

                Debug.Log("�Q�[���I��");

                //�Q�[���I��
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

                break;

            //�v���C���[���O�̏ꍇ�A
            case "PLAYERNAME":
                //���[�U�[���o�^�V�[���ɑJ��
                ChangeScene("DecideNameScene");
                break;
        }
    }

    //�V�[���J�ڏ���
    void ChangeScene(string nextSceneName)
    {
        //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            //�v���C���[���o�^�V�[��������
            m_sceneSwitcher.StartTransition("DecideNameScene");
        }
        //�ʏ�J��
        else
        {
            //���̃V�[���ɑJ��
            m_sceneSwitcher.StartTransition(nextSceneName);
        }
    }
}
}