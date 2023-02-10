using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using Photon.Pun;

/// <summary>
/// �����𖞂����ƃ��U���g��ʂɍs������
/// </summary>
namespace nsTankLab
{
    public class GoResult : MonoBehaviourPun
    {
        [SerializeField]ResultInit m_resultInitScript = null;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        SceneSwitcher m_sceneSwitcher = null;

        bool m_canGoResult = true;

        string m_winPlayerName = string.Empty;

        void Start()
        {
            //�}�b�`���O�V�[���̏ꍇ�͏��������͎��s���Ȃ��ł����̂Ŕj�����Ă���
            if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
            {
                Destroy(this);
            }

            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();
        }

        void Update()
        {
            if(!m_canGoResult)
            {
                return;
            }

           //�^���N��e�̓������~�܂��Ă���Ƃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //�`�������W���[�h�̎�
            if (m_saveData.GetSetSelectGameMode == "CHALLENGE")
            {
                //Enemy�^�O������GameObject�� �S�� �擾����B
                GameObject[] enemyObject = GameObject.FindGameObjectsWithTag(TagName.Enemy);
                //Player�^�O������GameObject���擾����B
                GameObject playerObject = GameObject.FindGameObjectWithTag(TagName.Player);

                //�GAI���S�@����ł�����A
                if (enemyObject.Length <= 0)
                {
                    Debug.Log("�G���S�@���S���܂����B");
                    //���݂̃X�e�[�W���Ō�̃X�e�[�W�̏ꍇ
                    if(m_saveData.GetSetSelectStageNum.Equals(m_saveData.GetTotalStageNum()))
                    {
                        //���U���g�˓�
                        InstantiateResultObject(6);
                    }
                    else
                    {
                        StopGame();
                        Invoke(nameof(ChangeChallengeNowNumCountSceneAndStageNum), 3.0f);
                    }

                    //����SE�Đ�
                    m_soundManager.PlayBGM("WinBGM",false);
                }
                //�S�@����ł��Ȃ��Ƃ��A
                //(�܂�v���C���[������ł���Ƃ��A)
                else if (playerObject is null)
                {
                    Debug.Log("�v���C���[�����S���܂����B");

                    //�̗͂��܂��c���Ă���ꍇ�͂�蒼��
                    if (m_saveData.GetSetHitPoint != 0)
                    {
                        StopGame();
                        Invoke(nameof(ChangeChallengeNowNumCountScene),3.0f);
                        //�s�kSE�Đ�
                        m_soundManager.PlayBGM("LoseBGM",false);
                    }
                    else
                    {
                        //�s�kSE�Đ�
                        m_soundManager.PlayBGM("Lose2BGM",false);
                        //���U���g�˓�
                        InstantiateResultObject(5);
                    }
                }
            }
            //�`�������W���[�h�ȊO�̃��[�h�̎�
            else
            {
                //Player�^�O������GameObject��S�Ď擾����B
                GameObject[] playerObject = GameObject.FindGameObjectsWithTag(TagName.Player);

                //�v���C���[���t�B�[���h��Ɉ�l�����ɂȂ�����A
                if (playerObject.Length == 1)
                {
                    m_winPlayerName = playerObject[0].name;

                    switch (SceneManager.GetActiveScene().name)
                    {
                        case SceneName.LocalGameScene:
                            GoResultScene(m_winPlayerName);
                            break;
                        case SceneName.OnlineGameScene:
                            photonView.RPC(nameof(GoResultScene), RpcTarget.All, m_winPlayerName);
                            break;
                    }
                }
            }
        }

        [PunRPC]
        void GoResultScene(string winPlayerName)
        {
            //���U���g�˓�
            int winPlayerNum = int.Parse(Regex.Replace(winPlayerName, @"[^1-4]", string.Empty));
            InstantiateResultObject(winPlayerNum);
            Debug.Log("���s�����܂����B");
        }

        void ChangeChallengeNowNumCountScene()
        {
            //���݂̃`�������W���J�E���g�V�[���ɑJ��
            m_sceneSwitcher.StartTransition(SceneName.ChallengeNowNumCountScene);
        }

        void ChangeChallengeNowNumCountSceneAndStageNum()
        {
            //���݂̃`�������W���J�E���g�V�[���ɑJ��
            m_sceneSwitcher.StartTransition(SceneName.ChallengeNowNumCountScene);
            //���̃X�e�[�W�ԍ��ɐi�߂�
            m_saveData.NextStageNum();
        }

        void InstantiateResultObject(int winPlayer)
        {
            //���U���g�����������J�n
            m_resultInitScript.enabled = true;
            m_resultInitScript.SetWinPlayer(winPlayer);

            StopGame();

            Destroy(this);
        }

        void StopGame()
        {
            //�^���N��e�̓������~�߂�
            m_saveData.GetSetmActiveGameTime = false;

            m_canGoResult = false;
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        }
    }
}