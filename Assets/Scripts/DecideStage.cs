using UnityEngine;

/// <summary>
/// ステージを決定する処理
/// </summary>
public class DecideStage : MonoBehaviour
{
    //ステージ名の左隣に表示するカーソルオブジェクト
    [SerializeField] GameObject m_cursorObject = null;
    //カーソルオブジェクトの位置
    [SerializeField] GameObject[] m_cursorPosition = null;

    public void SetCursorPosition(string character)
    {
        switch (character)
        {
            //ステージ1
            case "STAGE1":
                //カーソル移動
                m_cursorObject.transform.position = m_cursorPosition[0].transform.position;
                break;

            //ステージ2
            case "STAGE2":
                //カーソル移動
                m_cursorObject.transform.position = m_cursorPosition[1].transform.position;
                break;
        }
    }

    public void SetCharacter(string character)
    {
        //選択したステージを保存しておく
        GameObject.Find("SaveData").GetComponent<SaveData>().GetSetSelectStageName = character;

        //マッチングに遷移
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("MatchingScene");
    }
}