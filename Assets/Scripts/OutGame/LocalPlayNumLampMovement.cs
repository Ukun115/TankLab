using UnityEngine;

/// <summary>
/// ���[�J���}�b�`�̐l�������߂��������v�őI�����Ă���l���𗝉����₷������X�N���v�g
/// </summary>
namespace nsTankLab
{
    public class LocalPlayNumLampMovement : MonoBehaviour
    {
        [SerializeField] SaveData m_saveData = null;

        Vector3[] m_lampPosition = new []{new Vector3(0.383f, 0.0f, 0.389f),new Vector3(2.383f, 0.0f, 0.389f), new Vector3(4.433f, 0.0f, 0.389f) };

        void Update()
        {
            //�l���ɂ���ă����v�̈ʒu��ύX
            gameObject.transform.position = m_lampPosition[m_saveData.GetSetLocalMatchPlayNum-2];
        }
    }
}