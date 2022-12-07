using UnityEngine;

/// <summary>
/// �Q�[�����[�h�����肷�鏈��
/// </summary>
namespace nsTankLab
{
    public class DecideGameMode : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�x�����b�Z�[�W�\�������X�N���v�g")] WarningTextDisplay m_warningTextDisplay = null;

        //�V�[���X�C�b�`���[�X�N���v�g
        SceneSwitcher m_sceneSwitcher = null;

        //�Z�[�u�f�[�^
        SaveData m_saveData = null;

        ControllerData m_controllerData = null;

        bool m_notGood = false;

        //���[�J���}�b�`�̕K�v�v���C�l��
        int NEED_CONTROLLER_NUM = 4;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();
        }

        //�����ꂽ�{�^���̎�ނɂ���ď����𕪊�
        public void SetCharacter(string character)
        {
            m_notGood = false;

            var netWorkState = Application.internetReachability;

            //�I�����ꂽ�Q�[�����[�h��ۑ�
            m_saveData.GetSetSelectGameMode = character;

            switch (character)
            {
                //�`�������W���[�h�̏ꍇ�A
                case "CHALLENGE":
                    //���݂̃`�������W���J�E���g�V�[���ɑJ��
                    ChangeScene(SceneName.ChallengeNowNumCountScene);
                    break;

                //���[�J���}�b�`���[�h�̏ꍇ�A
                case "LOCALMATCH":
                    //�q����Ă���R���g���[���[�̐������肽��A
                    if (m_controllerData.GetConnectGamepad() >= NEED_CONTROLLER_NUM)
                    {
                        //�^���N�V�[���ɑJ��
                        m_sceneSwitcher.StartTransition(SceneName.SelectTankScene);
                    }
                    //�q����Ă���R���g���[���[�̐�������Ȃ�������A
                    else
                    {
                        Debug.Log($"<color=yellow>�s���R���g���[���[��:{NEED_CONTROLLER_NUM  - m_controllerData.GetConnectGamepad()}</color>");

                        //�x�����b�Z�[�W����ʕ\��
                        m_warningTextDisplay.Display($"Not enough \ncontrollers connected.\nRequired number: {NEED_CONTROLLER_NUM - m_controllerData.GetConnectGamepad()}");

                        m_notGood = true;
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
                        ChangeScene(SceneName.SelectTankScene);
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
                        //�p�X���[�h����V�[���ɑJ��
                        ChangeScene(SceneName.DecidePasswordScene);
                    }

                    break;

                //�ݒ�̏ꍇ�A
                case "CONFIG":
                    //�ݒ�V�[���ɑJ��
                    ChangeScene(SceneName.ConfigScene);

                    break;

                //�I���̏ꍇ�A
                case "EXIT":
                    //�Q�[���I��
                    Invoke(nameof(GameEnd), 0.5f);

                    break;

                //�v���C���[���O�̏ꍇ�A
                case "PLAYERNAME":
                    //���[�U�[���o�^�V�[���ɑJ��
                    ChangeScene(SceneName.DecideNameScene);
                    break;
            }
        }

        void GameEnd()
        {
            Debug.Log("�Q�[���I��");

            //�Q�[���I��
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        //�V�[���J�ڏ���
        void ChangeScene(string nextSceneName)
        {
            //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
            if (!PlayerPrefs.HasKey("PlayerName"))
            {
                //�v���C���[���o�^�V�[��������
                m_sceneSwitcher.StartTransition(SceneName.DecideNameScene, false);
            }
            //�ʏ�J��
            else
            {
                //���̃V�[���ɑJ��
                m_sceneSwitcher.StartTransition(nextSceneName);
            }
        }

        public bool GetNoGood()
        {
            return m_notGood;
        }

        void GetComponents()
        {
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}