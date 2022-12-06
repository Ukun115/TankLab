using UnityEngine;
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

        void Awake()
        {
            photonView.RPC(nameof(SettingNameAndColor), RpcTarget.All, $"{PhotonNetwork.LocalPlayer.ActorNumber}P", m_saveData.GetSetPlayerNum ^= 1);
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