using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashLineMovement : MonoBehaviour
{
    LineRenderer line = null;
    Camera m_mainCamera;
    Plane plane = new Plane();
    float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        m_mainCamera = Camera.main;
        plane.SetNormalAndPosition(Vector3.up, new Vector3(transform.position.x, 4.0f, transform.position.z-3));
    }

    // Update is called once per frame
    void Update()
    {
        var ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            var lookPoint = ray.GetPoint(distance);
            line.SetPosition(0, lookPoint);
            line.SetPosition(1, new Vector3(transform.position.x,4.0f,transform.position.z-3));
        }
    }
}
