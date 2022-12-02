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

        void Update()
        {
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