using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class DashLine : Photon.Pun.MonoBehaviourPun
{
    [SerializeField] GameObject m_dashLinePrefab = null;
    SaveData m_saveData = null;
    [SerializeField]GameObject m_parentObject = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject m_dashLineObject = Instantiate(
            m_dashLinePrefab,
            transform.localPosition,
            new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
            );
        m_dashLineObject.transform.parent = m_parentObject.transform;

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
        
    }
}
