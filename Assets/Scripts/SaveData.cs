using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;

/// <summary>
/// ユーザーのセーブデータを管理するスクリプト
/// </summary>
namespace nsTankLab
{
    public class SaveData : MonoBehaviour
    {
        [SerializeField] SoundManager m_soundManager = null;

        //オンラインモードかどうか
        bool m_isOnline = false;

        //選択されたゲームモード
        string m_selectGameMode = string.Empty;

        //プレイヤー番号
        int m_playerNum = 0;

        //現在のステージ番号
        int m_nowStageNum = 1;
        //合計ステージ数
        const int TOTAL_STAGE_NUM = 10;

        //現在の体力(チャレンジモードのみで使用)
        int m_hitPoint = 3;

        //スター
        bool[,] m_star = new bool[4,2];

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

        //ゲームの進む時間がアクティブかどうか
        bool m_activeGameTime = true;

        public static GameObject m_instanceSaveData = null;

        void Awake()
        {
            CheckInstance();
        }

        void Start()
        {
            //60fpsで固定する。
            Application.targetFrameRate = 60;

            //スターbool変数の初期化
            for(int playerNum = 0;playerNum < 4;playerNum++)
            {
                for (int starNum = 0; starNum < 2; starNum++)
                {
                    m_star[playerNum,starNum] = false;
                }
            }
        }

        //ゲームの進む時間がアクティブかどうかプロパティ
        public bool GetSetmActiveGameTime
        {
            //ゲッター
            get { return m_activeGameTime; }
            //セッター
            set { m_activeGameTime = value; }
        }

        //選択されたゲームモードプロパティ
        public string GetSetSelectGameMode
        {
            //ゲッター
            get { return m_selectGameMode; }
            //セッター
            set { m_selectGameMode = value; }
        }

        //選択されたステージ番号プロパティ
        public int GetSetSelectStageNum
        {
            //ゲッター
            get { return m_nowStageNum; }
            //セッター
            set { m_nowStageNum = value; }
        }

        //合計ステージ数ゲッター
        public int GetTotalStageNum()
        {
            return TOTAL_STAGE_NUM;
        }

        //次のステージ番号に設定
        public void NextStageNum()
        {
            m_nowStageNum++;
        }

        //選択されたタンク名ゲッター
        public string GetSelectTankName(int playerNum)
        {
            return m_selectTankName[playerNum];
        }
        //選択されたタンク名セッター
        public void SetSelectTankName(int playerNum, string tankNum)
        {
            m_selectTankName[playerNum-1] = $"Tank{(int.Parse(Regex.Replace(tankNum, @"[^0-9]", string.Empty)))}";

            //選択されたタンク番号を保存
            if (m_selectTankName[playerNum - 1] is not null)
            {
                m_selectTankNum[playerNum - 1] = int.Parse(Regex.Replace(tankNum, @"[^0-9]", string.Empty))-1;
            }
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
            if (m_selectSkillName[playerNum - 1] is not null)
            {
                m_selectSkillNum[playerNum - 1] = int.Parse(Regex.Replace(skillNum, @"[^0-9]", string.Empty))-1;
            }
        }
        //選択されたタンク番号ゲッター
        public int GetSelectTankNum(int playerNum)
        {
            return m_selectTankNum[playerNum];
        }
        //選択されたスキル番号ゲッター
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

        //体力プロパティ
        public int GetSetHitPoint
        {
            //ゲッター
            get { return m_hitPoint; }
            //セッター
            set { m_hitPoint = value; }
        }

        //スターセッター
        public void ActiveStar(int playerNum,int starNum)
        {
            m_star[playerNum,starNum] = true;
        }
        //スターゲッター
        public bool GetStar(int playerNum, int starNum)
        {
            return m_star[playerNum,starNum];
        }

        //セーブデータを初期状態に戻す処理
        public void SaveDataInit()
        {
            //オンラインモードかどうか
            m_isOnline = false;
            //選択されたゲームモード
            m_selectGameMode = string.Empty;
            //プレイヤー番号
            m_playerNum = 0;
            //現在のステージ番号
            m_nowStageNum = 1;
            //選択タンク名
            m_selectTankName = new string[4];
            //選択タンク番号
            m_selectTankNum = new int[4];
            //選択スキル名
            m_selectSkillName = new string[4];
            //選択スキル番号
            m_selectSkillNum = new int[4];
            //入力されたパスワード
            m_inputPassword = "----";
            //ゲームの進む時間がアクティブかどうか
            m_activeGameTime = false;
            //体力を戻す
            m_hitPoint = 3;
            //スターbool変数の初期化
            for (int playerNum = 0; playerNum < 4; playerNum++)
            {
                for (int starNum = 0; starNum < 2; starNum++)
                {
                    m_star[playerNum,starNum] = false;
                }
            }

            //BGMを初期化
            m_soundManager.PlayBGM("OutGameSceneBGM");
        }

        public void InitSelectTankAndSkill()
        {
            //選択タンク名
            m_selectTankName = new string[4];
            //選択タンク番号
            m_selectTankNum = new int[4];
            //選択スキル名
            m_selectSkillName = new string[4];
            //選択スキル番号
            m_selectSkillNum = new int[4];
        }

        //アプリケーションが終了する前に呼び出される関数
        //static変数はアプリケーションが終了されても初期化されず、ずっと残り続けるため、手動で初期化する
        void OnApplicationQuit()
        {
            m_instanceSaveData = null;
        }

        //シングルトンパターン
        //インスタンスチェック
        void CheckInstance()
        {
            if (m_instanceSaveData is null)
            {
                m_instanceSaveData = gameObject;

                //シーン遷移してもこのオブジェクトは破棄されずに保持したままにする
                DontDestroyOnLoad(gameObject);

                //デバック
                Debug.Log("SaveDataオブジェクトのインスタンスは未登録です。登録します。");

            }
            else
            {
                Destroy(gameObject);

                //デバック
                Debug.Log("SaveDataオブジェクトのインスタンスは登録済です。削除します。");
            }
        }
    }
}
//#error version
//↑C#言語バージョンの確認