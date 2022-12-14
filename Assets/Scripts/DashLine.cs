using UnityEngine;

/// <summary>
/// ダッシュラインを生成する処理
/// </summary>
namespace nsTankLab
{
public class DashLine : MonoBehaviour
{
    [SerializeField] GameObject m_dashLinePrefab = null;

    void Start()
    {
        GameObject m_dashLineObject = Instantiate(
            m_dashLinePrefab,
            transform.position,
            new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
            );
        m_dashLineObject.transform.parent = transform;
            //タンクの色に合わせてダッシュラインの色を変更
            m_dashLineObject.GetComponent<Renderer>().material.color = transform.root.GetComponent<Renderer>().material.color;
    }
}
}