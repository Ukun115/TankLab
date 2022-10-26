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
        InputKey(KeyCode.A,"A");
        InputKey(KeyCode.B,"B");
        InputKey(KeyCode.C,"C");
        InputKey(KeyCode.D,"D");
        InputKey(KeyCode.E,"E");
        InputKey(KeyCode.F,"F");
        InputKey(KeyCode.G,"G");
        InputKey(KeyCode.H,"H");
        InputKey(KeyCode.I,"I");
        InputKey(KeyCode.J,"J");
        InputKey(KeyCode.K,"K");
        InputKey(KeyCode.L,"L");
        InputKey(KeyCode.M,"M");
        InputKey(KeyCode.N,"N");
        InputKey(KeyCode.O,"O");
        InputKey(KeyCode.P,"P");
        InputKey(KeyCode.Q,"Q");
        InputKey(KeyCode.R,"R");
        InputKey(KeyCode.S,"S");
        InputKey(KeyCode.T,"T");
        InputKey(KeyCode.U,"U");
        InputKey(KeyCode.V,"V");
        InputKey(KeyCode.W,"W");
        InputKey(KeyCode.X,"X");
        InputKey(KeyCode.Y,"Y");
        InputKey(KeyCode.Z,"Z");

        //バックスペースキーで一文字消す
        InputKey(KeyCode.Backspace, "BACK");
        //エンターキーで名前確定
        InputKey(KeyCode.Return,"OK");
    }

    //キーボード入力されたときの処理
    void InputKey(KeyCode keyCode,string inputCharacter)
    {
        if (Input.GetKeyDown(keyCode))
        {
            m_decidePlayerName.SetCharacter(inputCharacter);
        }
    }
}
