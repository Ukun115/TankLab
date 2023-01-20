using UnityEngine;
using UnityEngine.SceneManagement;
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
        ControllerData m_controllerData = null;

        void Start()
        {
            //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            photonView.RPC(nameof(SettingNameAndColor), RpcTarget.All, $"{PhotonNetwork.LocalPlayer.ActorNumber}P", m_saveData.GetSetPlayerNum);

            //2Pの場合、
            if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                //レティクルの色を赤に変更。
                m_controllerData.SetCursorColor(2);
            }
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