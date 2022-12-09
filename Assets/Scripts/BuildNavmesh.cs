using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

/// <summary>
/// ナビメッシュ作成処理
/// </summary>
namespace nsTankLab
{
	//DefaultExecutionOrderに0より低い値(標準が0)を指定することで、他の処理よりも先に実行されている
	[DefaultExecutionOrder(-103)]
	public class BuildNavmesh : MonoBehaviour
	{
		[SerializeField, TooltipAttribute("敵AIオブジェクト")] GameObject[] m_enemyObject = null;

		//ナビメッシュサーフェース
		NavMeshSurface m_navMeshSurface = null;

		void Awake()
		{
			//チャレンジモードとマッチングシーンのときのみナビメッシュとプレイヤーと敵AIを生成を実行する
			if (SceneManager.GetActiveScene().name == SceneName.ChallengeGameScene || SceneManager.GetActiveScene().name == SceneName.MatchingScene)
			{
				//ナビメッシュを生成する。
				m_navMeshSurface = GetComponent<NavMeshSurface>();
				m_navMeshSurface.BuildNavMesh();

				//敵AIを生成する
				for (int enemyNum = 0; enemyNum < m_enemyObject.Length; enemyNum++)
				{
					m_enemyObject[enemyNum].SetActive(true);
				}
			}
		}
	}
}