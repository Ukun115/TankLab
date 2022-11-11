using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

/// <summary>
/// マウスカーソルのRayを飛ばしてRayにヒットしたオブジェクトを判別する処理
/// </summary>
namespace nsTankLab
{
public class RayCast : MonoBehaviour
{
    //Rayがヒットしたオブジェクト
    GameObject m_rayHitObject = null;

    ButtonMaterialChange m_buttonMaterialChange = null;

    //シーンスイッチャースクリプト
    SceneSwitcher m_sceneSwitcher = null;

    Camera m_camera = null;

    Vector3 m_rayPoint = Vector3.zero;

    [SerializeField, TooltipAttribute("カーソル画像の位置")] Transform m_cursorImagePosition = null;
    [SerializeField, TooltipAttribute("プレイヤー番号"), Range(1,4)]int m_playerNum = 1;

    SaveData m_saveData = null;
    SoundManager m_soundManager = null;
    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("カーソル画像オブジェクト")] GameObject m_cursorObject = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        m_buttonMaterialChange = GetComponent<ButtonMaterialChange>();

        m_camera = Camera.main;
    }

    void Update()
    {
        // ゲームパッドが接続されていたらゲームパッド操作
        if (m_controllerData.GetIsConnectedController(m_playerNum))
        {
            m_rayPoint = m_cursorImagePosition.position;

            //カーソル画像を表示
            m_cursorObject.SetActive(true);
        }
        //ゲームパッドが接続されていなかったらマウス操作
        else
        {
            m_rayPoint = Input.mousePosition;

            //カーソル画像を非表示
            m_cursorObject.SetActive(false);
        }

        //Rayを生成
        Ray ray = m_camera.ScreenPointToRay(m_rayPoint);
        RaycastHit hit;

        //Rayを投射
        if (Physics.Raycast(ray, out hit))
        {
            //Rayがコライダーにあたったときの処理
            RayHit(hit);
        }
    }

    //Rayがコライダーにあたったときの処理
    void RayHit(RaycastHit hit)
    {
        if (hit.collider.gameObject != m_rayHitObject)
        {
            //ボタンのマテリアルを変更する処理
            ChangeMaterial(hit);

            //接触しているオブジェクトを保存
            m_rayHitObject = hit.collider.gameObject;

            //ステージ選択シーンの時、
            if (SceneManager.GetActiveScene().name == "SelectStageScene")
            {
                //カーソルポジションを移動させる
                GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCursorPosition(hit.collider.name);
            }
        }

        //ボタンが押されたとき、
        FireButton(hit);
    }

    //ボタンのマテリアルを変更する処理
    void ChangeMaterial(RaycastHit hit)
    {
        if (m_rayHitObject != null)
        {
            m_buttonMaterialChange.ReturnChangeMaterial();
        }
        //マウスカーソルがあっているオブジェクトがブロックボタンのとき、
        if (hit.collider.CompareTag("BlockButton") || hit.collider.CompareTag("BackButton"))
        {
            m_buttonMaterialChange.ChangeMaterial(hit);

                //カーソルヒットSE再生
                m_soundManager.PlaySE("CursorHitSE");
            }
    }

    //ボタンが押されたときの処理
    void FireButton(RaycastHit hit)
    {
        //ゲームパッドが接続されていたらゲームパッド操作
        if (m_controllerData.GetIsConnectedController(m_playerNum))
        {
            //RBボタンがおされたとき、
            if (Input.GetButtonDown($"{m_playerNum}PJoystickRB"))
            {
                //文字を渡す
                PassCharacter(hit);
            }
        }
        //ゲームパッドが接続されていなければマウスでの操作
        else
        {
            //左クリックされたとき、
            if (Input.GetMouseButtonDown(0))
            {
                //文字を渡す
                PassCharacter(hit);
            }
        }
    }

    //文字を渡す処理
    void PassCharacter(RaycastHit hit)
    {
        //BlockButtonタグだったら処理を行う
        if (hit.collider.CompareTag("BlockButton"))
        {
            //現在のシーンによって処理を変更
            switch (SceneManager.GetActiveScene().name)
            {
                //タイトルシーン
                case "TitleScene":
                    //押されたボタンの文字を渡す
                    GameObject.Find("SceneManager").GetComponent<DecideGameMode>().SetCharacter(hit.collider.name);
                    break;

                //名前決めシーン
                case "DecideNameScene":
                    //押されたボタンの文字を渡す
                    GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter(hit.collider.name);
                    break;
                //パスワード入力画面
                case "InputPasswordScene":
                    //押されたボタンの文字を渡す
                    GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter(hit.collider.name);
                    break;
                //タンク選択シーン
                case "SelectTankScene":
                    if (m_saveData.GetSetSelectGameMode == "LOCALMATCH")
                    {
                        //押されたボタンの文字を渡す
                        GameObject.Find("SceneManager").GetComponent<DecideTank>().SetCharacter(m_playerNum, hit.collider.name);
                    }
                    else
                    {
                        //押されたボタンの文字を渡す
                        GameObject.Find("SceneManager").GetComponent<DecideTank>().SetCharacter(1, hit.collider.name);
                    }
                    break;

                //ステージ選択シーン
                case "SelectStageScene":
                    //押されたボタンの文字を渡す
                    GameObject.Find("SceneManager").GetComponent<DecideStage>().SetStageNum(int.Parse(Regex.Replace(hit.collider.name, @"[^0-9]", "")));
                    break;

                    //現在のチャレンジ数カウントシーン
                    case "ChallengeNowNumCountScene":
                        //チャレンジゲームシーンに遷移
                        m_sceneSwitcher.StartTransition("ChallengeGameScene");
                        break;
            }
                switch (SceneManager.GetActiveScene().name)
                {
                    case "TitleScene":
                        if (GameObject.Find("SceneManager").GetComponent<DecideGameMode>().GetNoGood())
                        {
                            //ダメSE再生
                            m_soundManager.PlaySE("NoGoodSE");
                        }
                        else
                        {
                            //選択SE再生
                            m_soundManager.PlaySE("SelectSE");
                        }
                        break;

                    case "DecideNameScene":
                        if (GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().GetNoGood())
                        {
                            //ダメSE再生
                            m_soundManager.PlaySE("NoGoodSE");
                        }
                        else
                        {
                            //選択SE再生
                            m_soundManager.PlaySE("SelectSE");
                        }
                        break;

                        case "InputPasswordScene":
                        if (GameObject.Find("SceneManager").GetComponent<DecidePassword>().GetNoGood())
                        {
                            //ダメSE再生
                            m_soundManager.PlaySE("NoGoodSE");
                        }
                        else
                        {
                            //選択SE再生
                            m_soundManager.PlaySE("SelectSE");
                        }
                        break;

                    default:
                        //選択SE再生
                        m_soundManager.PlaySE("SelectSE");
                        break;
                }
            }

        //BackButtonタグだったら処理を行う
        if (hit.collider.CompareTag("BackButton"))
        {
            //1つ前のシーンに戻る
            m_sceneSwitcher.BackScene();
        }
    }
}
}