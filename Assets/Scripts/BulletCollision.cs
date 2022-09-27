using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�������ɂԂ������Ƃ��ɋN���鏈��
/// </summary>
public class BulletCollision : MonoBehaviour
{
    FireBullet m_fireBulletScript = null;
    [SerializeField] GameObject m_deathMarkPrefab = null;
    [SerializeField] GameObject m_resultPrefab = null;

    void Start()
    {
        m_fireBulletScript = GameObject.Find("1pFireBulletPos").GetComponent<FireBullet>();
    }

    //�Փˏ���
    void OnCollisionEnter(Collision collision)
    {
        //�ǂɏՓ˂����ꍇ
        if(collision.gameObject.tag == "Wall")
        {
            //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
            m_fireBulletScript.ReduceBulletNum();

            //�e�����ł�����
            Destroy(this.gameObject,0.1f);
        }

        //�v���C���[�ɏՓ˂����ꍇ
        if (collision.gameObject.tag == "Player")
        {
            //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
            m_fireBulletScript.ReduceBulletNum();

            //�e�����ł�����
            Destroy(this.gameObject, 0.1f);

            //�Փ˂����v���C���[�����ł�����
            Destroy(collision.gameObject,0.1f);

            //���񂾏ꏊ�Ɂ~���S�}�[�N�I�u�W�F�N�g�𐶐�����B
            Instantiate(
                m_deathMarkPrefab,
                new Vector3(
                    collision.gameObject.transform.position.x,
                    collision.gameObject.transform.position.y - 0.49f,
                    collision.gameObject.transform.position.z
                    ),
                collision.gameObject.transform.rotation
                );

            //���U���g�������܂Ƃ߂Ă���Q�[���I�u�W�F�N�g�𐶐����A
            //���U���g���������s���Ă����B
            GameObject resultObject = Instantiate(m_resultPrefab, Vector3.zero, Quaternion.identity);

            //���S�����̂�1P�������ꍇ
            if (collision.gameObject.name == "1P")
            {
                //2P�����\��
                resultObject.GetComponent<ResultInit>().SetWinPlayer(2);
            }
            //���S�����̂�2P�������ꍇ
            if (collision.gameObject.name == "2P")
            {
                //1P�����\��
                resultObject.GetComponent<ResultInit>().SetWinPlayer(1);
            }
        }
    }
}