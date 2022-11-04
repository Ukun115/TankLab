using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

//プレイヤーの向いている方向を決める処理
public class PlayerLookDirection : Photon.Pun.MonoBehaviourPun
{
    Plane plane = new Plane();
    float distance = 0.0f;
    float m_mouseDistance = 0.0f;

    SaveData m_saveData = null;
    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("カーソル画像の位置")] Transform m_cursorPosition = null;

    Vector3 rayTarget = Vector3.zero;

    Camera m_camera = null;

    void Start()
    {
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

        m_camera = Camera.main;
    }

    void Update()
    {
        // ゲームパッドが接続されていたらゲームパッドでの操作
        if (m_controllerData.GetIsConnectedController(int.Parse(Regex.Replace(this.transform.root.name, @"[^0-9]", ""))))
        {
            rayTarget = m_cursorPosition.position;
        }
        else
        {
            rayTarget = Input.mousePosition;
        }

        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

        var ray = m_camera.ScreenPointToRay(rayTarget);
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
        Debug.DrawRay(this.transform.position, this.transform.forward * 5.0f, Color.red);
    }
}
