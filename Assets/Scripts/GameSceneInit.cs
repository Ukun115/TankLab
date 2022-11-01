using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �Q�[���V�[���̏���������
/// </summary>
public class GameSceneInit : MonoBehaviourPunCallbacks
{
    //�Z�[�u�f�[�^
    SaveData m_saveData = null;

    [SerializeField, TooltipAttribute("���[�J�����̊e�v���C���[�̈ʒu")] Transform[] m_localPlayerPosition = null;
    //�X�e�[�W���Ƃ̃v���C���[�̏����ʒu(���[�J���̏����ʒu)
    Vector3[][] m_stageLocalPlayerInitPosition =
    {
        //�X�e�[�W1
          new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)},
          //�X�e�[�W2
          new[] { new Vector3( -10.0f, 0.0f, 5.0f ),new Vector3(10.0f,0.0f,5.0f),new Vector3(-10.0f, 0.0f, -5.0f), new Vector3(10.0f, 0.0f, -5.0f)}
    };

    /// <summary>
    /// �I�����C���̏ꍇ�̃v���C���[�����Ŏg�p����ϐ�����
    /// </summary>
    [SerializeField, TooltipAttribute("�I�����C�����̃v���C���[�I�u�W�F�N�g")] GameObject m_onlinePlayerPrefab = null;
    //�X�e�[�W���Ƃ̃v���C���[�̏����ʒu(�I�����C���̏����ʒu)
    Vector3[][] m_stageOnlinePlayerInitPosition =
    {
        //�X�e�[�W1
        new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
        //�X�e�[�W2
        new[] {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f) },
    };
    [SerializeField, TooltipAttribute("�e�v���C���[�̖��O")] TextMeshProUGUI[] m_playerNameText = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�I�����ꂽ�X�e�[�W�ɂ���ăX�e�[�W���V�[���ɍ���
        SceneManager.LoadScene(m_saveData.GetSetSelectStageName, LoadSceneMode.Additive);

        //�f�o�b�N
        Debug.Log($"<color=yellow>�����X�e�[�W�F{m_saveData.GetSetSelectStageName}</color>");

        switch (m_saveData.GetSetSelectGameMode)
        {
            //�`�������W���[�h
            case "CHALLENGE":
                //���O�����[�U�[���ɂ���
                m_playerNameText[0].text = PlayerPrefs.GetString("PlayerName");
                break;

            //���[�J���v���C
            case "LOCALMATCH":
                //�v���C���[�ɏ����ʒu��ݒ肷��B
                for (int playerNum = 0; playerNum < 4; playerNum++)
                {
                    m_localPlayerPosition[playerNum].position = InitPlayerPosition(m_stageLocalPlayerInitPosition)[playerNum];
                }

                break;

            //�I�����C���v���C�̏ꍇ�A�v���C���[�𐶐�����B
            case "RANDOMMATCH":
            case "PRIVATEMATCH":
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
        Debug.Log($"<color=yellow>�Q���v���C���[:{num}</color>");
    }

    //�v���C���[�̏����ʒu��ݒ肷�鏈��
    Vector3[] InitPlayerPosition(Vector3[][] stageInitPlayerPosition)
    {
        //�X�e�[�W�ɂ���ď����ʒu��ݒ�
        switch (m_saveData.GetSetSelectStageName)
        {
            case "STAGE1":
                return stageInitPlayerPosition[0];
            case "STAGE2":
                return stageInitPlayerPosition[1];
            default:
                return null;
        }
    }

    //�v���C���[��������
    void PlayerGeneration()
    {
        GameObject m_gameObjectOnline = PhotonNetwork.Instantiate(
                    m_onlinePlayerPrefab.name,
                    InitPlayerPosition(m_stageOnlinePlayerInitPosition)[PhotonNetwork.LocalPlayer.ActorNumber - 1],    //�|�W�V����
                    Quaternion.identity,        //��]
                    0
                    );
        //�v���C���[�ԍ���ۑ�
        m_saveData.GetSetPlayerNum = (PhotonNetwork.LocalPlayer.ActorNumber - 1);
        //�v���C���[���\��
        photonView.RPC(nameof(DisplayPlayerName), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber - 1, PlayerPrefs.GetString("PlayerName"));
    }
}
