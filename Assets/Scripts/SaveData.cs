using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

/// <summary>
/// ユーザーのセーブデータを管理するスクリプト
/// </summary>
public class SaveData : MonoBehaviour
{
    //オンラインモードかどうか
    bool m_isOnline = false;

    //選択されたゲームモード
    string m_selectGameMode = "";

    //プレイヤー番号
    int m_playerNum = 0;

    //選択ステージ名
    string m_selectStageName = "Stage1";

    //選択タンク名
    string[] m_selectTankName = new string[4];
    //選択タンク番号
    int[] m_selectTankNum = new int[4];
    //選択スキル名
    string[] m_selectSkillName = new string[4];
    //選択スキル番号
    int[] m_selectSkillNum = new int[4];
    //入力されたパスワード
    string m_inputPassword = "----";

    [SerializeField, TooltipAttribute("プレイヤー名テキスト")] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //60fpsで固定する。
        Application.targetFrameRate = 60;

        //以前のプレイで名前が登録されていた場合、
        if(PlayerPrefs.HasKey("PlayerName"))
        {
            //登録されていた名前を表示させる
            m_playerNameText.text = PlayerPrefs.GetString("PlayerName");

            Debug.Log($"<color=yellow>ユーザー名:{m_playerNameText.text}</color>");
        }
        else
        {
            m_playerNameText.text = "NoName";
        }

        //シーン遷移してもこのオブジェクトは破棄されずに保持したままにする
        DontDestroyOnLoad(this);
    }

    //選択されたゲームモードプロパティ
    public string GetSetSelectGameMode
    {
        //ゲッター
        get { return m_selectGameMode; }
        //セッター
        set { m_selectGameMode = value; }
    }

    //選択されたステージ名プロパティ
    public string GetSetSelectStageName
    {
        //ゲッター
        get { return m_selectStageName; }
        //セッター
        set { m_selectStageName = value; }
    }

    //選択されたタンク名ゲッター
    public string GetSelectTankName(int playerNum)
    {
        return m_selectTankName[playerNum];
    }
    //選択されたタンク名セッター
    public void SetSelectTankName(int playerNum, string tankNum)
    {
        m_selectTankName[playerNum-1] = tankNum;

        //選択されたタンク番号を保存
        m_selectTankNum[playerNum-1] = int.Parse(Regex.Replace(m_selectTankName[playerNum - 1], @"[^0-9]", ""))-1;
    }
    //選択されたスキル名ゲッター
    public string GetSelectSkillName(int playerNum)
    {
        return m_selectSkillName[playerNum];
    }
    //選択されたスキル名セッター
    public void SetSelectSkillName(int playerNum, string skillNum)
    {
        m_selectSkillName[playerNum - 1] = skillNum;

        //選択されたスキル番号を保存
        m_selectSkillNum[playerNum - 1] = int.Parse(Regex.Replace(m_selectSkillName[playerNum - 1], @"[^0-9]", "")) - 1;
    }
    //選択されたタンク名ゲッター
    public int GetSelectTankNum(int playerNum)
    {
        return m_selectTankNum[playerNum];
    }
    //選択されたスキル名ゲッター
    public int GetSelectSkillNum(int playerNum)
    {
        return m_selectSkillNum[playerNum];
    }

    //オンラインモードフラグプロパティ
    public bool GetSetIsOnline
    {
        //ゲッター
        get { return m_isOnline; }
        //セッター
        set { m_isOnline = value; }
    }

    //入力されたパスワードプロパティ
    public string GetSetInputPassword
    {
        //ゲッター
        get { return m_inputPassword; }
        //セッター
        set { m_inputPassword = value; }
    }

    //プレイヤー番号
    public int GetSetPlayerNum
    {
        //ゲッター
        get { return m_playerNum; }
        //セッター
        set { m_playerNum = value; }
    }
}

//#error version
//↑C#言語バージョンの確認