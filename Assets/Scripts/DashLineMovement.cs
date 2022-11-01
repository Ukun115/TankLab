using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// ダッシュライン移動処理
/// </summary>
public class DashLineMovement : MonoBehaviour
{
    LineRenderer line = null;
    Camera m_mainCamera;
    Plane plane = new Plane();
    float distance = 0;
    Vector3 rayTarget = Vector3.zero;
    ControllerData m_controllerData = null;

    Transform m_cursorPosition = null;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        m_mainCamera = Camera.main;
        plane.SetNormalAndPosition(Vector3.up, new Vector3(transform.position.x, 4.0f, transform.position.z - 3));

        m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

        m_cursorPosition = GameObject.Find(this.transform.root.name+"Cursor").GetComponent<Transform>();
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

        var ray = m_mainCamera.ScreenPointToRay(rayTarget);
        if (plane.Raycast(ray, out distance))
        {
            var lookPoint = ray.GetPoint(distance);
            line.SetPosition(0, lookPoint);
            line.SetPosition(1, new Vector3(transform.position.x, 4.0f, transform.position.z - 3));
        }
    }
}