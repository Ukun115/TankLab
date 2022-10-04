using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideStage : MonoBehaviour
{
    [SerializeField] GameObject m_cursorObject = null;
    [SerializeField] GameObject[] m_cursorPosition = null;

    public void SetCursorPosition(string character)
    {
        switch (character)
        {
            case "STAGE1":
                //カーソル移動
                m_cursorObject.transform.position = m_cursorPosition[0].transform.position;
                break;
            case "STAGE2":
                //カーソル移動
                m_cursorObject.transform.position = m_cursorPosition[1].transform.position;
                break;
        }
    }

    public void SetCharacter(string character)
    {
        switch (character)
        {
            case "STAGE1":
                //ステージ1に遷移
                SceneManager.LoadScene("Stage1");
                break;
            case "STAGE2":
                //ステージ2に遷移
                SceneManager.LoadScene("Stage2");
                break;
        }
    }
}