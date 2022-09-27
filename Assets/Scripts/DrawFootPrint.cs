using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �@�̂̑��Ղ�\�������鏈��
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
            //���ՃI�u�W�F�N�g�𐶐�
            GameObject footPrintObject = Instantiate(m_footPrintPrefab, new Vector3(transform.position.x, transform.position.y-0.49f, transform.position.z), transform.rotation);
            //���ՃI�u�W�F�N�g�͑�ʂɐ�������A
            //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
            footPrintObject.transform.parent = m_parentObject.transform;
        }
    }
}