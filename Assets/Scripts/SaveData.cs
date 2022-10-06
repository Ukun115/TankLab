using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ユーザーのセーブデータを管理するスクリプト
/// </summary>
public class SaveData : MonoBehaviour
{
    //選択ステージ名
    string m_selectStageName = "";
    //選択タンク名
    string m_selectTankName = "";

    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //30fpsで固定する。
        Application.targetFrameRate = 30;

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

    //選択されたステージ名セッター
    public void SetSelectStageName(string stageName)
    {
        m_selectStageName = stageName;
    }

    //選択されたタンク名セッター
    public void SetSelectTankName(string tankName)
    {
        m_selectTankName = tankName;
    }
}