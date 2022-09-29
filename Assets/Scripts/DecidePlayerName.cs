using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DecidePlayerName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //仮にえへへという名前にしておく
        m_playerNameText.text = "ehehe";
    }

    void Update()
    {
        //プレイヤー名入力

        //左クリックされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //名前にできない名前の場合シーン遷移できないようにする

            //選択されていたモードによってシーン遷移先を分岐

            //タンク選択シーンに遷移
            SceneManager.LoadScene("SelectTankScene");
        }
    }
}
