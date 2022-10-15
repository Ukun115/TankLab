using UnityEngine;

/// <summary>
/// 機体の足跡を表示させる処理
/// </summary>
public class DrawFootPrint : MonoBehaviour
{
    //足跡プレファブ
    [SerializeField] GameObject m_footPrintPrefab;
    //タイマー
    float m_timer = 0;
    //足跡を表示させる間隔
    [SerializeField] float m_drawInterval = 0;
    //足跡ゲームオブジェクトを格納するゲームオブジェクト
    GameObject m_footPrintsBox = null;

    void Start()
    {
        m_footPrintsBox = GameObject.Find("Footprints");
    }

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_drawInterval)
        {
            //足跡を生成する処理
            InstantiateFootPrint();
        }
    }

    //足跡を生成する処理
    void InstantiateFootPrint()
    {
        //足跡オブジェクトを生成
        GameObject footPrintObject = Instantiate(m_footPrintPrefab, new Vector3(transform.position.x, -0.45f, transform.position.z), transform.rotation);
        //足跡オブジェクトは大量に生成され、
        //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
        footPrintObject.transform.parent = m_footPrintsBox.transform;

        m_timer = 0;
    }
}