using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInit : MonoBehaviourPunCallbacks
{
    GameObject m_gameObject = null;

    //プレイヤーのプレファブ
    [SerializeField] GameObject m_playerPrefab;

    Vector3[] m_initPosition = {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f)};

    void Start()
    {
        m_gameObject = PhotonNetwork.Instantiate(
            m_playerPrefab.name,
            m_initPosition[PhotonNetwork.LocalPlayer.ActorNumber-1],    //ポジション
            Quaternion.identity,        //回転
            0
            );
        //生成するゲームオブジェクトの名前をPlayer1or2にする
        m_gameObject.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;

        //デバック
        Debug.Log("プレイヤー番号は「" + m_gameObject.name + "」です。");
    }
}
