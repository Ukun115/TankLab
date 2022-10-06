using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //�T�[�o�[�ɐڑ�
        PhotonNetwork.ConnectUsingSettings();
    }

     void Update()
    {
        //�Q�l���������A
        if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            //�f�o�b�N
            Debug.Log("�Q�[���J�n");

            //�I�����C�����[�h�t���O�𗧂Ă�
            GameObject.Find("SaveData").GetComponent<SaveData>().GetSetIsOnline = true;

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
        Debug.Log("�T�[�o�[�ւ̐ڑ�������");
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
        PhotonNetwork.CreateRoom(null, roomOptions); //�������̓��[����

        //�f�o�b�N
        Debug.Log("�����Ɏ��s(�������쐬)");
    }

    //���������������Ƃ��ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        //�f�o�b�N
        Debug.Log("����������");
    }

    //�Q�[���V�[���Ɉڍs����֐�
    [PunRPC]
    void GoGameScene()
    {
        //�Q�[���V�[���Ɉڍs
        SceneManager.LoadScene("Stage2");
    }
}
