using UnityEngine;
using UnityEngine.InputSystem;

namespace nsTankLab
{
    public class DropBomb : MonoBehaviour
    {
        SoundManager m_soundManager = null;

        int m_timer = 0;

        int m_dropCoolTime = 300;

        bool m_isPressedButton = false;

        void Start()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        }

        void Update()
        {
            if (m_timer > 0)
            {
                m_timer--;
            }

            if (Gamepad.current is not null)
            {
                m_isPressedButton = Gamepad.current.leftShoulder.wasPressedThisFrame;
            }
            else
            {
                m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
            }

            if (m_isPressedButton && m_timer == 0)
            {
                BombInstantiate();

                //ê›íuâπçƒê∂
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

    }
}
