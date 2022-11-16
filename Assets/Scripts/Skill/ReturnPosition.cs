using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// å≥ÇÃà íuÇ…ñﬂÇÈÉXÉLÉãÇÃèàóù
/// </summary>
namespace nsTankLab
{
	public class ReturnPosition : MonoBehaviour
	{

		PlayerMovement m_playerMovement = null;
		Vector3 m_returnPosition = Vector3.zero;
		Queue<Vector3> m_playerPosition;
		bool m_skillFlg = true;


		void Start()
		{
			m_playerMovement = GetComponent<PlayerMovement>();
			m_playerPosition = new Queue<Vector3>();
			m_returnPosition = m_playerMovement.GetSetPlayerPosition;
		}

		void FixedUpdate()
		{
			m_playerPosition.Enqueue(m_playerMovement.GetSetPlayerPosition);

			if (m_playerPosition.Count == 120)
			{
				m_returnPosition = m_playerPosition.Dequeue();
			}
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(1) && m_skillFlg)
			{
				m_skillFlg = false;
				m_playerMovement.GetSetPlayerPosition = m_returnPosition;
				Invoke(nameof(Ct), 5.0f);
			}
		}

		void Ct()
		{
			m_skillFlg = true;
		}

	}
}
