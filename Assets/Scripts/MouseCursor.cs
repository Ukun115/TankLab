using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseCursor : Photon.Pun.MonoBehaviourPun
{
    Plane plane = new Plane();
    float distance = 0;

    SaveData m_saveData = null;

    void Start()
    {
        //plane.SetNormalAndPosition(Vector3.back, transform.localPosition);

        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    // Update is called once per frame
    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            return;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {
            var lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);
        }
    }
}
