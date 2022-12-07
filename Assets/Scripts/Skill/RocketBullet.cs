using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    public class RocketBullet : MonoBehaviour
    {
        bool m_isPressedButton = false;
        bool m_skillFlg = true;

        int m_playerNum = 0;

        ControllerData m_controllerData = null;

        SkillCool m_skillCoolScript = null;
        int m_coolTime = 5;

        void Start()
        {
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", string.Empty));
        }

        void Update()
        {
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                m_isPressedButton = m_controllerData.GetGamepad(m_playerNum).leftShoulder.wasPressedThisFrame;
            }
            else
            {
                m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
            }

            if (m_isPressedButton && m_skillFlg)
            {
                m_skillFlg = false;
                RocketBulletInstantiate();

                Invoke(nameof(Ct), m_coolTime);
                m_skillCoolScript.CoolStart(m_coolTime);
            }
        }

        void RocketBulletInstantiate()
        {
            GameObject m_bulletObject = Instantiate(
                        Resources.Load("RocketBullet") as GameObject,
                        transform.position,
                        new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
                        );
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