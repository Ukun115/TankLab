using UnityEngine;
using Photon.Pun;

/// <summary>
/// オンラインの際のプレイヤーの初期化処理
/// </summary>
namespace nsTankLab
{
    public class OnlinePlayerInit : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("各プレイヤーのタンクのマテリアル")] Material[] m_tankColor = new Material[2];

        SaveData m_saveData = null;

        void Awake()
        {
            photonView.RPC(nameof(SettingNameAndColor), RpcTarget.All, $"{PhotonNetwork.LocalPlayer.ActorNumber}P", m_saveData.GetSetPlayerNum ^= 1);
        }

        //タンクの名前とカラーを設定
        [PunRPC]
        void SettingNameAndColor(string name,int materialNum)
        {
            gameObject.name = name;
            gameObject.GetComponent<MeshRenderer>().material = m_tankColor[materialNum];
        }
    }
}