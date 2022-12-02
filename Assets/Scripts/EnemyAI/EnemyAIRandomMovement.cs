using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �GAI�̃����_���ړ�����
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
			//�R���|�[�l���g�擾�܂Ƃ�
			GetComponents();

			m_navMeshAgent.destination = transform.position;
			SetDestination();
			m_navMeshAgent.avoidancePriority = Random.Range(0, 100);
			//���x�ݒ�
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
			//�w�肵���ړI�n�ɏ�Q�������邩�ǂ����A�����������B�\�Ȃ̂����m�F���Ė��Ȃ���΃Z�b�g����B
			//pathPending �o�H�T���̏����ł��Ă��邩�ǂ���
			if (!m_navMeshAgent.pathPending)
			{
				if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
				{
					//hasPath �G�[�W�F���g���o�H�������Ă��邩�ǂ���
					//navMeshAgent.velocity.sqrMagnitude�̓X�s�[�h
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
			//SamplePosition�͐ݒ肵���ꏊ����5�͈̔͂ōł��߂�������Bake���ꂽ�ꏊ��T���B
			NavMesh.SamplePosition(randomPos, out m_navMeshHit, 5, 1);
			m_navMeshAgent.destination = m_navMeshHit.position;
		}

		//�R���|�[�l���g�擾
		void GetComponents()
	    {
			m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
			m_navMeshAgent = GetComponent<NavMeshAgent>();
		}
	}
}