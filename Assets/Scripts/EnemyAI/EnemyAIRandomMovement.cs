using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵AIのランダム移動処理
/// </summary>
namespace nsTankLab
{
	public class EnemyAIRandomMovement : MonoBehaviour
	{
		float m_wanderRangeX = 13.0f;
		float m_wanderRangeZ = 8.0f;
		NavMeshAgent m_navMeshAgent = null;
		NavMeshHit m_navMeshHit = new NavMeshHit();

		SaveData m_saveData = null;

		void Start()
		{
			//コンポーネント取得まとめ
			GetComponents();

			m_navMeshAgent.destination = transform.position;
			SetDestination();
			m_navMeshAgent.avoidancePriority = Random.Range(0, 100);
			//速度設定
			m_navMeshAgent.speed = 1.0f;
		}

		void Update()
		{
			if (!m_saveData.GetSetmActiveGameTime)
			{
				m_navMeshAgent.speed = 0.0f;

				return;
			}
			else
	        {
				m_navMeshAgent.speed = 1.0f;
			}

			RandomWander();
		}

		void RandomWander()
		{
			//指定した目的地に障害物があるかどうか、そもそも到達可能なのかを確認して問題なければセットする。
			//pathPending 経路探索の準備できているかどうか
			if (!m_navMeshAgent.pathPending)
			{
				if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
				{
					//hasPath エージェントが経路を持っているかどうか
					//navMeshAgent.velocity.sqrMagnitudeはスピード
					if (!m_navMeshAgent.hasPath || m_navMeshAgent.velocity.sqrMagnitude == 0f)
					{
						SetDestination();
					}
				}
			}
		}

		void SetDestination()
		{
			Vector3 randomPos = new Vector3(Random.Range(-m_wanderRangeX, m_wanderRangeX), 0, Random.Range(-m_wanderRangeZ, m_wanderRangeZ));
			//SamplePositionは設定した場所から5の範囲で最も近い距離のBakeされた場所を探す。
			NavMesh.SamplePosition(randomPos, out m_navMeshHit, 5, 1);
			m_navMeshAgent.destination = m_navMeshHit.position;
		}

		//コンポーネント取得
		void GetComponents()
	    {
			m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
			m_navMeshAgent = GetComponent<NavMeshAgent>();
		}
	}
}