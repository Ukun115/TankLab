using UnityEngine;

/// <summary>
/// タンクの足跡を表示させる処理
/// </summary>
namespace nsTankLab
{
public class DrawFootPrint : MonoBehaviour
{
    [SerializeField, TooltipAttribute("足跡プレファブオブジェクト")] GameObject m_footPrintPrefab;

    //足跡表示間隔
    const float DRAW_INTERVAL = 45.0f;

    [SerializeField, TooltipAttribute("足跡を生成するタンクのトランスフォーム")] Transform m_tankTransform = null;

    //足跡ゲームオブジェクトを格納するゲームオブジェクト
    GameObject m_footPrintsBox = null;

    //タイマー
    int m_timer = 0;

    void Start()
    {
        m_footPrintsBox = GameObject.Find("Footprints");
    }

    void Update()
    {
        m_timer++;

        if(m_timer > DRAW_INTERVAL)
        {
            //足跡描画
            Draw();
            //タイマー初期化
            m_timer = 0;
        }
    }

    //足跡描画処理
    void Draw()
    {
        //足跡オブジェクトを生成
        GameObject footPrintObject = Instantiate(
            m_footPrintPrefab,
            new Vector3(m_tankTransform.position.x, -0.45f, m_tankTransform.position.z),
            m_tankTransform.rotation
            );

        //足跡オブジェクトは大量に生成され、
        //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
        footPrintObject.transform.parent = m_footPrintsBox.transform;
    }
}
}