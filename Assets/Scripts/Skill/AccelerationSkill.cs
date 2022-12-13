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

		GameObject m_accelerationEffectPrefab = null;

		bool m_isPressedButton = false;

		ControllerData m_controllerData = null;

		int m_playerNum = 0;

		SoundManager m_soundManager = null;

		SaveData m_saveData = null;

		SkillCool m_skillCoolScript = null;

		int m_coolTime = 6;

		void Start()
		{
			//�R���|�[�l���g�擾�܂Ƃ�
			GetComponents();

			m_accelerationEffectPrefab = (GameObject)Resources.Load("Ef_SmokeRocketBigParticle");

			m_playerNum = int.Parse(Regex.Replace(gameObject.transform.root.name, @"[^1-4]", string.Empty));

			m_playerMovement.SetSkillSpeed(m_nomalTankSpeed);
		}

		void Update()
		{
			//�Q�[���i�s���~�܂��Ă���Ƃ�
			if (!m_saveData.GetSetmActiveGameTime)
			{
				return;
			}

			//����ؑ�
			SwitchingOperation();

			if (m_isPressedButton && m_skillFlg == true)
			{
				//�������Đ�
				m_soundManager.PlaySE("AccelerationSE");

				m_skillFlg = false;
				m_playerMovement.SetSkillSpeed(m_acceleTankSpeed);
				Invoke(nameof(RevertSpeed),0.7f);

				//�����G�t�F�N�g�𐶐�����B
				InstatiateAccelerationEffect("L");
				InstatiateAccelerationEffect("R");
			}
		}

		//����ؑ֏���
		void SwitchingOperation()
        {
			if (m_controllerData.GetGamepad(m_playerNum) is not null)
			{
				m_isPressedButton = m_controllerData.GetGamepad(m_playerNum).leftShoulder.wasPressedThisFrame;
			}
			else
			{
				m_isPressedButton = Mouse.current.rightButton.wasPressedThisFrame;
			}
		}

		void RevertSpeed()
		{
			m_playerMovement.SetSkillSpeed(m_nomalTankSpeed);
			//�G�t�F�N�g�폜
			DestroyEffect("L");
			DestroyEffect("R");

			Invoke(nameof(Ct), m_coolTime);
			m_skillCoolScript.CoolStart(m_coolTime);
		}

		void Ct()
		{
			m_skillFlg = true;
		}

		//�����G�t�F�N�g�𐶐�����
		void InstatiateAccelerationEffect(string lr)
        {
			GameObject effectObject = Instantiate(
			m_accelerationEffectPrefab,
			transform.Find($"JetPack/PlayerAccelerationEffectPosition_{lr}").position,
			transform.rotation,
			transform.Find($"JetPack/PlayerAccelerationEffectPosition_{lr}")
			);
			effectObject.name = $"AccelerationEffect_{lr}";

			effectObject.transform.Rotate(0, 180, 0);

			//�W�F�b�g�p�b�N�I�u�W�F�N�g���A�N�e�B�u�ɂ���
			transform.Find($"JetPack/JetPack_{lr}").gameObject.SetActive(true);
			transform.Find($"JetPack/JetPack_{lr}_Fire").gameObject.SetActive(true);
		}

		void DestroyEffect(string lr)
        {
			//�W�F�b�g�p�b�N�I�u�W�F�N�g���A�N�f�B�u�ɂ���
			transform.Find($"JetPack/JetPack_{lr}").gameObject.SetActive(false);
			transform.Find($"JetPack/JetPack_{lr}_Fire").gameObject.SetActive(false);
		}

		//�R���|�[�l���g�擾
		void GetComponents()
	    {
			m_playerMovement = GetComponent<PlayerMovement>();
			m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
			m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
			m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
		}

		public void SetSkillCoolScript(SkillCool skillCool)
        {
			m_skillCoolScript = skillCool;
        }
	}
}