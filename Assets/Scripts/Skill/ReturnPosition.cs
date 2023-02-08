using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Photon.Pun;


/// <summary>
/// 元の位置に戻るスキルの処理
/// </summary>
namespace nsTankLab
{
	public class ReturnPosition : MonoBehaviourPun
	{
		PlayerMovement m_playerMovement = null;
		Vector3 m_updateReturnPosition = Vector3.zero;
		Vector3 m_returnPosition = Vector3.zero;
		Queue<Vector3> m_playerPosition = new Queue<Vector3>();
		bool m_skillFlg = true;
		bool m_isPressedButton = false;

		int m_playerNum = 0;

		ControllerData m_controllerData = null;

		SaveData m_saveData = null;

		GameObject m_teleportEffectPrefab = null;

		SkillCool m_skillCoolScript = null;
		int m_coolTime = 6;

		SoundManager m_soundManager = null;

		void Start()
		{
			//コンポーネント取得まとめ
			GetComponents();

			m_teleportEffectPrefab = (GameObject)Resources.Load("TeleportEffect");

			m_updateReturnPosition = m_playerMovement.GetSetPlayerPosition;

			m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

			m_playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", string.Empty));
		}

		void FixedUpdate()
		{
			m_playerPosition.Enqueue(m_playerMovement.GetSetPlayerPosition);

			if (m_playerPosition.Count == 240)
			{
				m_updateReturnPosition = m_playerPosition.Dequeue();
			}
		}

		void Update()
		{
			//ゲーム進行が止まっているとき
			if (!m_saveData.GetSetmActiveGameTime)
			{
				return;
			}

			//このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
			if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
			{
				return;
			}

			//操作切替
			SwitchingOperation();

			if (m_isPressedButton && m_skillFlg)
			{
				switch (m_saveData.GetSetSelectGameMode)
				{
					//チャレンジモード,ローカルプレイ,トレーニング
					case "CHALLENGE":
					case "LOCALMATCH":
					case "TRAINING":
						PlayEffect();
						break;
					//オンラインプレイ
					case "RANDOMMATCH":
					case "PRIVATEMATCH":
						if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
						{
							photonView.RPC(nameof(PlayEffect), RpcTarget.All);
						}
						else if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
						{
							PlayEffect();
						}
						break;
				}
			}
		}

		[PunRPC]
		void PlayEffect()
        {
			m_skillFlg = false;
			Invoke(nameof(Teleport), 0.5f);

			m_returnPosition = m_updateReturnPosition;

			//ワープ音を再生する
			m_soundManager.PlaySE("WarpSE");

			//テレポートエフェクト再生
			InstatiateTeleportEffect();
		}

		//操作切替
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

		void Ct()
		{
			m_skillFlg = true;
		}

		void Teleport()
        {
			m_playerMovement.GetSetPlayerPosition = m_returnPosition;

			m_playerMovement.SetTeleportStopping(true);

			Invoke(nameof(StoppingOff),0.5f);

			Invoke(nameof(Ct), m_coolTime);
			m_skillCoolScript.CoolStart(m_coolTime);
		}

		void StoppingOff()
        {
			m_playerMovement.SetTeleportStopping(false);
		}

		//コンポーネント取得
		void GetComponents()
        {
			m_playerMovement = GetComponent<PlayerMovement>();
			m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
			m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
		}

		//テレポートエフェクトを生成処理
		void InstatiateTeleportEffect()
		{
			GameObject m_teleportEffectObject = Instantiate(
			m_teleportEffectPrefab,
			new Vector3(m_returnPosition.x,-0.4f, m_returnPosition.z),
			Quaternion.identity
			);
			m_teleportEffectObject.name = $"TeleportEffect";

			m_teleportEffectObject.transform.Rotate(90, 0, 0);
		}

		public void SetSkillCoolScript(SkillCool skillCool)
		{
			m_skillCoolScript = skillCool;
		}
	}
}
