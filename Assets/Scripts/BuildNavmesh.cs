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
	//�i�r���b�V���T�[�t�F�[�X
	NavMeshSurface m_navMeshSurface = null;

	//�GAI�I�u�W�F�N�g
	[SerializeField] GameObject[] m_enemyObject = null;

	void Awake()
	{
		//�`�������W���[�h�̂Ƃ��̂݃i�r���b�V���ƃv���C���[�ƓGAI�𐶐������s����
		if (SceneManager.GetActiveScene().name != "ChallengeGameScene")
		{
			return;
		}

		//�i�r���b�V���𐶐�����B
		m_navMeshSurface = GameObject.Find("Floor").GetComponent<NavMeshSurface>();
		m_navMeshSurface.BuildNavMesh();

		//�GAI�𐶐�����
		for (int enemyNum = 0; enemyNum < m_enemyObject.Length; enemyNum++)
		{
			m_enemyObject[enemyNum].SetActive(true);
		}
	}
}
}