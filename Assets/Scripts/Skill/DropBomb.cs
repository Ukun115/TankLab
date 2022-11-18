using UnityEngine;

namespace nsTankLab
{
    public class DropBomb : MonoBehaviour
    {
        SoundManager m_soundManager = null;

        void Start()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                BombInstantiate();

                //ê›íuâπçƒê∂
                m_soundManager.PlaySE("DropBombSE");
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
