using UnityEngine;
using System;

/// <summary>
/// ログインデータ
/// </summary>
namespace nsTankLab
{
public class LoginData : MonoBehaviour
{
    //ログインタイプ
    enum EnLoginType
    {
        enFirstUserLogin,  //初回ログイン
        enTodayLogin,      //ログイン
        enAlreadyLogin,    //ログイン済
        enErrorLogin       //不正ログイン
    }

    //今回ログインの日付データ
    int m_yearMonthDayData = 0;
    int m_hourMinuteData = 0;
    //前回ログインの日付データ
    int m_lastYearMonthDayDate = 0;
    int m_lastHourMinuteData = 0;
    //ログインタイプ
    EnLoginType m_judgeType;

        void Start()
    {
                //日付を数値化　2022年9月1日だと20220901になる
                m_yearMonthDayData = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                m_hourMinuteData = DateTime.Now.Hour * 100 + DateTime.Now.Minute;

                //前回ログイン時の日付データをロード データがない場合はenFirstUserLoginで0
                m_lastYearMonthDayDate = PlayerPrefs.GetInt("LastGetDate", (int)EnLoginType.enFirstUserLogin);
                m_lastHourMinuteData = PlayerPrefs.GetInt("LastHourMinuteGetDate", (int)EnLoginType.enFirstUserLogin);

                //前回ログインの日付データをデバック
                Debug.Log($"<color=yellow>前回のログイン（日）:{m_lastYearMonthDayDate}</color>");
                //ex)5:48の場合548になるからD4を使用することで0548にすることができている。
                Debug.Log($"<color=yellow>前回のログイン（時）:{m_lastHourMinuteData.ToString("D4")}</color>");

                //前回と今回の日付データ比較

                if (m_lastYearMonthDayDate < m_yearMonthDayData)//日付が進んでいる場合
                {
                    //ログイン
                    m_judgeType = EnLoginType.enTodayLogin;
                }
                else if (m_lastYearMonthDayDate == m_yearMonthDayData)//日付が進んでいない場合
                {
                    //ログイン済
                    m_judgeType = EnLoginType.enAlreadyLogin;
                }
                else if (m_lastYearMonthDayDate > m_yearMonthDayData)//日付が逆転している場合
                {
                    //不正ログイン
                    m_judgeType = EnLoginType.enErrorLogin;
                }

                switch (m_judgeType)
                {
                    //ログインボーナス
                    case EnLoginType.enTodayLogin:

                        //初ログインボーナス　lastDateに0が入っていたら処理実行
                        if (m_lastYearMonthDayDate == (int)EnLoginType.enFirstUserLogin)
                        {
                            //初ログインボーナス処理
                            Debug.Log("初ログインボーナス");
                        }
                        else
                        {
                            //普通のログインボーナス処理
                            Debug.Log("普通のログインボーナス");
                        }

                        break;

                    //すでにログイン済み
                    case EnLoginType.enAlreadyLogin:
                        //なにもしない
                        Debug.Log("本日はログイン済み");
                        break;

                    //不正ログイン
                    case EnLoginType.enErrorLogin:
                        //不正ログイン時の処理
                        Debug.Log("不正ログイン");
                        break;
                }

                //今回取得した日付をセーブ
                PlayerPrefs.SetInt("LastGetDate", m_yearMonthDayData);
                PlayerPrefs.SetInt("LastHourMinuteGetDate", m_hourMinuteData);
                PlayerPrefs.Save();
    }
    }
}