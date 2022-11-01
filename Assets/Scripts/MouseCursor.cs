using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseCursor : Photon.Pun.MonoBehaviourPun
{
    Plane plane = new Plane();
    float distance = 0;
    float m_mouceDistance = 0.0f;

    SaveData m_saveData = null;
    Camera m_mainCamera;

    void Start()
    {
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

		var ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        //plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
		{
			var lookPoint = ray.GetPoint(distance);
            m_mouceDistance = Vector3.Distance(lookPoint, transform.position);
            if (m_mouceDistance > 1.2f)
            {
                transform.LookAt(lookPoint);
            }
		}

	}
}
