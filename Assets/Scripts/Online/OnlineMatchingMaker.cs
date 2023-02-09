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

        float m_roomSearchAgainTimer = 0.0f;

        //�}�b�`���O�����������ǂ���(���[�����Č����ł��邩�ǂ���)
        bool m_isMatched = false;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            if (PhotonNetwork.IsConnected)
            {
                //�t�H�g���T�[�o�[����ؒf����
                PhotonNetwork.Disconnect();

                Debug.Log("<color=blue>Photon�T�[�o�[����ؒf���܂���</color>");
            }

            // �V�[���̎�������: �L��
            PhotonNetwork.AutomaticallySyncScene = true;
            // �ő�2�l�܂Ń��[���ɎQ���\
            m_roomOptions.MaxPlayers = 2;

            //�I�����C�����[�h�t���O�𗧂Ă�
            m_saveData.GetSetIsOnline = true;

            //Photon�T�[�o�[(�}�X�^�[�T�[�o�[)�ɐڑ�
            PhotonNetwork.ConnectUsingSettings();
        }

        void Update()
        {
            //�}�b�`���O�������Ă��Ȃ���������s�ł���
            if (!m_isMatched)
            {
                m_roomSearchAgainTimer+=Time.deltaTime;
                if (m_roomSearchAgainTimer > Random.Range(5,10))
                {
                    if (PhotonNetwork.InRoom)
                    {
                        Debug.Log("<color=blue>���[���Č���</color>");

                        //���[�������U�ޏo
                        PhotonNetwork.LeaveRoom();
                        //�Č����B���[����T���B�Ȃ������烋�[���쐬�B
                        //���r�[�ɎQ������
                        PhotonNetwork.JoinLobby();

                        m_roomSearchAgainTimer = 0;
                    }
                }
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
            DontDestroyOnLoad(gameObject);

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
            }
            // �V�[���̎�������: ����
            PhotonNetwork.AutomaticallySyncScene = false;

            //�I�����C�����[�h�t���O������
            m_saveData.GetSetIsOnline = false;
        }

        // ���[������ޏo�������ɌĂ΂��R�[���o�b�N
        public override void OnLeftRoom()
        {
            Debug.Log("<color=blue>���[������ޏo���܂���</color>");
        }

        //�������[���ɂ����ق��v���C���[���ޏo�����Ƃ��ɌĂ΂��R�[���o�b�N
        public override void OnPlayerLeftRoom(Player player)
        {
            Debug.Log("<color=blue>���v���C���[�����[������ޏo���܂���</color>");
        }

        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        [PunRPC]
        void Matched(int stageNum)
        {
            //�}�b�`���O�����e�L�X�g��\��
            m_matchedText.text = "Matched!!";

            //�}�b�`���O���������Đ�����
            m_soundManager.PlaySE("MatchingSE");

            m_saveData.GetSetSelectStageNum = stageNum;

            //���[���Č����ł��Ȃ��悤�ɂ���
            m_isMatched = true;
        }

        public bool IsMatched()
        {
            return m_isMatched;
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