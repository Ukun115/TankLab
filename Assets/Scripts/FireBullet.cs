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
    float m_yRot = 0;
    Quaternion m_originalQuaternion = Quaternion.identity;
    [SerializeField] int m_sameTimeBulletNum = 0;
    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine)
        {
            return;
        }

        //左クリックされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //デバック
            Debug.Log("発射");

            //発射
            if (m_saveData.GetSetIsOnline)
            {
                photonView.RPC(nameof(Fire), RpcTarget.All);
            }
            else
            {
                m_yRot = 0.0f;
                //元の回転を取得
                m_originalQuaternion = transform.rotation;
                //6発以上は発射しないようにする
                if (m_bulletNum >= 6 || m_bulletNum < 0)
                {
                    return;
                }

                for (int i = 0; i < m_sameTimeBulletNum; i++)
                {
                    //弾の発射角度
                    m_yRot += 20.0f * Mathf.Pow(-1, i) * i;
                    transform.Rotate(0.0f, m_yRot, 0.0f);

                    GameObject m_bulletObject = Instantiate(
                    m_bulletPrefab,
                    transform.position,
                    new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
                    );

                    //元の回転に戻す
                    transform.rotation = m_originalQuaternion;

                    m_bulletObject.name = 1 + "pBullet" + m_bulletNum;
                    m_bulletNum++;
                    //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                    m_bulletObject.transform.parent = GameObject.Find("Bullets").transform;
                };
            }
        }

    }

    //フィールド上に生成されている弾の数を減らす処理
    public void ReduceBulletNum()
    {
        m_bulletNum--;
    }

    [PunRPC]
    void Fire()
    {
        m_yRot = 0.0f;
        //元の回転を取得
        m_originalQuaternion = transform.rotation;
        //6発以上は発射しないようにする
        if (m_bulletNum >= 6 || m_bulletNum < 0)
        {
            return;
        }

        for (int i = 0; i < m_sameTimeBulletNum; i++)
        {
            //弾の発射角度
            m_yRot += 20.0f * Mathf.Pow(-1, i) * i;
            transform.Rotate(0.0f, m_yRot, 0.0f);

            GameObject m_bulletObject = PhotonNetwork.Instantiate(
            m_bulletPrefab.name,
            transform.position,
            new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
            );

            //元の回転に戻す
            transform.rotation = m_originalQuaternion;

            m_bulletObject.name = PhotonNetwork.LocalPlayer.ActorNumber + "pBullet" + m_bulletNum;
            m_bulletNum++;
            //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
            m_bulletObject.transform.parent = GameObject.Find("Bullets").transform;
        };
    }
}