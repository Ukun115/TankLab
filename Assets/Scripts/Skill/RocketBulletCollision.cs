using UnityEngine;

/// <summary>
/// �e�������ɂԂ������Ƃ��ɋN���鏈��
/// </summary>
namespace nsTankLab
{
    public class RocketBulletCollision : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("���S�}�[�J�[�v���t�@�u�I�u�W�F�N�g")] GameObject m_deathMarkPrefab = null;
        [SerializeField, TooltipAttribute("�X�p�[�N�G�t�F�N�g")] GameObject m_sparkEffectPrefab = null;
        [SerializeField, TooltipAttribute("�����G�t�F�N�g�v���t�@�u(�^���N)")] GameObject m_explosionTankEffectPrefab = null;
        [SerializeField, TooltipAttribute("�����G�t�F�N�g�v���t�@�u(�e)")] GameObject m_explosionBulletEffectPrefab = null;
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        //�o�[�`�����J����
        Cinemachine.CinemachineImpulseSource m_virtualCamera = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            //�o�[�`�����J����
            m_virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineImpulseSource>();
        }

        //�Փˏ���
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                //�ǂɏՓ˂����Ƃ��̏���
                case TagName.Wall:
                    OnCollisitonWall();
                    break;
                //�v���C���[or�GAI�ɏՓ˂����Ƃ��̏���
                case TagName.Player:
                case TagName.Enemy:
                    OnCollisitonPlayerOrEnemyAI(collision);
                    break;
                //�e�ɏՓ˂����Ƃ��̏���
                case TagName.Bullet:
                case TagName.RocketBullet:

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

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //���e�̔����ɂ��������Ƃ�
                case TagName.Bomb:
                    //�ڐG�������e�ɃX�p�[�N�G�t�F�N�g�𐶐�����B
                    GameObject explosionBulletEffect = Instantiate(
                    m_explosionBulletEffectPrefab,
                    transform.position,
                    Quaternion.identity
                    );
                    explosionBulletEffect.name = "ExplosionBulletEffect";

                    Destroy(gameObject);
                    //�e����SE�Đ�
                    m_soundManager.PlaySE("BulletDestroySE");

                    //���e�𔚔�������
                    other.gameObject.GetComponent<ExplosionBomb>().ActiveCollision();
                    break;
            }
        }

        //�ǂɏՓ˂����Ƃ��̏���
        void OnCollisitonWall()
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

            if (collision.gameObject.tag == TagName.Player)
            {
                //�v���C���[�̗̑͂�����������
                m_saveData.GetSetHitPoint--;
            }

            //�e�����ł�����
            Destroy(gameObject);

            //�Փ˂����v���C���[�����ł�����
            Destroy(collision.gameObject);

            //�J������U��������
            m_virtualCamera.GenerateImpulse();
        }
    }
}