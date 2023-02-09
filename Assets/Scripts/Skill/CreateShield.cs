using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace nsTankLab
{
    /// <summary>
    /// 前方にシールドを生成するスキル
    /// </summary>
    public class CreateShield : MonoBehaviourPun
    {
        bool m_isPressedButton = false;

        ControllerData m_controllerData = null;

        SoundManager m_soundManager = null;

        SaveData m_saveData = null;

        int m_playerNum = 0;

        bool m_skillFlg = true;

        SkillCool m_skillCoolScript = null;
        int m_coolTime = 5;

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

            if (m_isPressedButton && m_skillFlg)
            {
                switch (m_saveData.GetSetSelectGameMode)
                {
                    //チャレンジモード,ローカルプレイ,トレーニング
                    case "CHALLENGE":
                    case "LOCALMATCH":
                    case "TRAINING":
                        Create();
                        break;
                    //オンラインプレイ
                    case "RANDOMMATCH":
                    case "PRIVATEMATCH":
                        if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
                        {
                            photonView.RPC(nameof(Create), RpcTarget.All);
                        }
                        else if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                        {
                            Create();
                        }
                        break;
                }
            }
        }

        [PunRPC]
        void Create()
        {
            m_skillFlg = false;

            ShieldInstantiate();

            //生成音再生
            m_soundManager.PlaySE("DropBombSE");

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

        void ShieldInstantiate()
        {
            GameObject shieldObject = Instantiate(
                Resources.Load("ShieldWall") as GameObject,
                new Vector3(transform.position.x, -0.4f, transform.position.z),
                transform.rotation
            );
            shieldObject.name = $"{m_playerNum}PShield";
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
            m_skillFlg = true;
        }

        public void SetSkillCoolScript(SkillCool skillCool)
        {
            m_skillCoolScript = skillCool;
        }
    }
}