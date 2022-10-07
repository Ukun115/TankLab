using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
	int nextSceneNum = 0;

	//来たシーンを保存していくスタック(後入れ先出し)
	static Stack<string> scenes = new Stack<string>();

	void Awake()
	{
		//シーン遷移しても破棄しないようにする
		DontDestroyOnLoad( gameObject );

		//始めにスタックにタイトルシーンを入れておく
		scenes.Push("TitleScene");
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
			case "ChallengeGameScene":
				nextSceneNum = 6;
				break;
			case "OnlineGameScene":
				nextSceneNum = 7;
				break;
			case "ResultScene":
				nextSceneNum = 8;
				break;
			case "Stage1":
				nextSceneNum = 9;
				break;
			case "Stage2":
				nextSceneNum = 10;
				break;
		}

		//スタックに遷移するシーンを保存する
		scenes.Push(nextSceneName);

		var squares = new SquaresTransition()
		{
			nextScene = nextSceneNum,
			duration = 1.0f,
			squareSize = new Vector2( 10f, 9f ),
			smoothness = 0.0f
		};
		TransitionKit.instance.transitionWithDelegate( squares );
	}

	//1つ前のシーンに戻る処理
	public void BackScene()
    {
		//スタックに保存されているシーンを１つ減らす
		scenes.Pop();
		//１つ前のシーンをロードする
		SceneManager.LoadScene(scenes.Peek());
	}
}
