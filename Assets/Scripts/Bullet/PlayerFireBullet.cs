using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;

/// <summary>
/// プレイヤーが弾を発射する処理
/// </summary>
namespace nsTankLab
{
    public class PlayerFireBullet : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("弾プレファブオブジェクト")] GameObject m_bulletPrefab = null;
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("発射位置Transformコンポーネント")] Transform m_firePositionTransform = null;
        [SerializeField, TooltipAttribute("発射エフェクトプレファブ")] GameObject m_fireEffectPrefab = null;
        [SerializeField, TooltipAttribute("発射エフェクト生成位置")] Transform m_fireEffectInstantiatePosition = null;

        //フィールド上に生成されている弾の数
        int m_bulletNum = 0;
        //弾の発射角度
        float m_yRot = 0.0f;
        //弾の位置
        float m_xPos = 0.0f;
        //元の回転
        Quaternion m_originalQuaternion = Quaternion.identity;
        //元の位置
        Vector3 m_originalPosition = Vector3.zero;

        //セーブデータ
        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        ControllerData m_controllerData = null;
        //弾オブジェクトを格納するオブジェクト
        Transform m_bulletsBox = null;

        //発射したプレイヤー番号
        int m_playerNum = 0;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            //発射したプレイヤー番号を取得
            m_playerNum = int.Parse(Regex.Replace(m_firePositionTransform.root.name, @"[^1-4]", string.Empty));
        }

        void Update()
        {
            //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
            if (m_saveData.GetSetIsOnline && SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            if(!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //ゲームパッド操作
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                //RBボタンが押されたとき、
                if (m_controllerData.GetGamepad(m_playerNum).rightShoulder.wasPressedThisFrame)
                {
                    //弾発射
                    FireBullet();
                }
            }
            //キーマウ操作
            else
            {
                //左クリックが押されたとき、
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    //弾発射
                    FireBullet();
                }
            }
        }

        //フィールド上に生成されている弾の数を減らす処理
        public void ReduceBulletNum()
        {
            m_bulletNum = Mathf.Clamp(m_bulletNum - 1, 0, m_bulletNum);
        }

        //弾発射
        void FireBullet()
        {
            //連射できる回数以上は発射しないようにする
            if (m_bulletNum >= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_playerNum-1)].GetRapidFireNum())
            {
                return;
            }

            switch (SceneManager.GetActiveScene().name)
            {
                //現在のシーンがオンラインゲームシーンの時、
                case SceneName.OnlineGameScene:
                    if (m_saveData.GetSetIsOnline)
                    {
                        photonView.RPC(nameof(PlayFireEffect), RpcTarget.All);
                    }
                    break;

                default:
                    PlayFireEffect();
                    break;
            }

            m_yRot = 0.0f;
            //元の回転を取得
            m_originalQuaternion = m_firePositionTransform.rotation;

            m_xPos = 0.0f;
            //元の位置を取得
            m_originalPosition = transform.position;

            //同時に撃つ弾の数によって回す
            for (int i = 0; i < m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_playerNum-1)].GetSameTimeBulletNum(); i++)
            {
                //弾の発射角度
                m_yRot += 20.0f * Mathf.Pow(-1, i) * i;
                m_firePositionTransform.Rotate(0.0f, m_yRot, 0.0f);

                //弾の位置調整
                m_xPos += 0.5f* Mathf.Pow(-1, i) * i;
                m_firePositionTransform.Translate(m_xPos, 0.0f, 0.0f);

                switch (SceneManager.GetActiveScene().name)
                {
                    //現在のシーンがオンラインゲームシーンの時、
                    case SceneName.OnlineGameScene:
                        if (m_saveData.GetSetIsOnline)
                        {
                            //弾を生成
                            GameObject m_bulletObjectOnline = PhotonNetwork.Instantiate(
                            m_bulletPrefab.name,
                            m_firePositionTransform.position,
                            new Quaternion(0.0f, m_firePositionTransform.rotation.y, 0.0f, m_firePositionTransform.rotation.w)
                            );

                            //生成される弾の名前変更
                            m_bulletObjectOnline.name = $"PlayerBullet{ PhotonNetwork.LocalPlayer.ActorNumber}";

                            //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                            m_bulletObjectOnline.transform.parent = m_bulletsBox;

                            //生成される弾はタンクと切り離すため、発射したタンクオブジェクトデータを弾スクリプトに渡しておく。
                            m_bulletObjectOnline.GetComponent<BulletCollision>().SetFireTankObject(gameObject);
                        }
                        break;

                    default:
                        //弾を生成
                        GameObject m_bulletObject = Instantiate(
                        m_bulletPrefab,
                        m_firePositionTransform.position,
                        new Quaternion(0.0f, m_firePositionTransform.rotation.y, 0.0f, m_firePositionTransform.rotation.w)
                        );

                        //生成される弾の名前変更
                        m_bulletObject.name = $"PlayerBullet{m_playerNum}";

                        //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
                        m_bulletObject.transform.parent = m_bulletsBox;

                        //生成される弾はタンクと切り離すため、発射したタンクオブジェクトデータを弾スクリプトに渡しておく。
                        m_bulletObject.GetComponent<BulletCollision>().SetFireTankObject(gameObject);
                        break;
                }

                //元の回転に戻す
                m_firePositionTransform.rotation = m_originalQuaternion;

                //元の位置に戻す
                m_firePositionTransform.position = m_originalPosition;

                m_bulletNum++;
            }
        }

        [PunRPC]
        void PlayFireEffect()
        {
            //発射エフェクトを生成する。
            GameObject fireEffect = Instantiate(
            m_fireEffectPrefab,
            m_fireEffectInstantiatePosition.position,
            transform.rotation,
            m_fireEffectInstantiatePosition
            );
            fireEffect.name = "FireBulletEffect";
            //発射音再生
            m_soundManager.PlaySE("FireSE");
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_bulletsBox = GameObject.Find("Bullets").GetComponent<Transform>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}