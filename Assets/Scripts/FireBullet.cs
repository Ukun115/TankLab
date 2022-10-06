using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] GameObject m_bulletPrefab = null;
    //�t�B�[���h��ɐ�������Ă���e�̐�
    int m_bulletNum = 0;
    float m_yRot = 0;
    Quaternion m_originalQuaternion = Quaternion.identity;
    [SerializeField] GameObject m_parentObject = null;
    [SerializeField] int m_sameTimeBulletNum = 0;

    void Update()
    {
        //���N���b�N���ꂽ�Ƃ��A
        if (Input.GetMouseButtonDown(0))
        {
            m_yRot = 0.0f;
            //���̉�]���擾
            m_originalQuaternion = transform.rotation;
            //6���ȏ�͔��˂��Ȃ��悤�ɂ���
            if (m_bulletNum >= 6 || m_bulletNum < 0)
            {
                return;
            }

            for (int i = 0; i < m_sameTimeBulletNum; i++)
            {
                //�e�̔��ˊp�x
                m_yRot += 20.0f * Mathf.Pow(-1, i) * i;
                transform.Rotate(0.0f, m_yRot, 0.0f);

                GameObject m_bulletObject = Instantiate(
                    m_bulletPrefab,
                    transform.position,
                    new Quaternion(0.0f, transform.rotation.y , 0.0f, transform.rotation.w)
                );
                //���̉�]�ɖ߂�
                transform.rotation = m_originalQuaternion;
		
				m_bulletObject.name = "1pBullet" + m_bulletNum;
                m_bulletNum++;
                //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
                m_bulletObject.transform.parent = m_parentObject.transform;
            }
        }

    }

    //�t�B�[���h��ɐ�������Ă���e�̐������炷����
    public void ReduceBulletNum()
    {
        m_bulletNum--;
    }
}