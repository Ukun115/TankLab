using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ユーザーのセーブデータを管理するスクリプト
/// </summary>
public class SaveData : MonoBehaviour
{
    //オンラインモードかどうか
    bool m_isOnline = false;

    int m_playerNum = 0;

    //選択ステージ名
    string m_selectStageName = "Stage1";
    //選択タンク名
    string m_selectTankName = "Tank1";
    //入力されたパスワード
    string m_inputPassword = "----";

    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //30fpsで固定する。
        Application.targetFrameRate = 60;

        //以前のプレイで名前が登録されていた場合、
        if(PlayerPrefs.HasKey("PlayerName"))
        {
            //登録されていた名前を表示させる
            m_playerNameText.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            m_playerNameText.text = "NoName";
        }

        //シーン遷移してもこのオブジェクトは破棄されずに保持したままにする
        DontDestroyOnLoad(this);
    }

    //選択されたステージ名プロパティ
    public string GetSetSelectStageName
    {
        //ゲッター
        get { return m_selectStageName; }
        //セッター
        set { m_selectStageName = value; }
    }

    //選択されたタンク名プロパティ
    public string GetSetSelectTankName
    {
        //ゲッター
        get { return m_selectTankName; }
        //セッター
        set { m_selectTankName = value; }
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