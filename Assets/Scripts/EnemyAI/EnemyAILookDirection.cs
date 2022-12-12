using UnityEngine;

/// <summary>
/// �GAI�̌����Ă�����������߂鏈��
/// </summary>
namespace nsTankLab
{
    public class EnemyAILookDirection : MonoBehaviour
    {
        //��]���x
        float m_rotationSpeed = 0.5f;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //�Q�[���i�s���~�܂��Ă���Ƃ��͎��s���Ȃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //���܂ɉ�]����������t�ɂ���
            if (Random.Range(1, 250) == 1)
            {
                m_rotationSpeed *= -1f;
            }

            //��������]������
            transform.Rotate(0.0f, m_rotationSpeed, 0.0f);
        }
    }
}