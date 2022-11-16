using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

/// <summary>
/// �����𖞂����ƃ��U���g��ʂɍs������
/// </summary>
namespace nsTankLab
{
public class GoResult : MonoBehaviour
{
    GameObject resultObject = null;
    SaveData m_saveData = null;
    SoundManager m_soundManager = null;
    [SerializeField, TooltipAttribute("���U���g�����������Ă���v���t�@�u�I�u�W�F�N�g")] GameObject m_resultPrefab = null;

        SceneSwitcher m_sceneSwitcher = null;

    void Start()
    {
            //�}�b�`���O�V�[���̏ꍇ�͏��������͎��s���Ȃ��ł����̂Ŕj�����Ă���
            if (SceneManager.GetActiveScene().name == "MatchingScene")
            {
                Destroy(this);
            }

            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
    }

    void Update()
    {
        //�`�������W���[�h�̎�
        if (m_saveData.GetSetSelectGameMode == "CHALLENGE")
        {
            //Enemy�^�O������GameObject�� �S�� �擾����B
            GameObject[] enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
            //Player�^�O������GameObject���擾����B
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

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
                        //���݂̃`�������W���J�E���g�V�[���ɑJ��
                        m_sceneSwitcher.StartTransition("ChallengeNowNumCountScene");
                        //���̃X�e�[�W�ԍ��ɐi�߂�
                        m_saveData.NextStageNum();
                    }
            }
            //�S�@����ł��Ȃ��Ƃ��A
            //(�܂�v���C���[������ł���Ƃ��A)
            else if (playerObject is null)
            {
                    Debug.Log("�v���C���[�����S���܂����B");

                    //�̗͂��܂��c���Ă���ꍇ�͂�蒼��
                    if (m_saveData.GetSetHitPoint != 0)
                    {
                        //���݂̃`�������W���J�E���g�V�[���ɑJ��
                        m_sceneSwitcher.StartTransition("ChallengeNowNumCountScene");
                    }
                    else
                    {
                        //���U���g�˓�
                        InstantiateResultObject(5);
                    }
            }
        }
        //�`�������W���[�h�ȊO�̃��[�h�̎�
        else
        {
            //Player�^�O������GameObject��S�Ď擾����B
            GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player");

            //�v���C���[���t�B�[���h��Ɉ�l�����ɂȂ�����A
            if (playerObject.Length == 1)
            {
                //���U���g�˓�
                int winPlayerNum = int.Parse(Regex.Replace(playerObject[0].name, @"[^1-4]", ""));
                InstantiateResultObject(winPlayerNum);
                Debug.Log("���s�����܂����B");
            }
        }
    }

    void InstantiateResultObject(int winPlayer)
    {
        //���U���g�ɓ˓�
        //���U���g�����͖��V�[�����ƂɂP�x�݂̂������s���Ȃ�
        resultObject = GameObject.Find("Result");
            //���U���g�������܂Ƃ߂Ă���Q�[���I�u�W�F�N�g�𐶐����A
            //���U���g���������s���Ă����B
            resultObject = Instantiate(m_resultPrefab);
            resultObject.name = "Result";
            //�����\��
            resultObject.GetComponent<ResultInit>().SetWinPlayer(winPlayer);

                //����SE�Đ�
                m_soundManager.PlaySE("WinSE");
                //BGM�~�߂�
                m_soundManager.StopBGM();

                //�^���N��e�̓������~�߂�
                m_saveData.GetSetmActiveGameTime = false;

            Destroy(this);
    }
}
}