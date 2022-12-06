using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// プレイヤー名のキーボード入力処理
/// </summary>
namespace nsTankLab
{
    public class InputKeyBoardNumbers : MonoBehaviour
    {
        //パスワード決定処理スクリプト
        [SerializeField]DecidePassword m_decidePassword = null;

        void Update()
        {
            //キーボード入力されたときの処理
            FireKeyboard();
        }

        //キーボード入力されたときの処理
        void FireKeyboard()
        {
            InputKey(Keyboard.current.numpad0Key, "0");
            InputKey(Keyboard.current.numpad1Key, "1");
            InputKey(Keyboard.current.numpad2Key, "2");
            InputKey(Keyboard.current.numpad3Key, "3");
            InputKey(Keyboard.current.numpad4Key, "4");
            InputKey(Keyboard.current.numpad5Key, "5");
            InputKey(Keyboard.current.numpad6Key, "6");
            InputKey(Keyboard.current.numpad7Key, "7");
            InputKey(Keyboard.current.numpad8Key, "8");
            InputKey(Keyboard.current.numpad9Key, "9");

            //バックスペースキーで一文字消す
            InputKey(Keyboard.current.backspaceKey, "BACK");
            //エンターキーで名前確定
            InputKey(Keyboard.current.enterKey,"OK");
        }

        //キーボード入力されたときの処理
        void InputKey(KeyControl key, string inputCharacter)
        {
            if (key.wasPressedThisFrame)
            {
                m_decidePassword.SetCharacter(inputCharacter);
            }
        }
    }
}