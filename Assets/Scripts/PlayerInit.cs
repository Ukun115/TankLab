using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInit : MonoBehaviourPunCallbacks
{
    GameObject m_gameObject = null;

    //プレイヤーのプレファブ
    [SerializeField] GameObject m_playerPrefab;

    Vector3[] m_initPosition = {new Vector3(-10.0f,0.0f,0.0f ),new Vector3(10.0f,0.0f,0.0f)};

    void Start()
    {
        if (GameObject.Find("SaveData").GetComponent<SaveData>().GetSetIsOnline)
        {
            m_gameObject = PhotonNetwork.Instantiate(
                m_playerPrefab.name,
                m_initPosition[PhotonNetwork.LocalPlayer.ActorNumber - 1],    //ポジション
                Quaternion.identity,        //回転
                0
                );
            //生成するゲームオブジェクトの名前をPlayer1or2にする
            m_gameObject.name = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
        }
        else
        {
            m_gameObject = Instantiate(
                m_playerPrefab,
                m_initPosition[0],    //ポジション
                Quaternion.identity        //回転
                );
            //生成するゲームオブジェクトの名前をPlayer1にする
            m_gameObject.name = "Player1";
        }

        //デバック
        Debug.Log("プレイヤー番号は「" + m_gameObject.name + "」です。");
    }
}
