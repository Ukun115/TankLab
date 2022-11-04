using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// �e�������ɂԂ������Ƃ��ɋN���鏈��
/// </summary>
public class BulletCollision : MonoBehaviour
{
    //���݂̒e�̔��ˉ�
    int m_refrectionCount = 0;

    [SerializeField, TooltipAttribute("���S�}�[�J�[�v���t�@�u�I�u�W�F�N�g")] GameObject m_deathMarkPrefab = null;

    [SerializeField, TooltipAttribute("���U���g�����������Ă���v���t�@�u�I�u�W�F�N�g")] GameObject m_resultPrefab = null;

    [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

    //���˂����v���C���[�ԍ�
    int m_myPlayerNum = 0;

    SaveData m_saveData = null;

    GameObject resultObject = null;

    //���g�𔭎˂����^���N�̃I�u�W�F�N�g�f�[�^
    GameObject m_tankObject = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�GAI�̒e����Ȃ��Ƃ��͎��s
        if (this.gameObject.name != "EnemyBullet")
        {
            //���˂����v���C���[�ԍ����擾
            m_myPlayerNum = int.Parse(Regex.Replace(this.transform.name, @"[^1-4]", "")) - 1;
        }
    }

    //�Փˏ���
    void OnCollisionEnter(Collision collision)
    {
        //�ǂɏՓ˂����ꍇ
        if (collision.gameObject.CompareTag("Wall"))
        {
            //�ǂɏՓ˂����Ƃ��̏���
            OnCollisitonWall();
        }

        //�v���C���[or�GAI�ɏՓ˂����ꍇ
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Enemy"))
        {
            //�v���C���[or�GAI�ɏՓ˂����Ƃ��̏���
            OnCollisitonPlayerOrEnemyAI(collision);
        }

        //�e�ɏՓ˂����ꍇ
        if(collision.gameObject.CompareTag("Bullet"))
        {
            //�������ł�����
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
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
            Destroy(this.gameObject, 0.05f);
        }
    }

    //�v���C���[�ɏՓ˂����Ƃ��̏���
    void OnCollisitonPlayerOrEnemyAI(Collision collision)
    {
        //���񂾏ꏊ�Ɂ~���S�}�[�N�I�u�W�F�N�g�𐶐�����B
        Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                collision.gameObject.transform.position.x,
                -0.4f,
                collision.gameObject.transform.position.z
                ),
            collision.gameObject.transform.rotation
            );

        //�e�����ł�����
        Destroy(this.gameObject);

        //�Փ˂����v���C���[�����ł�����
        Destroy(collision.gameObject, 0.05f);

        //�`�������W���[�h�̎�
        if (m_saveData.GetSetSelectGameMode == "CHALLENGE")
        {
            //Enemy�^�O������GameObject�� �S�� �擾����B
            GameObject[] enemyObject = GameObject.FindGameObjectsWithTag("Enemy");
            //Player�^�O������GameObject���擾����B
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            //�GAI���S�@����ł�����A
            if (enemyObject.Length == 0)
            {
                //���U���g�ɓ˓�
                //���U���g�����͖��V�[�����ƂɂP�x�݂̂������s���Ȃ�
                if (resultObject is null)
                {
                    //���U���g�������܂Ƃ߂Ă���Q�[���I�u�W�F�N�g�𐶐����A
                    //���U���g���������s���Ă����B
                    resultObject = Instantiate(m_resultPrefab);
                    //1P�����\��
                    resultObject.GetComponent<ResultInit>().SetWinPlayer(1);
                }
            }
            //�S�@����ł��Ȃ��Ƃ��A
            //(�܂�v���C���[������ł���Ƃ��A)
            else if(playerObject is null)
            {
                //���U���g�������܂Ƃ߂Ă���Q�[���I�u�W�F�N�g�𐶐����A
                //���U���g���������s���Ă����B
                resultObject = Instantiate(m_resultPrefab);
                //�`�������W�I���\��
                resultObject.GetComponent<ResultInit>().SetWinPlayer(2);
            }
        }
        //�`�������W���[�h�ȊO�̃��[�h�̎�
        else
        {
            //Player�^�O������GameObject��S�Ď擾����B
            GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player");

            //�v���C���[���t�B�[���h��Ɉ�l�����ɂȂ�����A
            if(playerObject.Length == 1)
            {
                //���U���g�������܂Ƃ߂Ă���Q�[���I�u�W�F�N�g�𐶐����A
                //���U���g���������s���Ă����B
                resultObject = Instantiate(m_resultPrefab);
                //?P�����\��
                int winPlayerNum = int.Parse(Regex.Replace(playerObject[0].name, @"[^1-4]", ""));
                resultObject.GetComponent<ResultInit>().SetWinPlayer(winPlayerNum);
            }
        }
    }

    public void SetFireTankObject(GameObject tankObject)
    {
        m_tankObject = tankObject;
    }

    //�e���폜���ꂽ�Ƃ��ɌĂ΂��
    void OnDestroy()
    {
        //�GAI�̒e
        if (this.gameObject.name == "EnemyBullet")
        {
            if (m_tankObject is not null)
            {
                //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
            }
        }
        //�v���C���[�̒e
        else
        {
            if (m_tankObject is not null)
            {
                //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                m_tankObject.gameObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
            }
        }
    }
}