using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンスイッチャー
/// </summary>
namespace nsTankLab
{
public class SceneSwitcher : MonoBehaviour
{
	//来たシーンを保存していくスタック(後入れ先出し)
	static Stack<string> scenes = new Stack<string>();

	void Start()
	{
		//シーン遷移しても破棄しないようにする
		DontDestroyOnLoad( gameObject );

		//始めにスタックにタイトルシーンを入れておく
		scenes.Push("TitleScene");
	}

	//呼ばれたらトランジション起動
	public void StartTransition(string nextSceneName)
	{
		//スタックに遷移するシーンを保存する
		scenes.Push(nextSceneName);
		//遷移先のシーンをロード
		SceneManager.LoadScene(nextSceneName);
	}

	//1つ前のシーンに戻る処理
	public void BackScene()
    {
		//スタックに保存されているシーンを１つ減らす
		scenes.Pop();
		//１つ前のシーンをロード
		SceneManager.LoadScene(scenes.Peek());
	}
}
}