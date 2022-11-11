using UnityEngine;
using System;

/// <summary>
/// ���O�C���f�[�^
/// </summary>
namespace nsTankLab
{
public class LoginData : MonoBehaviour
{
    //���O�C���^�C�v
    enum EnLoginType
    {
        enFirstUserLogin,  //���񃍃O�C��
        enTodayLogin,      //���O�C��
        enAlreadyLogin,    //���O�C����
        enErrorLogin       //�s�����O�C��
    }

    //���񃍃O�C���̓��t�f�[�^
    int m_yearMonthDayData = 0;
    int m_hourMinuteData = 0;
    //�O�񃍃O�C���̓��t�f�[�^
    int m_lastYearMonthDayDate = 0;
    int m_lastHourMinuteData = 0;
    //���O�C���^�C�v
    EnLoginType m_judgeType;

        void Start()
    {
                //���t�𐔒l���@2022�N9��1������20220901�ɂȂ�
                m_yearMonthDayData = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                m_hourMinuteData = DateTime.Now.Hour * 100 + DateTime.Now.Minute;

                //�O�񃍃O�C�����̓��t�f�[�^�����[�h �f�[�^���Ȃ��ꍇ��enFirstUserLogin��0
                m_lastYearMonthDayDate = PlayerPrefs.GetInt("LastGetDate", (int)EnLoginType.enFirstUserLogin);
                m_lastHourMinuteData = PlayerPrefs.GetInt("LastHourMinuteGetDate", (int)EnLoginType.enFirstUserLogin);

                //�O�񃍃O�C���̓��t�f�[�^���f�o�b�N
                Debug.Log($"<color=yellow>�O��̃��O�C���i���j:{m_lastYearMonthDayDate}</color>");
                //ex)5:48�̏ꍇ548�ɂȂ邩��D4���g�p���邱�Ƃ�0548�ɂ��邱�Ƃ��ł��Ă���B
                Debug.Log($"<color=yellow>�O��̃��O�C���i���j:{m_lastHourMinuteData.ToString("D4")}</color>");

                //�O��ƍ���̓��t�f�[�^��r

                if (m_lastYearMonthDayDate < m_yearMonthDayData)//���t���i��ł���ꍇ
                {
                    //���O�C��
                    m_judgeType = EnLoginType.enTodayLogin;
                }
                else if (m_lastYearMonthDayDate == m_yearMonthDayData)//���t���i��ł��Ȃ��ꍇ
                {
                    //���O�C����
                    m_judgeType = EnLoginType.enAlreadyLogin;
                }
                else if (m_lastYearMonthDayDate > m_yearMonthDayData)//���t���t�]���Ă���ꍇ
                {
                    //�s�����O�C��
                    m_judgeType = EnLoginType.enErrorLogin;
                }

                switch (m_judgeType)
                {
                    //���O�C���{�[�i�X
                    case EnLoginType.enTodayLogin:

                        //�����O�C���{�[�i�X�@lastDate��0�������Ă����珈�����s
                        if (m_lastYearMonthDayDate == (int)EnLoginType.enFirstUserLogin)
                        {
                            //�����O�C���{�[�i�X����
                            Debug.Log("�����O�C���{�[�i�X");
                        }
                        else
                        {
                            //���ʂ̃��O�C���{�[�i�X����
                            Debug.Log("���ʂ̃��O�C���{�[�i�X");
                        }

                        break;

                    //���łɃ��O�C���ς�
                    case EnLoginType.enAlreadyLogin:
                        //�Ȃɂ����Ȃ�
                        Debug.Log("�{���̓��O�C���ς�");
                        break;

                    //�s�����O�C��
                    case EnLoginType.enErrorLogin:
                        //�s�����O�C�����̏���
                        Debug.Log("�s�����O�C��");
                        break;
                }

                //����擾�������t���Z�[�u
                PlayerPrefs.SetInt("LastGetDate", m_yearMonthDayData);
                PlayerPrefs.SetInt("LastHourMinuteGetDate", m_hourMinuteData);
                PlayerPrefs.Save();
    }
    }
}