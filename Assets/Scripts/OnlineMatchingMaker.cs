using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// �I�����C���̃}�b�`���O�̏���
/// </summary>
public class OnlineMatchingMaker : MonoBehaviourPunCallbacks
{
    //�}�b�`���O�����I�̃e�L�X�g
    [SerializeField]TextMeshProUGUI m_matchedText = null;
    //�Z�[�u�f�[�^
    SaveData m_saveData = null;
    //�I�����C�����[���̃I�v�V����
    RoomOptions m_roomOptions = new RoomOptions();

    void Awake()
    {
        // �V�[���̎�������: �L��
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        m_roomOptions.MaxPlayers = 2; // �ő�2�l�܂œ����\

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�I�����C�����[�h�t���O�𗧂Ă�
        m_saveData.GetSetIsOnline = true;

        //Photon�T�[�o�[�ɐڑ�
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //�T�[�o�[�ւ̐ڑ�����������ƌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        //�f�o�b�N
        Debug.Log("Photon�T�[�o�[�ւ̐ڑ�������");

        //���r�[�ɎQ������
        PhotonNetwork.JoinLobby();
    }

    //���r�[�ւ̎Q������������ƌĂ΂��R�[���o�b�N
    public override void OnJoinedLobby()
    {
        //�v���C�x�[�g�}�b�`�̏ꍇ
        if (m_saveData.GetSetInputPassword != "----")
        {
            //����J�ɂ���
            m_roomOptions.IsVisible = false;
            PhotonNetwork.JoinOrCreateRoom(m_saveData.GetSetInputPassword,m_roomOptions,TypedLobby.Default);
        }
        //�����_���}�b�`�̏ꍇ
        else
        {
            PhotonNetwork.JoinRandomOrCreateRoom(null);
        }
        //�j�󂳂�Ȃ��悤�ɂ���
        DontDestroyOnLoad(this);

        //�f�o�b�N
        Debug.Log("���r�[�ւ̓���������");
    }

    // ���[���̍쐬�������������ɌĂ΂��R�[���o�b�N
    public override void OnCreatedRoom()
    {
        Debug.Log("���[���̍쐬�ɐ������܂���");
    }

    // ���[���̍쐬�����s�������ɌĂ΂��R�[���o�b�N
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("���[���̍쐬�Ɏ��s���܂���");
    }

    // ���[���ւ̎Q���������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���֎Q�����܂���");
    }

    // ���[�������w�肵�����[���ւ̎Q�������s�������ɌĂ΂��R�[���o�b�N
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("�v���C�x�[�g���[���ւ̎Q���Ɏ��s���܂���");
    }

    // �����_���ȃ��[���ւ̎Q�������s�������ɌĂ΂��R�[���o�b�N
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // �����_���ŎQ���ł��郋�[�������݂��Ȃ��Ȃ�A�V�K�Ń��[�����쐬����
        PhotonNetwork.CreateRoom(null);

        Debug.Log("�����_�����[���ւ̎Q���Ɏ��s���܂����B�����_�����[�����쐬���܂��B");
    }

    //���v���C���[�����[���ɎQ�������Ƃ��ɌĂ΂��R�[���o�b�N
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("���v���C���[�����[���ɎQ�����܂����B");
        //�Q�[���V�[���ɑJ��
        photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        //���v���C���[�������Ă���Ȃ��悤�ɂ��Ă���
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    //���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g���j�����ꂽ�Ƃ��ɌĂ΂��
    //(�V�[�����؂�ւ�����Ƃ��ɌĂ΂��)
    void OnDestroy()
    {
        if (PhotonNetwork.IsConnected)
        {
            //�t�H�g���T�[�o�[����ؒf����
            PhotonNetwork.Disconnect();

            Debug.Log("Photon�T�[�o�[����ؒf���܂���");

            Destroy(this);
        }

       //�I�����C�����[�h�t���O������
       m_saveData.GetSetIsOnline = false;
    }

    // ���[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnLeftRoom()
    {
        Debug.Log("���[������ޏo���܂���");
    }

    //�������[���ɂ����ق��v���C���[���ޏo�����Ƃ��ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player player)
    {
        Debug.Log("���v���C���[�����[������ޏo���܂���");
    }

    //�Q�[���V�[���Ɉڍs����֐�
    [PunRPC]
    void GoGameScene()
    {
        //�}�b�`���O�����e�L�X�g��\��
        m_matchedText.text = "Matched!!";

        PhotonNetwork.IsMessageQueueRunning = false;

        //�Q�[���V�[���Ɉڍs
        PhotonNetwork.LoadLevel("OnlineGameScene");
    }
}