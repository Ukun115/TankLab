using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

namespace nsTankLab
{
    /// <summary>
    /// トレーニングシーンでのタンクとスキルを選択するウィンドウ表示クラス
    /// </summary>
    public class DisplayTankAndSkillWindow : MonoBehaviour
    {
        //ウィンドウ2D
        [SerializeField] GameObject m_window2D = null;
        //ウィンドウタイトルテキスト
        [SerializeField] TextMeshProUGUI m_titleText = null;
        //ボタンテキスト
        [SerializeField] GameObject[] m_buttonText = { null };
        //ウィンドウ3D
        [SerializeField] GameObject m_window3D = null;
        //ボタンオブジェクト
        [SerializeField] GameObject[] m_buttonObject = { null };
        //タンク&スキル詳細テキスト
        [SerializeField] TextMeshProUGUI[] m_infoTankSkillText = null;
        [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("読む込むスキル説明テキストが書き込まれている.txtファイル")] TextAsset[] m_skillTextAsset = { null };

        //ウィンドウを表示しているかどうか
        bool m_isDisplay = false;

        //前回選択されたウィンドウ
        string m_selectBefore = string.Empty;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        public void  DisplayWindow(string character)
        {
            //最後に押されたボタンが今回押されたボタンと同じ場合
            if(m_selectBefore == character)
            {
                //すでに表示されているとき、
                if(m_isDisplay)
                {
                    //ウィンドウを閉じる
                    WindowClose();

                    return;
                }
            }

            //ウィンドウを開く
            WindowOpen(character);
        }

        //ウィンドウを開く処理
        void WindowOpen(string character)
        {
            //ウィンドウを開く
            m_window2D.SetActive(true);
            m_window3D.SetActive(true);

            switch (character)
            {
                case "TankWindow":
                    //ボタンテキスト
                    m_buttonText[0].SetActive(true);
                    m_buttonText[1].SetActive(false);
                    //ボタンオブジェクト
                    m_buttonObject[0].SetActive(true);
                    m_buttonObject[1].SetActive(false);
                    //
                    m_infoTankSkillText[0].enabled = true;
                    m_infoTankSkillText[1].enabled = false;
                    //タイトルテキストを更新する
                    m_titleText.text = "TANK";
                    break;

                case "SkillWindow":
                    //ボタンテキスト
                    m_buttonText[0].SetActive(false);
                    m_buttonText[1].SetActive(true);
                    //ボタンオブジェクト
                    m_buttonObject[0].SetActive(false);
                    m_buttonObject[1].SetActive(true);
                    //
                    m_infoTankSkillText[0].enabled = false;
                    m_infoTankSkillText[1].enabled = true;
                    //タイトルテキストを更新する
                    m_titleText.text = "SKILL";
                    break;
            }

            //プレイヤー等が動けないようにする
            m_saveData.GetSetmActiveGameTime = false;
            //表示している状態にする
            m_isDisplay = true;

            //最後に押されたボタンを今回押されたボタンに更新
            m_selectBefore = character;
        }

        //ウィンドウを閉じる処理
        void WindowClose()
        {
            //ウィンドウを閉じる
            m_window2D.SetActive(false);
            m_window3D.SetActive(false);

            //プレイヤー等が動けるようにする
            m_saveData.GetSetmActiveGameTime = true;
            //表示していない状態にする
            m_isDisplay = false;
        }

        public void UpdateTankSkillInfo(string character)
        {
            //説明文更新

            //タンク
            if (character.Contains("TANK"))
            {
                //選択しているタンクのステータス
                TankStatus tankStatus = m_tankDataBase.GetTankLists()[int.Parse(Regex.Replace(character, @"[^0-9]", string.Empty)) - 1];
                m_infoTankSkillText[0].text = $"Tank Speed : {tankStatus.GetTankSpeed()}\nFire Speed : {tankStatus.GetBulletSpeed()}\nRapid Fire : {tankStatus.GetRapidFireNum()}\nSame Time Fire : {tankStatus.GetSameTimeBulletNum()}\nBullet Refrection:{tankStatus.GetBulletRefrectionNum()}";
            }
            //スキル
            else if (character.Contains("SKILL"))
            {
                m_infoTankSkillText[1].text = $"{ m_skillTextAsset[int.Parse(Regex.Replace(character, @"[^0-9]", string.Empty)) - 1].text}";
            }
        }
    }
}