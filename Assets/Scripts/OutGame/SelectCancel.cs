using UnityEngine;

/// <summary>
///選択しているタンクをキャンセルする処理
/// </summary>
public class SelectCancel : MonoBehaviour
{
    [SerializeField, TooltipAttribute("プレイヤー番号"), Range(1,4)]int m_playerNum = 0;

    [SerializeField, TooltipAttribute("押されるボタンの文字列(LT or RT)")]string m_buttonCharacter = "";

    //セーブデータ
    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    void Update()
    {
        //ボタンが押されたら、
        //(文字列補間を使用)
        switch(m_buttonCharacter)
        {
            case "LT":
                if (Input.GetAxisRaw($"{ m_playerNum }PJoystickLT") < -0.1f)
                {
                    //選択をキャンセル
                    Cancel();
                }
                break;

            case "RT":
                if (Input.GetAxisRaw($"{ m_playerNum }PJoystickRT") > 0.1f)
                {
                    //選択をキャンセル
                    Cancel();
                }
                break;
        }
    }

    //選択キャンセル処理
    void Cancel()
    {
        //保存されていたタンクをキャンセル
        m_saveData.SetSelectTankName(m_playerNum, null);
        //UI表示を非表示にする
        this.gameObject.SetActive(false);
    }
}
