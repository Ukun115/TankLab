using UnityEngine;

/// <summary>
/// プレイヤー名のキーボード入力処理
/// </summary>
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("A");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("B");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("C");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("D");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("E");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("F");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("G");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("H");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("I");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("J");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("K");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("L");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("M");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("N");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("O");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("P");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("Q");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("R");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("S");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("T");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("U");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("V");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("W");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("X");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("Y");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //押されたボタンの文字を渡す
            m_decidePlayerName.SetCharacter("Z");
        }

        //バックスペース
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //一文字消す
            m_decidePlayerName.SetCharacter("BACK");
        }

        //エンターキー
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //名前確定
            m_decidePlayerName.SetCharacter("OK");
        }
    }
}
