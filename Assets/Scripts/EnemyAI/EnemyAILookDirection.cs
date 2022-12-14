using UnityEngine;

/// <summary>
/// 敵AIの向いている方向を決める処理
/// </summary>
namespace nsTankLab
{
    public class EnemyAILookDirection : MonoBehaviour
    {
        //回転速度
        float m_rotationSpeed = 0.5f;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //ゲーム進行が止まっているときは実行しない
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //たまに回転する方向を逆にする
            if (Random.Range(1, 250) == 1)
            {
                m_rotationSpeed *= -1f;
            }

            //向きを回転させる
            transform.Rotate(0.0f, m_rotationSpeed, 0.0f);
        }
    }
}