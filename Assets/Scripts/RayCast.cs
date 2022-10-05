using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    Transform[] m_childrenObject; // 子オブジェクト達を入れる配列

    //ブロックボタンで使われているマテリアル
    [SerializeField] Material[] m_blockButtonMaterial = null;

    GameObject m_rayHitObject = null;

    bool m_isColorChange = false;

    void Update()
    {
        //Rayを生成
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Rayを投射
        if (Physics.Raycast(ray, out hit))
        {

            switch (SceneManager.GetActiveScene().name)
            {
                //タイトルシーン、名前決めシーン、パスワード入力画面の時、
                case "TitleScene":
                case "DecideNameScene":
                case "InputPasswordScene":
                    if (hit.collider.gameObject != m_rayHitObject)
                    {
                        if (m_rayHitObject != null && m_childrenObject != null)
                        {
                            if (m_isColorChange)
                            {
                                //前回マウスカーソルが当たっていたブロックボタンオブジェクトをもとの色に戻す
                                for (int i = 0; i < m_childrenObject.Length; i++)
                                {
                                    //割り当てられているマテリアルによって変更先のマテリアルを決定する
                                    if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Normal (Instance)")
                                    {
                                        m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[0];
                                    }
                                    else if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Dark (Instance)")
                                    {
                                        m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[1];
                                    }
                                }
                                m_isColorChange = false;
                            }
                        }

                        //マウスカーソルがあっているオブジェクトがブロックボタンのとき、
                        if (hit.collider.CompareTag("BlockButton"))
                        {
                            // 子オブジェクト達を入れる配列の初期化
                            m_childrenObject = new Transform[hit.collider.gameObject.transform.childCount];

                            //子オブジェクトを取得
                            for (int i = 0; i < hit.collider.gameObject.transform.childCount; i++)
                            {
                                m_childrenObject[i] = hit.collider.gameObject.transform.GetChild(i); // GetChild()で子オブジェクトを取得
                            }

                            //ブロックボタンの色(マテリアル)を変更する
                            for (int i = 0; i < m_childrenObject.Length; i++)
                            {
                                //割り当てられているマテリアルによって変更先のマテリアルを決定する
                                if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Light (Instance)")
                                {
                                    m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[1];
                                }
                                else if (m_childrenObject[i].gameObject.GetComponent<Renderer>().material.name == "Gray_Normal (Instance)")
                                {
                                    m_childrenObject[i].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[2];
                                }
                            }

                            m_isColorChange = true;
                        }
                        //接触しているオブジェクトを保存
                        m_rayHitObject = hit.collider.gameObject;
                    }
                    break;

                //ステージ選択シーンの時、
                case "SelectStageScene":
                    //カーソルポジションを移動させる
                    GameObject.Find("SceneManager").GetComponent<DecideStage>().SetCursorPosition(hit.collider.name);
                    break;
            }

            //左クリックされたとき、
            if (Input.GetMouseButtonDown(0))
            {
                //BlockButtonタグだったら処理を行う
                if (hit.collider.CompareTag("BlockButton"))
                {
                    //現在のシーンによって処理を変更
                    switch(SceneManager.GetActiveScene().name)
                    {
                        //タイトルシーン
                        case "TitleScene":
                            //押されたボタンの文字を渡す
                            GameObject.Find("SceneManager").GetComponent<DecideGameMode>().SetCharacter(hit.collider.name);
                            break;

                            //名前決めシーン
                        case "DecideNameScene":
                            //押されたボタンの文字を渡す
                            GameObject.Find("SceneManager").GetComponent< DecidePlayerName>().SetCharacter(hit.collider.name);
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
            }
        }
    }
}