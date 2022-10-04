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
            case "RANDOMMATCH":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    //SceneManager.LoadScene("DecideNameScene");
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(1);
                }
                //通常遷移
                else
                {
                    //タンクシーンに遷移
                    //SceneManager.LoadScene("SelectTankScene");
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition(2);
                }

                break;

            case "PRIVATEMATCH":
                //プレイヤー名を登録していなかった場合、
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //プレイヤー名登録シーンを挟む
                    SceneManager.LoadScene("DecideNameScene");
                }
                //通常遷移
                else
                {

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
