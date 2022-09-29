using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タンク選択画面中のタンクを選択する処理
/// </summary>
public class TankSelect : MonoBehaviour
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
        //左入力されたら、(Aキーが押されたら、)
        if (Input.GetKeyDown(KeyCode.A))
        {
            //選択番号を-1する
            m_selectNum--;

            //一番左だったら、
            if(m_selectNum < 0)
            {
                //一番右に選択を移す
                m_selectNum = m_cursorPoint.Length-1;
            }


            //カーソルオブジェクトを移動させる
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //右入力されたら、(Dキーが押されたら、)
        if (Input.GetKeyDown(KeyCode.D))
        {

            //選択番号を+1する
            m_selectNum++;

            //一番右だったら、
            if (m_selectNum > m_cursorPoint.Length-1)
            {
                //一番左に選択を移す
                m_selectNum = 0;
            }

            //カーソルオブジェクトを移動させる
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //左クリックされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //ステージ選択シーンに遷移
            SceneManager.LoadScene("SelectStageScene");
        }
    }
}
