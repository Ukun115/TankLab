using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

/// <summary>
/// �i�r���b�V���쐬����
/// </summary>
namespace nsTankLab
{
	//DefaultExecutionOrder��0���Ⴂ�l(�W����0)���w�肷�邱�ƂŁA���̏���������Ɏ��s����Ă���
	[DefaultExecutionOrder(-103)]
	public class BuildNavmesh : MonoBehaviour
	{
		[SerializeField, TooltipAttribute("�GAI�I�u�W�F�N�g")] GameObject[] m_enemyObject = null;

		//�i�r���b�V���T�[�t�F�[�X
		NavMeshSurface m_navMeshSurface = null;

		void Awake()
		{
			//�`�������W���[�h�ƃ}�b�`���O�V�[���̂Ƃ��̂݃i�r���b�V���ƃv���C���[�ƓGAI�𐶐������s����
			if (SceneManager.GetActiveScene().name == SceneName.ChallengeGameScene || SceneManager.GetActiveScene().name == SceneName.MatchingScene)
			{
				//�i�r���b�V���𐶐�����B
				m_navMeshSurface = GetComponent<NavMeshSurface>();
				m_navMeshSurface.BuildNavMesh();

				//�GAI�𐶐�����
				for (int enemyNum = 0; enemyNum < m_enemyObject.Length; enemyNum++)
				{
					m_enemyObject[enemyNum].SetActive(true);
				}
			}
		}
	}
}