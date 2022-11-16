using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace nsTankLab
{
    public class DropBomb : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                BombInstantiate();
            }
        }

        void BombInstantiate()
        {
            GameObject m_bulletObject = Instantiate(
                        Resources.Load("Bomb") as GameObject,
                        new Vector3(transform.position.x,-0.4f, transform.position.z),
                        transform.rotation
                        );
        }

    }
}
