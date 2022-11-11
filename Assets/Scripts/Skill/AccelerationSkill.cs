using UnityEngine;

/// <summary>
/// ˆê’èŠÔ‰Á‘¬‚·‚éƒXƒLƒ‹‚Ìˆ—
/// </summary>
namespace nsTankLab
{
public class AccelerationSkill : MonoBehaviour
{
	
	PlayerMovement m_playerMovement = null;
		
	float m_acceleTankSpeed = 2.0f;
	float m_nomalTankSpeed = 1.0f;
	bool m_skillFlg = true;

	void Start()
	{
		m_playerMovement = GetComponent<PlayerMovement>();

	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1) && m_skillFlg == true)
		{
			m_skillFlg = false;
			m_playerMovement.SetSkillSpeed(m_acceleTankSpeed);
			Invoke(nameof(RevertSpeed),3.0f);
			Invoke(nameof(Ct), 6.0f);
		}
	}

	void RevertSpeed()
	{
		m_playerMovement.SetSkillSpeed(m_nomalTankSpeed);
	}
	void Ct()
	{
		m_skillFlg = true;
	}
	}
}
