using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ユーザーのセーブデータを管理するスクリプト
/// </summary>
public class SaveData : MonoBehaviour
{
    //選択ステージ番号
    int m_selectStageNum = 0;
    //選択タンク番号
    int m_selectTankNum = 0;

    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
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
}