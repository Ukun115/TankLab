using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nsTankLab
{
    public class ExplosionBomb : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("Rigidbody")] Rigidbody m_rigidbody = null;
        [SerializeField, TooltipAttribute("”š”­‚Ì“–‚½‚è”»’è")] SphereCollider m_sphereCollider = null;
        void Start()
        {
            Invoke(nameof(ActiveCollision), 3.0f);
        }

		void ActiveCollision()
        {
            m_sphereCollider.enabled = true;
            m_rigidbody.useGravity = true;
            Destroy(gameObject,0.05f);
        }
	}
}
