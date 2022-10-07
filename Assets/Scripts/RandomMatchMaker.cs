using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{
    [SerializeField]TextMeshProUGUI m_matchedText = null;

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //Photon�T�[�o�[�ɐڑ�
        PhotonNetwork.ConnectUsingSettings();
    }

     void Update()
    {
        //�Q�l���������A
        if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            //�f�o�b�N
            Debug.Log("�Q�[���J�n");

            //�Q�[���V�[���ɑJ��
            photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        }
    }

    //�T�[�o�[�ւ̐ڑ�����������ƌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        //�����_���ŕ����ɓ�������
        PhotonNetwork.JoinRandomRoom();

        //�f�o�b�N
        Debug.Log("Photon�T�[�o�[�ւ̐ڑ�������");
    }

    //���r�[�ւ̓�������������ƌĂ΂��R�[���o�b�N
    public override void OnJoinedLobby()
    {
        //���r�[�ɓ��������瑦���ɕ����ɓ�������
        PhotonNetwork.JoinRandomRoom();

        //�f�o�b�N
        Debug.Log("���r�[�ւ̓���������");
    }

    // �����Ɏ��s�����ꍇ�ɌĂ΂��R�[���o�b�N
    // �P�l�ڂ͕������Ȃ����ߕK�����s����̂ŕ������쐬����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // �ő�2�l�܂œ����\
        PhotonNetwork.JoinOrCreateRoom(m_saveData.GetSetInputPassword, roomOptions,TypedLobby.Default); //�������̓��[����

        //�f�o�b�N
        Debug.Log("�����Ɏ��s(�������쐬)");
        Debug.Log("���[������" + m_saveData.GetSetInputPassword + "�ł��B");
    }

    //���������������Ƃ��ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        //�f�o�b�N
        Debug.Log("����������");
    }

    //���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g���j�����ꂽ�Ƃ��ɌĂ΂��
    //(�V�[�����؂�ւ�����Ƃ��ɌĂ΂��)
    void OnDestroy()
    {
        //�I�����C�����Ȃ����ĂȂ�������A
        if (PhotonNetwork.LocalPlayer.ActorNumber != 2)
        {
            //�t�H�g���T�[�o�[����ؒf����
            PhotonNetwork.Disconnect();

            Debug.Log("Photon�T�[�o�[����ؒf���܂���");
        }
    }

    // ���[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnLeftRoom()
    {
        Debug.Log("���[������ޏo���܂���");
    }

    //�Q�[���V�[���Ɉڍs����֐�
    [PunRPC]
    void GoGameScene()
    {
        //�}�b�`���O�����e�L�X�g��\��
        m_matchedText.text = "Matched!!";

        //�I�����C�����[�h�t���O�𗧂Ă�
        m_saveData.GetSetIsOnline = true;

        //�Q�[���V�[���Ɉڍs
        SceneManager.LoadScene("OnlineGameScene");
    }
}