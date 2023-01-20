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

        bool m_skillFlg = true;

        bool m_isPressedButton = false;

        int m_playerNum = 0;

        ControllerData m_controllerData = null;

        SaveData m_saveData = null;

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
                m_skillFlg = false;

                BombInstantiate();

                //設置音再生
                m_soundManager.PlaySE("DropBombSE");

                Invoke(nameof(Ct), m_coolTime);
                m_skillCoolScript.CoolStart(m_coolTime);
            }
        }

        //操作切替
        void SwitchingOperation()
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

        void BombInstantiate()
        {
            GameObject bombObject = Instantiate(
                Resources.Load("Bomb") as GameObject,
                new Vector3(transform.position.x,-0.4f, transform.position.z),
                transform.rotation
            );
            bombObject.GetComponent<ExplosionBomb>().SetDropPlayer(gameObject);
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
