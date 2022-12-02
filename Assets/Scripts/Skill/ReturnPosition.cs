using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Text.RegularExpressions;


/// <summary>
/// ���̈ʒu�ɖ߂�X�L���̏���
/// </summary>
namespace nsTankLab
{
	public class ReturnPosition : MonoBehaviour
	{

		PlayerMovement m_playerMovement = null;
		Vector3 m_returnPosition = Vector3.zero;
		Queue<Vector3> m_playerPosition;
		bool m_skillFlg = true;
		bool m_isPressedButton = false;

		int m_playerNum = 0;

		ControllerData m_controllerData = null;

		void Start()
		{
			//�R���|�[�l���g�擾�܂Ƃ�
			GetComponents();

			m_playerPosition = new Queue<Vector3>();
			m_returnPosition = m_playerMovement.GetSetPlayerPosition;


			m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", ""));
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
				m_playerMovement.GetSetPlayerPosition = m_returnPosition;
				Invoke(nameof(Ct), 5.0f);
			}
		}

		void Ct()
		{
			m_skillFlg = true;
		}

		//�R���|�[�l���g�擾
		void GetComponents()
        {
			m_playerMovement = GetComponent<PlayerMovement>();
			m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
		}
	}
}
