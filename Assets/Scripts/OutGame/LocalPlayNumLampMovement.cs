using UnityEngine;

/// <summary>
/// ローカルマッチの人数を決めた時ランプで選択している人数を理解しやすくするスクリプト
/// </summary>
namespace nsTankLab
{
    public class LocalPlayNumLampMovement : MonoBehaviour
    {
        [SerializeField] SaveData m_saveData = null;

        Vector3[] m_lampPosition = new []{new Vector3(0.383f, 0.0f, 0.389f),new Vector3(2.383f, 0.0f, 0.389f), new Vector3(4.433f, 0.0f, 0.389f) };

        void Update()
        {
            //人数によってランプの位置を変更
            gameObject.transform.position = m_lampPosition[m_saveData.GetSetLocalMatchPlayNum-2];
        }
    }
}