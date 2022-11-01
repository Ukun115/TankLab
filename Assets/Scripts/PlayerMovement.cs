using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// プレイヤーの移動スクリプト
/// </summary>
public class PlayerMovement : Photon.Pun.MonoBehaviourPun
{
    //剛体
    Rigidbody m_rigidbody = null;

    //移動方向
    Vector3 m_moveDirection = Vector3.zero;

    //移動速度
    [SerializeField] float m_speed = 5.0f;

    //左右矢印キーの値(-1.0f～1.0f)
    float m_horizontal = 0.0f;
    //上下矢印キーの値(-1.0f～1.0f)
    float m_vertical = 0.0f;

    float m_latestVertical;

    SaveData m_saveData = null;

    Vector3 latestPos;

    void Start()
    {
        //Rigidbodyのコンポーネントを取得する
        m_rigidbody = GetComponent<Rigidbody>();
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

        //左右矢印キーの値(-1.0f～1.0f)を取得する
        m_horizontal = Input.GetAxis("Horizontal");
        //上下矢印キーの値(-1.0f～1.0f)を取得する
        m_vertical = Input.GetAxis("Vertical");

        //入力されたキーの値を保存
        m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
        m_moveDirection = transform.TransformDirection(m_moveDirection);

        //斜めの距離が長くなる√2倍になるのを防ぐ。
        m_moveDirection.Normalize();

        //移動方向に速度を掛ける(通常移動)
        m_moveDirection *= m_speed;
    }

    void FixedUpdate()
    {
        //前フレームとの位置の差から進行方向を割り出してその方向に回転します。
        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f )
        {
			if (m_moveDirection == new Vector3(0, 0, 0)) return;
			if (m_vertical > 0 && m_latestVertical > 0)
			{
				Quaternion rot = Quaternion.LookRotation(differenceDis);
				rot = Quaternion.Slerp(m_rigidbody.transform.rotation, rot, 0.1f);
				this.transform.rotation = rot;
			}
			if (m_vertical < 0 && m_latestVertical < 0)
			{
				Quaternion rot = Quaternion.LookRotation(-differenceDis);
				rot = Quaternion.Slerp(m_rigidbody.transform.rotation, rot, 0.1f);
				this.transform.rotation = rot;
			}
		}
        m_latestVertical = m_vertical;

        //剛体に移動を割り当て(一緒に重力も割り当て)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
    }
}
