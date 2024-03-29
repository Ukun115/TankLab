﻿using UnityEngine;
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

		public static GameObject m_instanceTransition = null;

		bool m_isPush = true;
		string m_nextSceneName = string.Empty;

		void Awake()
		{
			CheckInstance();
		}

		//スタックを初期化してタイトルシーンをプッシュする処理
		public void PushTitleScene()
        {
			scenes = new Stack<string>();
			scenes.Push(SceneName.TitleScene);
			scenes.Push(SceneName.TitleScene);
		}

	    //呼ばれたらトランジション起動
	    public void StartTransition(string nextSceneName,bool isPush = true,float delay = 0.0f)
		{
			m_isPush = isPush;
			m_nextSceneName = nextSceneName;

			Invoke(nameof(Next),delay);
		}

		//1つ前のシーンに戻る処理
		public void BackScene(float delay = 0.0f)
		{
			Invoke(nameof(Back),delay);
		}

		void Next()
        {
			if (m_isPush)
			{
				//スタックに遷移するシーンを保存する
				scenes.Push(m_nextSceneName);
			}
			//遷移先のシーンをロード
			SceneManager.LoadScene(m_nextSceneName);
		}

		void Back()
        {
			//スタックに保存されているシーンを１つ減らす
			scenes.Pop();
			//１つ前のシーンをロード
			SceneManager.LoadScene(scenes.Peek());
		}

		//アプリケーションが終了する前に呼び出される関数
		//static変数はアプリケーションが終了されても初期化されず、ずっと残り続けるため、手動で初期化する
		void OnApplicationQuit()
		{
			m_instanceTransition = null;
		}

		//シングルトンパターン
		//インスタンスチェック
		void CheckInstance()
		{
			if (m_instanceTransition is null)
			{
				m_instanceTransition = gameObject;

				//シーン遷移してもこのオブジェクトは破棄されずに保持したままにする
				DontDestroyOnLoad(gameObject);

				//デバック
				Debug.Log("Transitionオブジェクトのインスタンスは未登録です。登録します。");

			}
			else
			{
				Destroy(gameObject);

				//デバック
				Debug.Log("Transitionオブジェクトのインスタンスは登録済です。削除します。");
			}
		}
	}
}