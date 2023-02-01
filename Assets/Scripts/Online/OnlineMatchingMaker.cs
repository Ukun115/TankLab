using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// �I�����C���̃}�b�`���O�̏���
/// </summary>
namespace nsTankLab
{
    public class OnlineMatchingMaker : MonoBehaviourPunCallbacks
    {
        [SerializeField, TooltipAttribute("�}�b�`���O�����e�L�X�g")] TextMeshProUGUI m_matchedText = null;

        //�Z�[�u�f�[�^
        SaveData m_saveData = null;
        //�I�����C�����[���̃I�v�V����
        RoomOptions m_roomOptions = new RoomOptions();

        SoundManager m_soundManager = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            // �V�[���̎�������: �L��
            PhotonNetwork.AutomaticallySyncScene = true;
            // �ő�2�l�܂Ń��[���ɎQ���\
            m_roomOptions.MaxPlayers = 2;

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
            Debug.Log("<color=blue>Photon�T�[�o�[�ւ̐ڑ�������</color>");

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
                //�v���C�x�[�g���[���ɎQ������B�Q���ł��郋�[�����Ȃ�������A���[�����쐬����B
                PhotonNetwork.JoinOrCreateRoom(m_saveData.GetSetInputPassword,m_roomOptions,TypedLobby.Default);
            }
            //�����_���}�b�`�̏ꍇ
            else
            {
                //�����_���ȃ��[���ɎQ������B�Q���ł��郋�[�����Ȃ�������A���[�����쐬����B
                PhotonNetwork.JoinRandomOrCreateRoom(null);
            }

            //�V�[���J�ڂ��Ă��j�󂳂�Ȃ��悤�ɂ���
            DontDestroyOnLoad(this);

            //�f�o�b�N
            Debug.Log("<color=blue>���r�[�ւ̎Q��������</color>");
        }

        // ���[���̍쐬�������������ɌĂ΂��R�[���o�b�N
        public override void OnCreatedRoom()
        {
            Debug.Log("<color=blue>���[���̍쐬�ɐ������܂���</color>");
        }

        // ���[���̍쐬�����s�������ɌĂ΂��R�[���o�b�N
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("<color=blue>���[���̍쐬�Ɏ��s���܂���</color>");
        }

        // ���[���ւ̎Q���������������ɌĂ΂��R�[���o�b�N
        public override void OnJoinedRoom()
        {
            Debug.Log("<color=blue>���[���֎Q�����܂���</color>");
        }

        // ���[�������w�肵�����[���ւ̎Q�������s�������ɌĂ΂��R�[���o�b�N
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("<color=blue>�v���C�x�[�g���[���ւ̎Q���Ɏ��s���܂���</color>");
        }

        // �����_�����[���ւ̎Q�������s�������ɌĂ΂��R�[���o�b�N
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            // �����_�����[�������݂��Ȃ��Ȃ�A�V�K�Ń��[�����쐬����
            PhotonNetwork.CreateRoom(null);

            Debug.Log("<color=blue>�����_�����[���ւ̎Q���Ɏ��s���܂����B�����_�����[�����쐬���܂��B</color>");
        }

        //���v���C���[�����[���ɎQ�������Ƃ��ɌĂ΂��R�[���o�b�N
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("<color=blue>���v���C���[�����[���ɎQ�����܂����B</color>");

            int randomNum = Random.Range(1, 10);
            photonView.RPC(nameof(Matched), RpcTarget.All,randomNum);

            //�Q�[���V�[���ɑJ��
            //�I������Ă���X�e�[�W�𒊑I
            Invoke(nameof(DelayGoGameScene),2.0f);
            //�Q�[�����ɑ��v���C���[�����[���ɎQ�����Ă��Ȃ��悤�ɂ��Ă���
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        void DelayGoGameScene()
        {
            photonView.RPC(nameof(GoGameScene), RpcTarget.All);
        }

        //���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g���j�����ꂽ�Ƃ��ɌĂ΂��
        //(�V�[�����؂�ւ�����Ƃ��ɌĂ΂��)
        void OnDestroy()
        {
            if (PhotonNetwork.IsConnected)
            {
                //�t�H�g���T�[�o�[����ؒf����
                PhotonNetwork.Disconnect();

                Debug.Log("<color=blue>Photon�T�[�o�[����ؒf���܂���</color>");

                Destroy(this);
            }

           //�I�����C�����[�h�t���O������
           m_saveData.GetSetIsOnline = false;
        }

        // ���[������ޏo�������ɌĂ΂��R�[���o�b�N
        public override void OnLeftRoom()
        {
            Debug.Log("���[������ޏo���܂���</color>");
        }

        //�������[���ɂ����ق��v���C���[���ޏo�����Ƃ��ɌĂ΂��R�[���o�b�N
        public override void OnPlayerLeftRoom(Player player)
        {
            Debug.Log("���v���C���[�����[������ޏo���܂���</color>");
        }

        [PunRPC]
        void Matched(int stageNum)
        {
            //�}�b�`���O�����e�L�X�g��\��
            m_matchedText.text = "Matched!!";

            //�}�b�`���O���������Đ�����
            m_soundManager.PlaySE("MatchingSE");

            m_saveData.GetSetSelectStageNum = stageNum;
        }

        //�Q�[���V�[���Ɉڍs����֐�
        [PunRPC]
        void GoGameScene()
        {
            //�Q�[���V�[���Ɉڍs
            PhotonNetwork.LoadLevel("OnlineGameScene");

            PhotonNetwork.IsMessageQueueRunning = false;
        }
    }
}