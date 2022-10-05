using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] GameObject m_bulletPrefab = null;
    //�t�B�[���h��ɐ�������Ă���e�̐�
    int m_bulletNum = 0;

    void Update()
    {
        //���N���b�N���ꂽ�Ƃ��A
        if (Input.GetMouseButtonDown(0))
        {
            //�T���ȏ�͔��˂��Ȃ��悤�ɂ���
            if(m_bulletNum >= 5||m_bulletNum<0)
            {
                return;
            }

            GameObject m_bulletObject = Instantiate(
                m_bulletPrefab,
                transform.position,
                new Quaternion(0.0f, transform.rotation.y,0.0f,transform.rotation.w)
            );
            m_bulletObject.name = "1pBullet" + m_bulletNum;
            m_bulletNum++;
            //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
            m_bulletObject.transform.parent = GameObject.Find("Bullets").transform;
        }
    }

    //�t�B�[���h��ɐ�������Ă���e�̐������炷����
    public void ReduceBulletNum()
    {
        m_bulletNum--;
    }
}