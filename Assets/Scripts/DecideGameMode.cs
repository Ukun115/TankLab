using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideGameMode : MonoBehaviour
{
    public void SetCharacter(string character)
    {
        switch (character)
        {
            case "CHALLENGE":
                //チャレンジゲームに遷移
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("ChallengeGameScene");
                break;

            case "RANDOMMATCH":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("DecideNameScene");
                }
                //通常遷移
                else
                {
                    //タンクシーンに遷移
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                }

                break;

            case "PRIVATEMATCH":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("DecideNameScene");
                }
                //通常遷移
                else
                {
                    //パスワード入力画面に遷移
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("InputPasswordScene");
                }

                break;

            case "EXIT":
                //ゲーム終了
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

                break;
        }
    }
}
