using UnityEngine;

/// <summary>
/// タンクの足跡を表示させる処理
/// </summary>
namespace nsTankLab
{
    public class DrawFootPrint : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("足跡プレファブオブジェクト")] GameObject m_footPrintPrefab = null;
        [SerializeField, TooltipAttribute("足跡を生成するタンクのトランスフォーム")] Transform m_tankTransform = null;

        //足跡表示間隔
        const float DRAW_INTERVAL = 0.5f;

        //足跡ゲームオブジェクトを格納するゲームオブジェクト
        Transform m_footPrintsBox = null;

        SaveData m_saveData = null;

        //タイマー
        float m_timer = 0.0f;

        void Start()
        {
            m_footPrintsBox = GameObject.Find("Footprints").GetComponent<Transform>();

            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //ゲーム進行が止まっているときは実行しない
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            m_timer+=Time.deltaTime;

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
                new Vector3(m_tankTransform.position.x, -0.45f,m_tankTransform.position.z),
                m_tankTransform.rotation
                );

            //足跡オブジェクトは大量に生成され、
            //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
            footPrintObject.transform.parent = m_footPrintsBox;
        }
    }
}