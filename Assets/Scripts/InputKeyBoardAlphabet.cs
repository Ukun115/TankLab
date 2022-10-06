using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー名のキーボード入力
/// </summary>
public class InputKeyBoardAlphabet : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("A");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("B");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("C");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("D");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("E");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("F");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("G");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("H");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("I");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("J");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("K");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("L");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("M");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("N");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("O");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("P");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("Q");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("R");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("S");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("T");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("U");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("V");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("W");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("X");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("Y");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //押されたボタンの文字を渡す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("Z");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //一文字消す
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("BACK");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //名前確定
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("OK");
        }
    }
}
