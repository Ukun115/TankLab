using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
	int nextSceneNum = 0;

	void Awake()
	{
		//シーン遷移しても破棄しないようにする
		DontDestroyOnLoad( gameObject );
	}

	//呼ばれたらトランジション起動
	public void StartTransition(string nextSceneName)
	{
		switch(nextSceneName)
        {
			case "TitleScene":
				nextSceneNum = 0;
				break;
			case "DecideNameScene":
				nextSceneNum = 1;
				break;
			case "InputPasswordScene":
				nextSceneNum = 2;
				break;
			case "SelectTankScene":
				nextSceneNum = 3;
				break;
			case "SelectStageScene":
				nextSceneNum = 4;
				break;
			case "MatchingScene":
				nextSceneNum = 5;
				break;
			case "Stage1":
				nextSceneNum = 6;
				break;
			case "Stage2":
				nextSceneNum = 7;
				break;
			case "ResultScene":
				nextSceneNum = 8;
				break;
		}

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
