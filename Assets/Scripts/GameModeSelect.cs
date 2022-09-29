using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面中のゲームモードを選択する処理
/// </summary>
public class GameModeSelect : MonoBehaviour
{
    //カーソルとなるオブジェクト
    [SerializeField] GameObject m_cursorObject = null;
    //カーソルのポジションとなるポイント
    [SerializeField] GameObject[] m_cursorPoint = null;
    //現在選択されている番号
    int m_selectNum = 0;

    void Start()
    {
        //カーソルオブジェクトの位置を初期位置に置く
        m_cursorObject.transform.position = m_cursorPoint[0].transform.position;
    }

    void Update()
    {
        //上入力されたら、(Wキーが押されたら、)
        if (Input.GetKeyDown(KeyCode.W))
        {
            //選択番号を-1する
            m_selectNum--;

            //一番上だったら、
            if(m_selectNum < 0)
            {
                //一番下に選択を移す
                m_selectNum = m_cursorPoint.Length-1;
            }


            //カーソルオブジェクトを移動させる
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //下入力されたら、(Sキーが押されたら、)
        if (Input.GetKeyDown(KeyCode.S))
        {

            //選択番号を+1する
            m_selectNum++;

            //一番下だったら、
            if (m_selectNum > m_cursorPoint.Length-1)
            {
                //一番上に選択を移す
                m_selectNum = 0;
            }

            //カーソルオブジェクトを移動させる
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //左クリックされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //選択番号によって処理を分岐
            switch(m_selectNum)
            {
                //ランダムマッチ
                case 0:

                    //FirstPlayのキーが存在しない場合はシーン遷移先を名前決めシーンに行くようにする
                    if (!PlayerPrefs.HasKey("FirstPlay"))
                    {
                        SceneManager.LoadScene("DecideNameScene");
                        //FirstPlayのキーに値を入れることで、二度とこのネスト内を実行しないようにする
                        PlayerPrefs.SetInt("FirstPlay", 1);
                    }
                    else
                    {
                        //タンク選択シーンに遷移
                        SceneManager.LoadScene("SelectTankScene");
                    }

                    break;

                //プライベートマッチ
                case 1:

                    //FirstPlayのキーが存在しない場合はシーン遷移先を名前決めシーンに行くようにする
                    if (!PlayerPrefs.HasKey("FirstPlay"))
                    {
                        SceneManager.LoadScene("DecideNameScene");
                        //FirstPlayのキーに値を入れることで、二度とこのネスト内を実行しないようにする
                        PlayerPrefs.SetInt("FirstPlay", 1);
                    }
                    else
                    {

                    }

                    break;

                //ゲーム終了
                case 2:
                    //UnityEditorでプレイしているとき
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    //それ以外
                    #else
                        Application.Quit();
                    #endif
                    break;
            }
        }
    }
}
