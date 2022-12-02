using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    public class DropBomb : MonoBehaviour
    {
        SoundManager m_soundManager = null;

        int m_timer = 0;

        int m_dropCoolTime = 300;

        bool m_isPressedButton = false;

        int m_playerNum = 0;

        ControllerData m_controllerData = null;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", ""));
        }

        void Update()
        {
            if (m_timer > 0)
            {
                m_timer--;
            }

            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                m_isPressedButton = m_controllerData.GetGamepad(m_playerNum).leftShoulder.wasPressedThisFrame;
            }
            else
            {
                m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
            }

            if (m_isPressedButton && m_timer == 0)
            {
                BombInstantiate();

                //設置音再生
                m_soundManager.PlaySE("DropBombSE");

                m_timer = m_dropCoolTime;
            }
        }

        void BombInstantiate()
        {
            GameObject m_bulletObject = Instantiate(
                Resources.Load("Bomb") as GameObject,
                new Vector3(transform.position.x,-0.4f, transform.position.z),
                transform.rotation
            );
            m_bulletObject.GetComponent<ExplosionBomb>().SetDropPlayer(gameObject);
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }

    }
}
