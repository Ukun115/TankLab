using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �Q�[���V�[���̏���������
/// </summary>
public class GameSceneInit : MonoBehaviourPunCallbacks
{
    //�v���C���[�̃v���t�@�u
    [SerializeField] GameObject m_playerPrefab;

    //�e�v���C���[�̏����ʒu
    Vector3[] m_initPosition = {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f)};

    //�e�v���C���[�̉�ʕ\������閼�O
    [SerializeField] TextMeshProUGUI[] m_playerNameText = null;

    //�Z�[�u�f�[�^
    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�I�����ꂽ�X�e�[�W�ɂ���ăX�e�[�W���V�[���ɍ���
        SceneManager.LoadScene(m_saveData.GetSetSelectStageName, LoadSceneMode.Additive);

        //�f�o�b�N
        Debug.Log(m_saveData.GetSetSelectStageName + "����������܂����B");

        if (m_saveData.GetSetIsOnline)
        {
            PhotonNetwork.IsMessageQueueRunning = true;

            GameObject m_gameObject = PhotonNetwork.Instantiate(
                m_playerPrefab.name,
                m_initPosition[PhotonNetwork.LocalPlayer.ActorNumber - 1],    //�|�W�V����
                Quaternion.identity,        //��]
                0
                );
            //�v���C���[�ԍ���ۑ�
            m_saveData.GetSetPlayerNum = (PhotonNetwork.LocalPlayer.ActorNumber-1);

            //�v���C���[���\��
            photonView.RPC(nameof(DisplayPlayerName), RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber-1, PlayerPrefs.GetString("PlayerName"));
        }
        else
        {
            GameObject m_gameObject = Instantiate(
                m_playerPrefab,
                m_initPosition[0],    //�|�W�V����
                Quaternion.identity        //��]
                );
            //��������Q�[���I�u�W�F�N�g�̖��O��Player1�ɂ���
            m_gameObject.name = "Player1";

            //�v���C���[����\��
            m_playerNameText[0].text = PlayerPrefs.GetString("PlayerName");

            //�f�o�b�N
            Debug.Log("�v���C���[���Q�����܂����B");
        }
    }

    //�v���C���[����\��������֐�
    [PunRPC]
    void DisplayPlayerName(int num,string playerName)
    {
        //�v���C���[����\��
        m_playerNameText[num].text = playerName;

        //�f�o�b�N
        Debug.Log("�v���C���[" + num + "���Q�����܂����B");
    }
}
