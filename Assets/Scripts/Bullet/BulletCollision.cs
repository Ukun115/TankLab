using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �e�������ɂԂ������Ƃ��ɋN���鏈��
/// </summary>
namespace nsTankLab
{
    public class BulletCollision : MonoBehaviourPun
    {
        [SerializeField, TooltipAttribute("���S�}�[�J�[�v���t�@�u�I�u�W�F�N�g")] GameObject m_deathMarkPrefab = null;
        [SerializeField, TooltipAttribute("�X�p�[�N�G�t�F�N�g")] GameObject m_sparkEffectPrefab = null;
        [SerializeField, TooltipAttribute("�����G�t�F�N�g�v���t�@�u(�^���N)")] GameObject m_explosionTankEffectPrefab = null;
        [SerializeField, TooltipAttribute("�����G�t�F�N�g�v���t�@�u(�e)")] GameObject m_explosionBulletEffectPrefab = null;
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

        //���݂̒e�̔��ˉ�
        int m_refrectionCount = 0;

        //���˂����v���C���[�ԍ�
        int m_playerNum = 0;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        //���g�𔭎˂����^���N�̃I�u�W�F�N�g�f�[�^
        GameObject m_tankObject = null;

        //�o�[�`�����J����
        Cinemachine.CinemachineImpulseSource m_virtualCamera = null;

        ControllerData m_controllerData = null;

        void Start()
        {
            //�R���|�\�l���g�擾�܂Ƃ�
            GetComponens();

            //�GAI�̒e����Ȃ��Ƃ��͎��s
            if (gameObject.name != "EnemyBullet")
            {
                //�I�����C���ő��肪���������e
                if (gameObject.name == "Bullet(Clone)")
                {
                    //���g���v���C���[1
                    if (m_saveData.GetSetPlayerNum == 1)
                    {
                        gameObject.name = $"PlayerBullet{2}";
                        m_playerNum = 1;

                        m_tankObject = GameObject.Find("2P/PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition");
                    }
                    //���g���v���C���[2
                    else if (m_saveData.GetSetPlayerNum == 2)
                    {
                        gameObject.name = $"PlayerBullet{1}";
                        m_playerNum = 0;

                        m_tankObject = GameObject.Find("1P/PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition");
                    }
                }
                else
                {
                    //���˂����v���C���[�ԍ����擾
                    m_playerNum = int.Parse(Regex.Replace(transform.name, @"[^1-4]", string.Empty)) - 1;
                }
            }
        }

        //�Փˏ���
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                //��
                case TagName.Wall:
                    OnCollisitonWall();
                    break;
                //�v���C���[or�GAI
                case TagName.Player:
                case TagName.Enemy:
                    OnCollisitonPlayerOrEnemyAI(collision);
                    break;
                //�e
                case TagName.Bullet:
                    //�e�����G�t�F�N�g����
                    ExplosionBulletEffectInstantiate();

                    Destroy(collision.gameObject);
                    break;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //���e
                case TagName.Bomb:
                    //�e�����G�t�F�N�g����
                    ExplosionBulletEffectInstantiate();

                    //���e�𔚔�������
                    other.gameObject.GetComponent<ExplosionBomb>().ActiveCollision();
                    break;
            }
        }

        //�e�����G�t�F�N�g��������
        void ExplosionBulletEffectInstantiate()
        {
            //�ڐG�������e�ɃX�p�[�N�G�t�F�N�g�𐶐�����B
            GameObject explosionBulletEffect = Instantiate(
            m_explosionBulletEffectPrefab,
            transform.position,
            Quaternion.identity
            );
            explosionBulletEffect.name = "ExplosionBulletEffect";

            Destroy(gameObject);

            //�e����SE�Đ�
            if (m_soundManager is null)
            {
                m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            }
            m_soundManager.PlaySE("BulletDestroySE");
        }

        //�ǂɏՓ˂����Ƃ��̏���
        void OnCollisitonWall()
        {
            m_refrectionCount++;

            //�w�肳��Ă��锽�ˉ񐔕����˂�����A
            if (m_refrectionCount > m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_playerNum)].GetBulletRefrectionNum())
            {
                //�e�����G�t�F�N�g����
                ExplosionBulletEffectInstantiate();
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
            DeathMarkInstantiate(collision);

            //���񂾏ꏊ�ɔ����G�t�F�N�g�𐶐�����B
            ExplosionEffectInstantiate();

            //�v���C���[�̏ꍇ
            if (collision.gameObject.tag == TagName.Player)
            {
                //�v���C���[�̗̑͂�����������
                m_saveData.GetSetHitPoint--;
            }

            //�e�����ł�����
            Destroy(gameObject);

            if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                PhotonNetwork.Destroy(collision.gameObject);
            }
            else
            {
                //�Փ˂����v���C���[�����ł�����
                Destroy(collision.gameObject);
            }

            //�J����&�Q�[���p�b�h�U������
            Vibration(collision);
        }

        //���S�}�[�J�[��������
        void DeathMarkInstantiate(Collision collision)
        {
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
        }

        //�����G�t�F�N�g��������
        void ExplosionEffectInstantiate()
        {
            GameObject m_explosionEffect = Instantiate(
            m_explosionTankEffectPrefab,
            transform.position,
            Quaternion.identity
            );
            m_explosionEffect.name = "ExplosionTankEffect";
        }

        //�R���g���[���[�Ɖ�ʂ�U�������鏈��
        void Vibration(Collision collision)
        {
            //�J������U��������
            m_virtualCamera.GenerateImpulse();

            if (collision.gameObject.tag == TagName.Player)
            {
                //�Q�[���p�b�h���ڑ�����Ă�����A
                if (Gamepad.current is not null)
                {
                    if (m_controllerData.GetGamepad(int.Parse(Regex.Replace(collision.gameObject.name, @"[^1-4]", string.Empty))) is not null)
                    {
                        //���j���ꂽ�Q�[���p�b�h��U��������
                        m_controllerData.GetGamepad(int.Parse(Regex.Replace(collision.gameObject.name, @"[^1-4]", string.Empty))).SetMotorSpeeds(0.0f, 1.0f);
                    }
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
            if(gameObject.name.Contains("EnemyBullet"))
            {
                if (m_tankObject != null)
                {
                    //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                    m_tankObject.GetComponent<EnemyAIFireBullet>().ReduceBulletNum();
                }
            }
            //�v���C���[�̒e
            if (gameObject.name.Contains("PlayerBullet"))
            {
                if (m_tankObject != null)
                {
                    //�t�B�[���h��ɐ�������Ă���e�̐��f�[�^�����炷
                    m_tankObject.GetComponent<PlayerFireBullet>().ReduceBulletNum();
                }
            }
        }

        //�R���|�[�l���g�擾
        void GetComponens()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();

            //�o�[�`�����J����
            m_virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineImpulseSource>();
        }
    }
}