using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// ダッシュラインを生成する処理
/// </summary>
namespace nsTankLab
{
public class DashLine : MonoBehaviourPun
{
    [SerializeField] GameObject m_dashLinePrefab = null;

    void Start()
    {
            //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene && !photonView.IsMine)
            {
                return;
            }

            GameObject m_dashLineObject = Instantiate(
            m_dashLinePrefab,
            transform.position,
            new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
            );
        m_dashLineObject.transform.parent = transform;
            //タンクの色に合わせてダッシュラインの色を変更
            m_dashLineObject.GetComponent<Renderer>().material.color = transform.root.GetComponent<MeshRenderer>().material.color;
    }
}
}