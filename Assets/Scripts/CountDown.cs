using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// カウントダウン処理
    /// </summary>
    public class CountDown : MonoBehaviour
    {
        TextMeshProUGUI m_countDownText = null;

        float m_timer = 0;

        SaveData m_saveData = null;

        SoundManager m_soundManager = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            m_countDownText = GetComponent<TextMeshProUGUI>();

            //タンクや弾が動けないようにする
            m_saveData.GetSetmActiveGameTime = false;

            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            //BGMを一旦止める
            m_soundManager.StopBGM();

            //ゲーム開始音を再生する
            m_soundManager.PlaySE("GameStartGingleSE");

            m_countDownText.text = "READY...";
        }

        void Update()
        {
            switch((int)m_timer)
            {
                case 4:
                    m_countDownText.text = "GO!!";
                    break;
                case 5:
                    //タンクや弾が動けるようにする
                    m_saveData.GetSetmActiveGameTime = true;
                    //カウントダウンスクリプトを削除
                    Destroy(gameObject);

                    switch (m_saveData.GetSetSelectGameMode)
                    {
                        //チャレンジモード
                        case "CHALLENGE":
                            //チャレンジモードのBGMを再生する
                            m_soundManager.PlayBGM("GameSceneBGM01");
                            break;
                        //ローカルプレイ
                        //オンラインプレイの場合
                        case "LOCALMATCH":
                        case "RANDOMMATCH":
                        case "PRIVATEMATCH":
                            //ローカルプレイモード、オンラインモードのBGMを再生する
                            m_soundManager.PlayBGM("GameSceneBGM02");
                            break;
                    }
                    break;
            }

            m_timer+=Time.deltaTime;
        }
    }
}