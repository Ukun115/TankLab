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

    [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

    //���˂����v���C���[�ԍ�
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    //���g�𔭎˂����^���N�̃I�u�W�F�N�g�f�[�^
    GameObject m_tankObject = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

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
                //�������ł�����
                Destroy(gameObject);
                Destroy(collision.gameObject);
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
            //�e�����ł�����
            Destroy(gameObject);
        }
    }

    //�v���C���[�ɏՓ˂����Ƃ��̏���
    void OnCollisitonPlayerOrEnemyAI(Collision collision)
    {
        //���񂾏ꏊ�Ɂ~���S�}�[�N�I�u�W�F�N�g�𐶐�����B
        GameObject deathMark = Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                collision.gameObject.transform.position.x,
                -0.4f,
                collision.gameObject.transform.position.z
                ),
            collision.gameObject.transform.rotation
            );
        deathMark.name = "DeathMark";

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
        switch (gameObject.name)
        {
            //�GAI�̒e
            case "EnemyBullet":
                if (m_tankObject is not null)
                {
                    //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                    m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
                }
                break;

            //�v���C���[�̒e
            default:
                if (m_tankObject is not null)
                {
                    //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                    m_tankObject.gameObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
                }
                break;
        }
    }
}
}