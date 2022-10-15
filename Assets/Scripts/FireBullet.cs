using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// 弾を発射する処理
/// </summary>
public class FireBullet : MonoBehaviourPun
{
    //弾オブジェクトのプレファブ
    [SerializeField] GameObject m_bulletPrefab = null;
    //フィールド上に生成されている弾の数
    int m_bulletNum = 0;
    //弾の発射角度
    float m_yRot = 0;
    //元の回転
    Quaternion m_originalQuaternion = Quaternion.identity;
    //同時に発射する弾数
    [SerializeField] int m_sameTimeBulletNum = 0;
    //セーブデータ
    SaveData m_saveData = null;
    //弾オブジェクトを格納するオブジェクト
    GameObject m_bulletsBox = null;

    void Start()
    {
        m_bulletsBox = GameObject.Find("Bullets");

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

        //左クリックされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //左クリックされたときの処理
            FireLeftClick();
        }
    }

    //フィールド上に生成されている弾の数を減らす処理
    public void ReduceBulletNum()
    {
        m_bulletNum--;
    }

    //左クリックされたときの処理
    void FireLeftClick()
    {
        Debug.Log(m_bulletNum);

        //5発以上は発射しないようにする
        if (m_bulletNum >= 5)
        {
            return;
        }

        m_yRot = 0.0f;
        //元の回転を取得
        m_originalQuaternion = transform.rotation;

        //同時に撃つ弾の数によって回す
        for (int i = 0; i < m_sameTimeBulletNum; i++)
        {
            //弾の発射角度
            m_yRot += 20.0f * Mathf.Pow(-1, i) * i;
            transform.Rotate(0.0f, m_yRot, 0.0f);

            //オンラインモード
            if (m_saveData.GetSetIsOnline && SceneManager.GetActiveScene().name == "OnlineGameScene")
            {

                //弾を生成
                GameObject m_bulletObject = PhotonNetwork.Instantiate(
                m_bulletPrefab.name,
                transform.position,
                new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
                );

                //生成される弾の名前変更
                m_bulletObject.name = PhotonNetwork.LocalPlayer.ActorNumber + "pBullet" + m_bulletNum;

                //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                m_bulletObject.transform.parent = m_bulletsBox.transform;
            }
            else
            {
                //弾を生成
                GameObject m_bulletObject = Instantiate(
                m_bulletPrefab,
                transform.position,
                new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
                );

                //生成される弾の名前変更
                m_bulletObject.name = 1 + "pBullet" + m_bulletNum;

                //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                m_bulletObject.transform.parent = m_bulletsBox.transform;
            }

            //元の回転に戻す
            transform.rotation = m_originalQuaternion;

            m_bulletNum++;
        }
    }
}