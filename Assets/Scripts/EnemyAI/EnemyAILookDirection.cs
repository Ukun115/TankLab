using UnityEngine;

//�GAI�̌����Ă�����������߂鏈��
public class EnemyAILookDirection : MonoBehaviour
{
    //��]���x
    float m_rotationSpeed = 0.5f;

    void Update()
    {
        //���܂ɉ�]����������t�ɂ���
        if (Random.Range(1, 250) == 1)
        {
            m_rotationSpeed *= -1f;
        }

        //��������]������
        this.transform.Rotate(0.0f, m_rotationSpeed, 0.0f);
    }
}
