using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FireBullet : Photon.Pun.MonoBehaviourPun
{
    [SerializeField] GameObject m_bulletPrefab = null;
    //フィールド上に生成されている弾の数
    int m_bulletNum = 0;

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (!photonView.IsMine)
        {
            return;
        }

        //左クリックされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //５発以上は発射しないようにする
            if(m_bulletNum >= 5||m_bulletNum<0)
            {
                return;
            }

            GameObject m_bulletObject = Instantiate(
                m_bulletPrefab,
                transform.position,
                new Quaternion(0.0f, transform.rotation.y,0.0f,transform.rotation.w)
            );
            m_bulletObject.name = "1pBullet" + m_bulletNum;
            m_bulletNum++;
            //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
            m_bulletObject.transform.parent = GameObject.Find("Bullets").transform;
        }
    }

    //フィールド上に生成されている弾の数を減らす処理
    public void ReduceBulletNum()
    {
        m_bulletNum--;
    }
}