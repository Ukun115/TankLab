using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �I�����C���̍ۂ̃v���C���[�̏���������
/// </summary>
namespace nsTankLab
{
    public class OnlinePlayerInit : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("�e�v���C���[�̃^���N�̃}�e���A��")] Material[] m_tankColor = new Material[2];
        SaveData m_saveData = null;
        ControllerData m_controllerData = null;

        void Start()
        {
            //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            photonView.RPC(nameof(SettingNameAndColor), RpcTarget.All, $"{PhotonNetwork.LocalPlayer.ActorNumber}P", m_saveData.GetSetPlayerNum);

            //2P�̏ꍇ�A
            if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                //���e�B�N���̐F��ԂɕύX�B
                m_controllerData.SetCursorColor(2);
            }
        }

        //�^���N�̖��O�ƃJ���[��ݒ�
        [PunRPC]
        void SettingNameAndColor(string name,int materialNum)
        {
            gameObject.name = name;
            gameObject.GetComponent<MeshRenderer>().material = m_tankColor[materialNum];
        }
    }
}