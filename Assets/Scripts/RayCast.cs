using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// マウスカーソルのRayを飛ばしてRayにヒットしたオブジェクトを判別する処理
/// </summary>
public class RayCast : MonoBehaviour
{
    //子オブジェクト達を入れる配列
    Transform[] m_childrenObject;

    //ブロックボタンで使われているマテリアル
    [SerializeField] Material[] m_blockButtonMaterial = null;

    //Rayがヒットしたオブジェクト
    GameObject m_rayHitObject = null;

    //カラーチェンジができるかどうか
    bool m_ableColorChange = false;

    //シーンスイッチャースクリプト
    SceneSwitcher m_sceneSwitcher = null;

    Camera m_camera = null;

    void Start()
    {
        m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();

        m_camera = Camera.main;
    }

    void Update()
    {
        //Rayを生成
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
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
            if (m_rayHitObject != null && m_childrenObject != null)
            {
                if (m_ableColorChange)
                {
                    //前回マウスカーソルが当たっていたブロックボタンオブジェクトをもとの色に戻す
                    //マテリアルのカラーチェンジ処理
                    ChangeMaterialColor("Gray_Normal (Instance)", "Gray_Dark (Instance)", 0, false);
                }
            }

            //マウスカーソルがあっているオブジェクトがブロックボタンのとき、
            if (hit.collider.CompareTag("BlockButton") || hit.collider.CompareTag("BackButton"))
            {
                // 子オブジェクト達を入れる配列の初期化
                m_childrenObject = new Transform[hit.collider.gameObject.transform.childCount];

                //子オブジェクトを取得
                for (int i = 0; i < hit.collider.gameObject.transform.childCount; i++)
                {
                    m_childrenObject[i] = hit.collider.gameObject.transform.GetChild(i); // GetChild()で子オブジェクトを取得
                }

                //マテリアルのカラーチェンジ処理
                ChangeMaterialColor("Gray_Light (Instance)", "Gray_Normal (Instance)", 1, true);
            }
            //接触しているオブジェクトを保存
            m_rayHitObject = hit.collider.gameObject;

            //ステージ選択シーンの時、
            if (SceneManager.GetActiveScene().name == "SelectStageScene")
            {
                //カーソルポジションを移動させる
                GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCursorPosition(hit.collider.name);
            }
        }

        //左クリックされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //左クリックされたときの処理
            FireLeftClick(hit);
        }
    }

    //左クリックされたときの処理
    void FireLeftClick(RaycastHit hit)
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
                    //押されたボタンの文字を渡す
                    GameObject.Find("SceneManager").GetComponent<DecideTank>().SetCharacter(hit.collider.name);
                    break;

                //ステージ選択シーン
                case "SelectStageScene":
                    //押されたボタンの文字を渡す
                    GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCharacter(hit.collider.name);
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

    //マテリアルのカラーチェンジ処理
    void ChangeMaterialColor(string materialName1,string materialName2,int materialNum,bool ableColorChange)
    {
        //ブロックボタンの色(マテリアル)を変更する
        for (int i = 0; i < m_childrenObject.Length; i++)
        {
            //割り当てられているマテリアルによって変更先のマテリアルを決定する
            if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == materialName1)
            {
                m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum];
            }
            else if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == materialName2)
            {
                m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum+1];
            }
        }
        m_ableColorChange = ableColorChange;
    }
}