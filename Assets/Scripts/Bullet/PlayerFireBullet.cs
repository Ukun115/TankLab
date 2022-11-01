using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;

/// <summary>
/// プレイヤーが弾を発射する処理
/// </summary>
public class PlayerFireBullet : MonoBehaviourPun
{
    [SerializeField, TooltipAttribute("弾プレファブオブジェクト")] GameObject m_bulletPrefab = null;
    //フィールド上に生成されている弾の数
    int m_bulletNum = 0;
    //弾の発射角度
    float m_yRot = 0;
    //元の回転
    Quaternion m_originalQuaternion = Quaternion.identity;

    [SerializeField, TooltipAttribute("弾生成位置")] Transform m_bulletInstantiatePosition = null;

    //セーブデータ
    SaveData m_saveData = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    ControllerData m_controllerData = null;
    //弾オブジェクトを格納するオブジェクト
    GameObject m_bulletsBox = null;

    //発射したプレイヤー番号
    int m_myPlayerNum = 0;

    void Start()
    {
        m_bulletsBox = GameObject.Find("Bullets");

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

        //発射したプレイヤー番号を取得
        m_myPlayerNum = int.Parse(Regex.Replace(this.transform.root.name, @"[^1-4]", "")) - 1;
    }

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

        // ゲームパッドが接続されていたらゲームパッドでの操作
        if (m_controllerData.GetIsConnectedController(m_myPlayerNum))
        {
            //ゲームパッド操作
            //RBボタンが押されたとき、
            //transform.root.nameは最上位の親の名前を取得しています。
            //(1つ上の親の名前はtransform.parent.nameで取得できます。)
            if (Input.GetButtonDown($"{m_myPlayerNum}JoystickRB"))
            {
                //弾生成処理
                BulletInstantiate();
            }
        }
        //それ以外の時、
        else
        {
            //キーマウ操作
            //左クリックが押されたとき、
            if (Input.GetMouseButtonDown(0))
            {
                //弾生成処理
                BulletInstantiate();
            }
        }
    }

    //フィールド上に生成されている弾の数を減らす処理
    public void ReduceBulletNum()
    {
        m_bulletNum--;
    }

    //弾生成処理
    void BulletInstantiate()
    {
        //連射できる回数以上は発射しないようにする
        if (m_bulletNum >= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetRapidFireNum())
        {
            return;
        }

        m_yRot = 0.0f;
        //元の回転を取得
        m_originalQuaternion = m_bulletInstantiatePosition.rotation;

        //同時に撃つ弾の数によって回す
        for (int i = 0; i < m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetSameTimeBulletNum(); i++)
        {
            //弾の発射角度
            m_yRot += 20.0f * Mathf.Pow(-1, i) * i;
            m_bulletInstantiatePosition.Rotate(0.0f, m_yRot, 0.0f);

            //オンラインモード
            if (m_saveData.GetSetIsOnline && SceneManager.GetActiveScene().name == "OnlineGameScene")
            {

                //弾を生成
                GameObject m_bulletObject = PhotonNetwork.Instantiate(
                m_bulletPrefab.name,
                m_bulletInstantiatePosition.position,
                new Quaternion(0.0f, m_bulletInstantiatePosition.rotation.y, 0.0f, m_bulletInstantiatePosition.rotation.w)
                );

                //生成される弾の名前変更
                m_bulletObject.name = $"{ PhotonNetwork.LocalPlayer.ActorNumber}pBullet";

                //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                m_bulletObject.transform.parent = m_bulletsBox.transform;
            }
            else
            {
                //弾を生成
                GameObject m_bulletObject = Instantiate(
                m_bulletPrefab,
                m_bulletInstantiatePosition.position,
                new Quaternion(0.0f, m_bulletInstantiatePosition.rotation.y, 0.0f, m_bulletInstantiatePosition.rotation.w)
                );

                //生成される弾の名前変更
                m_bulletObject.name = $"{1}pBullet";

                //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                m_bulletObject.transform.parent = m_bulletsBox.transform;
            }

            //元の回転に戻す
            m_bulletInstantiatePosition.rotation = m_originalQuaternion;

            m_bulletNum++;
        }
    }
}