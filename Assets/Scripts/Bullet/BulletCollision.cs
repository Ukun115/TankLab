using UnityEngine;

/// <summary>
/// �e�������ɂԂ������Ƃ��ɋN���鏈��
/// </summary>
public class BulletCollision : MonoBehaviour
{
    //�e�𔭎˂��鏈���������ꂽ�X�N���v�g
    FireBullet m_fireBulletScript = null;
    BulletMovement m_bulletMovement = null;
    //����
    Rigidbody m_rigidbody = null;

    [SerializeField, TooltipAttribute("�e�̔��ˉ�")] int m_refrectionNum = 0;

    //���݂̒e�̔��ˉ�
    int m_refrectionCount = 0;

    [SerializeField, TooltipAttribute("���S�}�[�J�[�v���t�@�u�I�u�W�F�N�g")] GameObject m_deathMarkPrefab = null;

    [SerializeField, TooltipAttribute("���U���g�����������Ă���v���t�@�u�I�u�W�F�N�g")] GameObject m_resultPrefab = null;

    //�e���������������ǂ���
    bool m_isNumReduce = true;


    void Start()
    {
        m_fireBulletScript = GameObject.Find("FireBulletPos").GetComponent<FireBullet>();

        m_rigidbody = GetComponent<Rigidbody>();

        m_bulletMovement = GetComponent<BulletMovement>();
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

        //�v���C���[�ɏՓ˂����ꍇ
        if (collision.gameObject.CompareTag("Player"))
        {
            //�v���C���[�ɏՓ˂����Ƃ��̏���
            OnCollisitonPlayer(collision);
        }
    }

    //�ǂɏՓ˂����Ƃ��̏���
    void OnCollisitonWall()
    {
        m_refrectionCount++;

        m_bulletMovement.SetIsRefrectionBefore(true);

        //�w�肳��Ă��锽�ˉ񐔕����˂�����A
        if (m_refrectionCount > m_refrectionNum)
        {
            m_bulletMovement.SetIsRefrectionBefore(false);

            if (m_isNumReduce)
            {
                //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                m_fireBulletScript.ReduceBulletNum();

                m_isNumReduce = false;
            }

            //�e�����ł�����
            Destroy(this.gameObject, 0.05f);

        }

        transform.rotation = new Quaternion(0.0f, m_rigidbody.velocity.y, 0.0f, 1.0f);
    }

    //�v���C���[�ɏՓ˂����Ƃ��̏���
    void OnCollisitonPlayer(Collision collision)
    {
        if (m_isNumReduce)
        {
            //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
            m_fireBulletScript.ReduceBulletNum();

            m_isNumReduce = false;
        }

        //�e�����ł�����
        Destroy(this.gameObject, 0.05f);

        //�Փ˂����v���C���[�����ł�����
        Destroy(collision.gameObject, 0.05f);

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
        GameObject resultObject = Instantiate(m_resultPrefab);

        //���S�����v���C���[�ɂ���ĕ���
        switch (collision.gameObject.name)
        {
            case "1P":
                //2P�����\��
                resultObject.GetComponent<ResultInit>().SetWinPlayer(2);
                break;
            case "2P":
                //1P�����\��
                resultObject.GetComponent<ResultInit>().SetWinPlayer(1);
                break;
        }
    }
}