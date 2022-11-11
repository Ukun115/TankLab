using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

/// <summary>
/// ユーザーのセーブデータを管理するスクリプト
/// </summary>
namespace nsTankLab
{
public class SaveData : MonoBehaviour
{
    //オンラインモードかどうか
    bool m_isOnline = false;

    //選択されたゲームモード
    string m_selectGameMode = "";

    //プレイヤー番号
    int m_playerNum = 0;

        //現在のステージ番号
        int m_nowStageNum = 1;
        //合計ステージ数
     const int TOTAL_STAGE_NUM = 2;

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

    [SerializeField, TooltipAttribute("プレイヤー名テキスト")] TextMeshProUGUI m_playerNameText = null;

    [SerializeField] Texture2D m_handCursor = null;

       public static GameObject m_instanceSaveData = null;

    void Awake()
    {
            CheckInstance();
    }

        void Start()
        {
            //60fpsで固定する。
            Application.targetFrameRate = 60;

            //カーソル画像をデフォルトから変更
            Cursor.SetCursor(m_handCursor, new Vector2(m_handCursor.width / 2, m_handCursor.height / 2), CursorMode.Auto);

            //以前のプレイで名前が登録されていた場合、
            if (PlayerPrefs.HasKey("PlayerName"))
            {
                //登録されていた名前を表示させる
                m_playerNameText.text = PlayerPrefs.GetString("PlayerName");

                Debug.Log($"<color=yellow>ユーザー名:{m_playerNameText.text}</color>");
            }
            else
            {
                m_playerNameText.text = "NoName";
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
    //選択されたタンク番号ゲッター
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

        //セーブデータを初期状態に戻す処理
        public void SaveDataInit()
        {
            //オンラインモードかどうか
            m_isOnline = false;
            //選択されたゲームモード
            m_selectGameMode = "";
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
            m_activeGameTime = true;
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