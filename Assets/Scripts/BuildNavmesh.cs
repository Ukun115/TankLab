using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

//DefaultExecutionOrder��0���Ⴂ�l(�W����0)���w�肷�邱�ƂŁA���̏���������Ɏ��s����Ă���
[DefaultExecutionOrder(-103)]
public class BuildNavmesh : MonoBehaviour
{
	//�i�r���b�V���T�[�t�F�[�X
	NavMeshSurface m_navMeshSurface = null;

	//�v���C���[�I�u�W�F�N�g
	[SerializeField] GameObject m_playerObject = null;
	//�GAI�I�u�W�F�N�g
	[SerializeField] GameObject[] m_enemyObject = null;

	void Awake()
	{
		//�`�������W���[�h�̂Ƃ��̂݃i�r���b�V���ƃv���C���[�ƓGAI�𐶐������s����
		if(SceneManager.GetActiveScene().name != "ChallengeGameScene")
        {
			return;
        }

		//�i�r���b�V���𐶐�����B
		m_navMeshSurface = GameObject.Find("Floor").GetComponent<NavMeshSurface>();
		m_navMeshSurface.BuildNavMesh();

		//�v���C���[�𐶐�����
		m_playerObject.SetActive(true);
		//�GAI�𐶐�����
		for (int enemyNum = 0; enemyNum < m_enemyObject.Length; enemyNum++)
		{
			m_enemyObject[enemyNum].SetActive(true);
		}
	}
}