using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
	void Awake()
	{
		//シーン遷移しても破棄しないようにする
		DontDestroyOnLoad( gameObject );
	}

	//呼ばれたらトランジション起動
	public void StartTransition(int nextSceneNum)
	{
		var squares = new SquaresTransition()
		{
			nextScene = nextSceneNum,
			duration = 1.0f,
			squareSize = new Vector2( 10f, 9f ),
			smoothness = 0.0f
		};
		TransitionKit.instance.transitionWithDelegate( squares );
	}
}
