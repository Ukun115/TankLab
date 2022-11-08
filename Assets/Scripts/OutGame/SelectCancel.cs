using UnityEngine;

/// <summary>
///選択しているタンクをキャンセルする処理
/// </summary>
namespace nsTankLab
{
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
        if (Input.GetAxisRaw($"{ m_playerNum }PJoystick{m_buttonCharacter}") < -0.1f)
        {
            //選択をキャンセル
            Cancel();
        }
    }

    //選択キャンセル処理
    void Cancel()
    {
        //保存されていたタンクをキャンセル
        m_saveData.SetSelectTankName(m_playerNum, null);
        //UI表示を非表示にする
        gameObject.SetActive(false);
    }
}
}