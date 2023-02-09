using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    public class DropBomb : MonoBehaviourPun
    {
        SoundManager m_soundManager = null;

        bool m_isPressedButton = false;

        int m_playerNum = 0;

        ControllerData m_controllerData = null;

        SaveData m_saveData = null;

        SkillCool m_skillCoolScript = null;
        int m_coolTime = 5;

        ExplosionBomb m_nowDroppingBomb = null;

        enum EnState
        {
            //設置可能状態
            enDroppableState,
            //設置している状態
            enDroppingState,
            //起爆してクールタイムが発生している状態
            enCoolState
        }
        EnState m_state = EnState.enDroppableState;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", string.Empty));
        }

        void Update()
        {
            //ゲーム進行が止まっているとき
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            //操作切替
            SwitchingOperation();

            //スキルボタンが押されたとき、
            if (m_isPressedButton)
            {
                //設置可能状態のとき、
                if (m_state == EnState.enDroppableState)
                {
                    switch (m_saveData.GetSetSelectGameMode)
                    {
                        //チャレンジモード,ローカルプレイ,トレーニング
                        case "CHALLENGE":
                        case "LOCALMATCH":
                        case "TRAINING":
                            Drop();
                            break;
                        //オンラインプレイ
                        case "RANDOMMATCH":
                        case "PRIVATEMATCH":
                            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
                            {
                                photonView.RPC(nameof(Drop), RpcTarget.All);
                            }
                            else if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                            {
                                Drop();
                            }
                            break;
                    }
                }
                //設置している状態の時、
                else if(m_state == EnState.enDroppingState)
                {
                    //ボム起爆開始
                    m_nowDroppingBomb.ActiveFlashing();
                }
            }
        }

        [PunRPC]
        void Drop()
        {
           //設置した状態にする
            m_state = EnState.enDroppingState;

            BombInstantiate();

            //設置音再生
            m_soundManager.PlaySE("DropBombSE");
        }

        //クール開始処理
        public void CoolStart()
        {
            m_state = EnState.enCoolState;
            Invoke(nameof(Ct), m_coolTime);
            m_skillCoolScript.CoolStart(m_coolTime);
        }

        //操作切替
        void SwitchingOperation()
        {
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                if (m_controllerData.GetGamepad(1) is not null)
                {
                    m_isPressedButton = m_controllerData.GetGamepad(1).leftShoulder.wasPressedThisFrame;
                }
                else
                {
                    m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
                }
            }
            else
            {
                if (m_controllerData.GetGamepad(m_playerNum) is not null)
                {
                    m_isPressedButton = m_controllerData.GetGamepad(m_playerNum).leftShoulder.wasPressedThisFrame;
                }
                else
                {
                    m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
                }
            }
        }

        void BombInstantiate()
        {
            GameObject bombObject = Instantiate(
                Resources.Load("Bomb") as GameObject,
                new Vector3(transform.position.x,-0.4f, transform.position.z),
                transform.rotation
            );
            bombObject.GetComponent<ExplosionBomb>().SetDropPlayer(gameObject);

            m_nowDroppingBomb = null;
            m_nowDroppingBomb = bombObject.GetComponent<ExplosionBomb>();
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Ct()
        {
            //設置可能状態に戻す
            m_state = EnState.enDroppableState;
        }

        public void SetSkillCoolScript(SkillCool skillCool)
        {
            m_skillCoolScript = skillCool;
        }
    }
}
