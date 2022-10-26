using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class MouseCursor : Photon.Pun.MonoBehaviourPun
{
    Plane plane = new Plane();
    float distance = 0;

    SaveData m_saveData = null;
    ControllerData m_controllerData = null;

    [SerializeField, TooltipAttribute("カーソル画像の位置")] Transform m_cursorPosition = null;

    [SerializeField, TooltipAttribute("大砲の基点トランスフォーム")] Transform m_cannonPivotTransform = null;

    Vector3 rayTarget;

    void Start()
    {
        //plane.SetNormalAndPosition(Vector3.back, transform.localPosition);

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
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

        var ray = Camera.main.ScreenPointToRay(rayTarget);
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {
            var lookPoint = ray.GetPoint(distance);
            m_cannonPivotTransform.LookAt(lookPoint);
        }
    }
}
