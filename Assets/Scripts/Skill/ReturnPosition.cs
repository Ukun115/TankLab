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
		Vector3 m_returnPosition;
		Queue<Vector3> m_playerPosition;
		bool m_skillFlg = true;


		void Start()
		{
			m_playerMovement = GetComponent<PlayerMovement>();
			m_playerPosition = new Queue<Vector3>();
			m_returnPosition = m_playerMovement.GetPlayerPosition();
		}

		void FixedUpdate()
		{
			m_playerPosition.Enqueue(m_playerMovement.GetPlayerPosition());

			if (m_playerPosition.Count == 90)
			{
				m_returnPosition = m_playerPosition.Dequeue();
			}
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(1) && m_skillFlg == true)
			{
				m_skillFlg = false;
				m_playerMovement.SetPlayerPosition(m_returnPosition);
				Invoke(nameof(Ct), 6.0f);
			}
		}

		void Ct()
		{
			m_skillFlg = true;
		}

	}
}
