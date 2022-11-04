using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

//DefaultExecutionOrderに0より低い値(標準が0)を指定することで、他の処理よりも先に実行されている
[DefaultExecutionOrder(-103)]
public class BuildNavmesh : MonoBehaviour
{
	//ナビメッシュサーフェース
	NavMeshSurface m_navMeshSurface = null;

	//プレイヤーオブジェクト
	[SerializeField] GameObject m_playerObject = null;
	//敵AIオブジェクト
	[SerializeField] GameObject[] m_enemyObject = null;

	void Awake()
	{
		//チャレンジモードのときのみナビメッシュとプレイヤーと敵AIを生成を実行する
		if(SceneManager.GetActiveScene().name != "ChallengeGameScene")
        {
			return;
        }

		//ナビメッシュを生成する。
		m_navMeshSurface = GameObject.Find("Floor").GetComponent<NavMeshSurface>();
		m_navMeshSurface.BuildNavMesh();

		//プレイヤーを生成する
		m_playerObject.SetActive(true);
		//敵AIを生成する
		for (int enemyNum = 0; enemyNum < m_enemyObject.Length; enemyNum++)
		{
			m_enemyObject[enemyNum].SetActive(true);
		}
	}
}