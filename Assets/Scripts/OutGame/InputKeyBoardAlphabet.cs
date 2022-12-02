using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// プレイヤー名のキーボード入力処理
/// </summary>
namespace nsTankLab
{
    public class InputKeyBoardAlphabet : MonoBehaviour
    {
        //プレイヤー名決定スクリプト
        DecidePlayerName m_decidePlayerName = null;

        void Start()
        {
            m_decidePlayerName = GameObject.Find("SceneManager").GetComponent<DecidePlayerName>();
        }

        void Update()
        {
            //キーボード入力されたときの処理
            FireKeyBoard();
        }

        //キーボード入力されたときの処理
        void FireKeyBoard()
        {
            InputKey(Keyboard.current.aKey,"A");
            InputKey(Keyboard.current.bKey,"B");
            InputKey(Keyboard.current.cKey,"C");
            InputKey(Keyboard.current.dKey,"D");
            InputKey(Keyboard.current.eKey,"E");
            InputKey(Keyboard.current.fKey,"F");
            InputKey(Keyboard.current.gKey,"G");
            InputKey(Keyboard.current.hKey,"H");
            InputKey(Keyboard.current.iKey,"I");
            InputKey(Keyboard.current.jKey,"J");
            InputKey(Keyboard.current.kKey,"K");
            InputKey(Keyboard.current.lKey,"L");
            InputKey(Keyboard.current.mKey,"M");
            InputKey(Keyboard.current.nKey,"N");
            InputKey(Keyboard.current.oKey,"O");
            InputKey(Keyboard.current.pKey,"P");
            InputKey(Keyboard.current.qKey,"Q");
            InputKey(Keyboard.current.rKey,"R");
            InputKey(Keyboard.current.sKey,"S");
            InputKey(Keyboard.current.tKey,"T");
            InputKey(Keyboard.current.uKey,"U");
            InputKey(Keyboard.current.vKey,"V");
            InputKey(Keyboard.current.wKey,"W");
            InputKey(Keyboard.current.xKey,"X");
            InputKey(Keyboard.current.yKey,"Y");
            InputKey(Keyboard.current.zKey, "Z");

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
                m_decidePlayerName.SetCharacter(inputCharacter);
            }
        }
    }
}