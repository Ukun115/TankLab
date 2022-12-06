using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    /// <summary>
    /// 前方にシールドを生成するスキル
    /// </summary>
    public class CreateShield : MonoBehaviour
    {
        bool m_isPressedButton = false;

        ControllerData m_controllerData = null;

        SoundManager m_soundManager = null;

        SaveData m_saveData = null;

        int m_playerNum = 0;

        int m_timer = 0;

        int m_createCoolTime = 300;

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

            if (m_timer > 0)
            {
                m_timer--;
            }

            //操作切替
            SwitchingOperation();

            if (m_isPressedButton && m_timer == 0)
            {
                ShieldInstantiate();

                //生成音再生
                m_soundManager.PlaySE("DropBombSE");

                m_timer = m_createCoolTime;
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
    }
}