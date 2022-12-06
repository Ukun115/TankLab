using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �Q�[���V�[���̏���������
/// </summary>
namespace nsTankLab
{
    public class GameSceneInit : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("���[�J�����̊e�v���C���[�̈ʒu")] Transform[] m_localPlayerPosition = null;
        [SerializeField, TooltipAttribute("�I�����C�����̃v���C���[�I�u�W�F�N�g")] GameObject m_onlinePlayerPrefab = null;
        [SerializeField, TooltipAttribute("�e�v���C���[�̖��O")] TextMeshProUGUI[] m_playerNameText = null;

        //�Z�[�u�f�[�^
        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        //�X�e�[�W���Ƃ̃v���C���[�̏����ʒu(���[�J���̏����ʒu)
        Vector3[][] m_stageLocalPlayerInitPosition =
        {
            //�X�e�[�W1
            new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)},
            //�X�e�[�W2
            new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)}
        };

        //�X�e�[�W���Ƃ̃v���C���[�̏����ʒu(�I�����C���̏����ʒu)
        Vector3[][] m_stageOnlinePlayerInitPosition =
        {
            //�X�e�[�W1
            new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
            //�X�e�[�W2
            new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
        };

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            //�I�����ꂽ�X�e�[�W�ɂ���ăX�e�[�W���V�[���ɍ���
            SceneManager.LoadScene($"Stage{ m_saveData.GetSetSelectStageNum}", LoadSceneMode.Additive);

            //�f�o�b�N
            Debug.Log($"<color=yellow>�����X�e�[�W�FStage{m_saveData.GetSetSelectStageNum}</color>");

            switch (m_saveData.GetSetSelectGameMode)
            {
                //�`�������W���[�h
                case "CHALLENGE":
                    //���O�����[�U�[���ɂ���
                    m_playerNameText[0].text = PlayerPrefs.GetString("PlayerName");
                    //�`�������W���[�h��BGM���Đ�����
                    m_soundManager.PlayBGM("GameSceneBGM01");
                    break;

                //���[�J���v���C
                case "LOCALMATCH":
                    //�v���C���[�ɏ����ʒu��ݒ肷��B
                    for (int playerNum = 0; playerNum < m_localPlayerPosition.Length; playerNum++)
                    {
                        m_localPlayerPosition[playerNum].position = m_stageLocalPlayerInitPosition[m_saveData.GetSetSelectStageNum - 1][playerNum];
                    }
                    //���[�J���v���C���[�h��BGM���Đ�����
                    m_soundManager.PlayBGM("GameSceneBGM02");

                    break;

                //�I�����C���v���C�̏ꍇ�A�v���C���[�𐶐�����B
                case "RANDOMMATCH":
                case "PRIVATEMATCH":
                    //�}�b�`���O�V�[���̓I�����C���Ή��̃v���C���[���f���͐������Ȃ��B
                    if(SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                    {
                        break;
                    }

                    //�I�����C���v���C���[�h��BGM���Đ�����
                    m_soundManager.PlayBGM("GameSceneBGM02");

                    PhotonNetwork.IsMessageQueueRunning = true;

                    //�v���C���[��������
                    PlayerGeneration();

                    break;
            }
        }

        //�v���C���[����\��������֐�
        [PunRPC]
        void DisplayPlayerName(int num,string playerName)
        {
            //�v���C���[����\��
            m_playerNameText[num].text = playerName;

            //�f�o�b�N
            Debug.Log($"<color=blue>�Q���v���C���[:{m_saveData.GetSetPlayerNum}P</color>");
        }

        //�v���C���[��������
        void PlayerGeneration()
        {
            Debug.Log($"<color=blue>Photon���[�J���v���C���[�̃A�N�^�[�i���o�[:{PhotonNetwork.LocalPlayer.ActorNumber}</color>");

            GameObject gameObjectOnline = PhotonNetwork.Instantiate(
                m_onlinePlayerPrefab.name,
                m_stageOnlinePlayerInitPosition[m_saveData.GetSetSelectStageNum-1][PhotonNetwork.LocalPlayer.ActorNumber-1],    //�|�W�V����
                Quaternion.identity,        //��]
                0
             );

            //�v���C���[�ԍ���ۑ�
            m_saveData.GetSetPlayerNum = PhotonNetwork.LocalPlayer.ActorNumber-1;
            //�v���C���[���\��
            photonView.RPC(nameof(DisplayPlayerName), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber-1, PlayerPrefs.GetString("PlayerName"));
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        }
    }
}