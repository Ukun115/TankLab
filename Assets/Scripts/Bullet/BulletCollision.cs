using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// �e�������ɂԂ������Ƃ��ɋN���鏈��
/// </summary>
namespace nsTankLab
{
public class BulletCollision : MonoBehaviour
{
    //���݂̒e�̔��ˉ�
    int m_refrectionCount = 0;

    [SerializeField, TooltipAttribute("���S�}�[�J�[�v���t�@�u�I�u�W�F�N�g")] GameObject m_deathMarkPrefab = null;
    [SerializeField, TooltipAttribute("�X�p�[�N�G�t�F�N�g")] GameObject m_sparkEffectPrefab = null;
    [SerializeField, TooltipAttribute("�����G�t�F�N�g�v���t�@�u(�^���N)")] GameObject m_explosionTankEffectPrefab = null;
    [SerializeField, TooltipAttribute("�����G�t�F�N�g�v���t�@�u(�e)")] GameObject m_explosionBulletEffectPrefab = null;

    [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

    //���˂����v���C���[�ԍ�
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;
    SoundManager m_soundManager = null;

    //���g�𔭎˂����^���N�̃I�u�W�F�N�g�f�[�^
    GameObject m_tankObject = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

        //�GAI�̒e����Ȃ��Ƃ��͎��s
        if (gameObject.name != "EnemyBullet")
        {
            //���˂����v���C���[�ԍ����擾
            m_myPlayerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", "")) - 1;
        }
    }

    //�Փˏ���
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //�ǂɏՓ˂����Ƃ��̏���
            case "Wall":
                OnCollisitonWall();
                break;
            //�v���C���[or�GAI�ɏՓ˂����Ƃ��̏���
            case "Player":
            case "Enemy":
                OnCollisitonPlayerOrEnemyAI(collision);
                break;
            //�e�ɏՓ˂����Ƃ��̏���
            case "Bullet":

                    //�ڐG�����ǂɃX�p�[�N�G�t�F�N�g�𐶐�����B
                    GameObject explosionBulletEffect = Instantiate(
                    m_explosionBulletEffectPrefab,
                    transform.position,
                    Quaternion.identity
                    );
                    explosionBulletEffect.name = "ExplosionBulletEffect";

                    //�������ł�����
                    Destroy(gameObject);
                Destroy(collision.gameObject);
                    //�e����SE�Đ�
                    m_soundManager.PlaySE("BulletDestroySE");
                    break;
        }
    }

    //�ǂɏՓ˂����Ƃ��̏���
    void OnCollisitonWall()
    {
        m_refrectionCount++;

        //�w�肳��Ă��锽�ˉ񐔕����˂�����A
        if (m_refrectionCount > m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myPlayerNum)].GetBulletRefrectionNum())
        {
                //�e����SE�Đ�
                m_soundManager.PlaySE("BulletDestroySE");

                //�ڐG�����ǂɃX�p�[�N�G�t�F�N�g�𐶐�����B
                GameObject explosionBulletEffect = Instantiate(
                m_explosionBulletEffectPrefab,
                transform.position,
                Quaternion.identity
                );
                explosionBulletEffect.name = "ExplosionBulletEffect";

                //�e�����ł�����
                Destroy(gameObject);
        }
        else
            {
                //�ڐG�����ǂɃX�p�[�N�G�t�F�N�g�𐶐�����B
                GameObject sparkEffect = Instantiate(
                m_sparkEffectPrefab,
                transform.position,
                Quaternion.identity
                );
                sparkEffect.name = "SparkEffect";

                //�ǔ��ˉ��Đ�
                m_soundManager.PlaySE("BulletRefrectionSE");
            }
    }

    //�v���C���[�ɏՓ˂����Ƃ��̏���
    void OnCollisitonPlayerOrEnemyAI(Collision collision)
    {
            //���j���Đ�
            m_soundManager.PlaySE("DeathSE");

            //���񂾏ꏊ�Ɂ~���S�}�[�N�I�u�W�F�N�g�𐶐�����B
            GameObject deathMark = Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                collision.gameObject.transform.position.x,
                -0.4f,
                collision.gameObject.transform.position.z
                ),
            Quaternion.identity
            );
        deathMark.name = "DeathMark";

            //���񂾏ꏊ�ɔ����G�t�F�N�g�𐶐�����B
            GameObject m_explosionEffect = Instantiate(
            m_explosionTankEffectPrefab,
            transform.position,
            Quaternion.identity
            );
            m_explosionEffect.name = "ExplosionTankEffect";

            if (collision.gameObject.tag == "Player")
            {
                //�v���C���[�̗̑͂�����������
                m_saveData.GetSetHitPoint--;
            }

        //�e�����ł�����
        Destroy(gameObject);

        //�Փ˂����v���C���[�����ł�����
        Destroy(collision.gameObject);
    }

    public void SetFireTankObject(GameObject tankObject)
    {
        m_tankObject = tankObject;
    }

    //�e���폜���ꂽ�Ƃ��ɌĂ΂��
    void OnDestroy()
    {
            //�GAI�̒e
            if(gameObject.name.Contains("EnemyBullet"))
            {
                if (m_tankObject.ToString() != "null")
                {
                    //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                    m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
                }
            }
            //�v���C���[�̒e
            if (gameObject.name.Contains("PlayerBullet"))
            {
                if (m_tankObject.ToString() != "null")
                {
                    //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                    m_tankObject.gameObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
                }
            }
    }
}
}