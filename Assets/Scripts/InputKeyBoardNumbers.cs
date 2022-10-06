using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー名のキーボード入力
/// </summary>
public class InputKeyBoardNumbers : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("5");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("6");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("7");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("8");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("9");
        }


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //一文字消す
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("BACK");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //名前確定
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("OK");
        }
    }
}
