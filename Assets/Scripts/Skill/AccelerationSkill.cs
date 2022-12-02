using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

/// <summary>
/// ��莞�ԉ�������X�L���̏���
/// </summary>
namespace nsTankLab
{
	public class AccelerationSkill : MonoBehaviour
	{
		PlayerMovement m_playerMovement = null;

		float m_acceleTankSpeed = 4.0f;
		float m_nomalTankSpeed = 1.0f;
		bool m_skillFlg = true;

		GameObject accelerationEffect = null;
		GameObject m_accelerationEffectPrefab = null;

		bool m_isPressedButton = false;

		ControllerData m_controllerData = null;

		int m_playerNum = 0;

		SoundManager m_soundManager = null;

		void Start()
		{
			//�R���|�[�l���g�擾�܂Ƃ�
			GetComponents();

			m_accelerationEffectPrefab = (GameObject)Resources.Load("Ef_SmokeRocketBigParticle");

			m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", ""));
		}

		void Update()
		{
			if(m_controllerData.GetGamepad(m_playerNum) is not null)
	        {
				m_isPressedButton = m_controllerData.GetGamepad(m_playerNum).leftShoulder.wasPressedThisFrame;
			}
			else
	        {
				m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
	        }

			if (m_isPressedButton && m_skillFlg == true)
			{
				//�������Đ�
				m_soundManager.PlaySE("AccelerationSE");

				m_skillFlg = false;
				m_playerMovement.SetSkillSpeed(m_acceleTankSpeed);
				Invoke(nameof(RevertSpeed),0.7f);
				Invoke(nameof(Ct), 6.0f);

				//�����G�t�F�N�g�𐶐�����B
				accelerationEffect = Instantiate(
				m_accelerationEffectPrefab,
				transform.Find("PlayerAccelerationEffectPosition").position,
				transform.rotation,
				transform.Find("PlayerAccelerationEffectPosition")
				);
				accelerationEffect.name = "AccelerationEffect";

				accelerationEffect.transform.Rotate(0, 180, 0);
			}
		}

		void RevertSpeed()
		{
			m_playerMovement.SetSkillSpeed(m_nomalTankSpeed);
				Destroy(accelerationEffect);
				accelerationEffect = null;
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
			m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
		}
	}
}