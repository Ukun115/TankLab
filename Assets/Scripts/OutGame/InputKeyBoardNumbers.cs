using UnityEngine;

/// <summary>
/// プレイヤー名のキーボード入力処理
/// </summary>
namespace nsTankLab
{
public class InputKeyBoardNumbers : MonoBehaviour
{
    //パスワード決定処理スクリプト
    DecidePassword m_decidePassword = null;

    void Start()
    {
        m_decidePassword = GameObject.Find("SceneManager").GetComponent<DecidePassword>();
    }

    void Update()
    {
        //キーボード入力されたときの処理
        FireKeyboard();
    }

    //キーボード入力されたときの処理
    void FireKeyboard()
    {
        //キーボード上のキー
        InputKey(KeyCode.Alpha0, "0");
        InputKey(KeyCode.Alpha1, "1");
        InputKey(KeyCode.Alpha2, "2");
        InputKey(KeyCode.Alpha3, "3");
        InputKey(KeyCode.Alpha4, "4");
        InputKey(KeyCode.Alpha5, "5");
        InputKey(KeyCode.Alpha6, "6");
        InputKey(KeyCode.Alpha7, "7");
        InputKey(KeyCode.Alpha8, "8");
        InputKey(KeyCode.Alpha9, "9");
        //テンキー
        InputKey(KeyCode.Keypad0, "0");
        InputKey(KeyCode.Keypad1, "1");
        InputKey(KeyCode.Keypad2, "2");
        InputKey(KeyCode.Keypad3, "3");
        InputKey(KeyCode.Keypad4, "4");
        InputKey(KeyCode.Keypad5, "5");
        InputKey(KeyCode.Keypad6, "6");
        InputKey(KeyCode.Keypad7, "7");
        InputKey(KeyCode.Keypad8, "8");
        InputKey(KeyCode.Keypad9, "9");

        //バックスペースキーで一文字消す
        InputKey(KeyCode.Backspace, "BACK");
        //エンターキーで名前確定
        InputKey(KeyCode.Return,"OK");
    }

    //キーボード入力されたときの処理
    void InputKey(KeyCode keyCode, string inputCharacter)
    {
        if (Input.GetKeyDown(keyCode))
        {
            m_decidePassword.SetCharacter(inputCharacter);
        }
    }
}
}