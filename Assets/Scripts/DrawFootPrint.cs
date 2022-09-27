using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 機体の足跡を表示させる処理
/// </summary>
public class DrawFootPrint : MonoBehaviour
{
    [SerializeField] GameObject m_footPrintPrefab;
    float m_time = 0;
    [SerializeField] float m_drawInterval = 0;
    [SerializeField] GameObject m_parentObject = null;

    void Update()
    {
        m_time += Time.deltaTime;
        if (m_time > m_drawInterval)
        {
            m_time = 0;
            //足跡オブジェクトを生成
            GameObject footPrintObject = Instantiate(m_footPrintPrefab, new Vector3(transform.position.x, transform.position.y-0.49f, transform.position.z), transform.rotation);
            //足跡オブジェクトは大量に生成され、
            //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
            footPrintObject.transform.parent = m_parentObject.transform;
        }
    }
}