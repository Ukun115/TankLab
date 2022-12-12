using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

/// <summary>
/// プレイヤーの向いている方向を決める処理
/// </summary>
namespace nsTankLab
{
    public class PlayerLookDirection : Photon.Pun.MonoBehaviourPun
    {
        Plane plane = new Plane();
        float distance = 0.0f;
        float m_mouseDistance = 0.0f;

        SaveData m_saveData = null;
        ControllerData m_controllerData = null;

        RectTransform m_cursorPosition = null;

        Camera m_camera = null;

        int m_playerNum = 0;

        void Start()
        {
            //コンポーネント取得まとめ
            GetComponents();

            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            m_camera = Camera.main;


            m_playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", string.Empty));
        }

        void Update()
        {
            //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
            if (m_saveData.GetSetIsOnline && SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            //ゲーム進行が止まっているときは実行しない
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            var ray = m_camera.ScreenPointToRay(DecideRayTarget());
            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if (plane.Raycast(ray, out distance))
            {
                var lookPoint = ray.GetPoint(distance);
                m_mouseDistance = Vector3.Distance(lookPoint, transform.position);
                if(m_mouseDistance > 1.2f)
                {
                    transform.LookAt(lookPoint);
                }
            }
            //射出方向のデバック表示
            Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.red);
        }

        Vector3 DecideRayTarget()
        {
           // ゲームパッドが接続されていたらゲームパッドでの操作
           if (m_controllerData.GetGamepad(m_playerNum) is not null)
           {
                if(m_cursorPosition is null)
                {
                    m_cursorPosition = GameObject.Find($"{m_playerNum}PCursor").GetComponent<RectTransform>();
                }

               return m_cursorPosition.position;
           }
           else
           {
               return Mouse.current.position.ReadValue();
           }
        }

        //コンポーネント取得
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}