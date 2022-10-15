using UnityEngine;

/// <summary>
/// プレイヤー名のキーボード入力処理
/// </summary>
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
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("5");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("6");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("7");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("8");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            //押されたボタンの文字を渡す
            m_decidePassword.SetCharacter("9");
        }

        //バックスペース
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //一文字消す
            m_decidePassword.SetCharacter("BACK");
        }

        //エンターキー
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //名前確定
            m_decidePassword.SetCharacter("OK");
        }
    }
}
