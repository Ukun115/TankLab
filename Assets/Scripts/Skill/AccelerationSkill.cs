using UnityEngine;

/// <summary>
/// 一定時間加速するスキルの処理
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

		void Start()
	{
		m_playerMovement = GetComponent<PlayerMovement>();

			m_accelerationEffectPrefab = (GameObject)Resources.Load("Ef_SmokeRocketBigParticle");
		}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1) && m_skillFlg == true)
		{
			m_skillFlg = false;
			m_playerMovement.SetSkillSpeed(m_acceleTankSpeed);
			Invoke(nameof(RevertSpeed),0.5f);
			Invoke(nameof(Ct), 6.0f);

				//加速エフェクトを生成する。
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
	}
}
