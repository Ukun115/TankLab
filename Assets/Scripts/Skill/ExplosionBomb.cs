using UnityEngine;

namespace nsTankLab
{
    public class ExplosionBomb : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("Rigidbody")] Rigidbody m_rigidbody = null;
        [SerializeField, TooltipAttribute("�����̓����蔻��")] SphereCollider m_sphereCollider = null;
        [SerializeField, TooltipAttribute("���S�}�[�J�[�v���t�@�u�I�u�W�F�N�g")] GameObject m_deathMarkPrefab = null;
        [SerializeField, TooltipAttribute("�����G�t�F�N�g�v���t�@�u")] GameObject m_explosionEffectPrefab = null;
        [SerializeField, TooltipAttribute("���e�̃}�e���A��")] Material[] m_bombMaterial = null;

        SoundManager m_soundManager = null;

        GameObject m_dropPlayer = null;

        //�_�ł��邩�ǂ���
        bool m_isFlashing = false;

        bool m_normalColor = true;

        Renderer m_renderer = null;

        int m_timer = 0;

        SaveData m_saveData = null;

        //�o�[�`�����J����
        Cinemachine.CinemachineImpulseSource m_virtualCamera = null;

        enum enColor
        {
            enNormalColor,
            enRed
        }

        void Start()
        {
            Invoke(nameof(ActiveFlashing), 20.0f);

            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();
        }

        void Update()
        {
            //�_�ł��邩�ǂ���
            if(m_isFlashing)
            {
                m_timer++;

                if(m_timer > 5)
                {
                    m_timer = 0;
                    m_normalColor = !m_normalColor;
                }

                if(m_normalColor)
                {
                    //�ʏ�F�ɂ���
                    m_renderer.material = m_bombMaterial[(int)enColor.enNormalColor];
                }
                else
                {
                    //�ԐF�ɂ���
                    m_renderer.material = m_bombMaterial[(int)enColor.enRed];
                }
            }
        }

        //�_�ł��A�N�e�B�u�ɂ���
        public void ActiveFlashing()
        {
            m_isFlashing = true;

            Invoke(nameof(ActiveCollision), 1.5f);
        }

        public void ActiveCollision()
        {
            //�N�[���^�C���J�n
            if (m_dropPlayer != null && m_dropPlayer.tag != TagName.Enemy)
            {
                m_dropPlayer.GetComponent<DropBomb>().CoolStart();
            }

            //�������Đ�
            m_soundManager.PlaySE("BombExplosionSE");

            //�����G�t�F�N�g�Đ�
            GameObject fireEffect = Instantiate(
            m_explosionEffectPrefab,
            transform.position,
            transform.rotation
            );
            fireEffect.name = "BombExplosionEffect";

            m_sphereCollider.enabled = true;
            m_rigidbody.useGravity = true;

            Destroy(gameObject, 0.05f);
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //�v���C���[or�GAI�ɏՓ˂����Ƃ��̏���
                case TagName.Player:
                    if (m_dropPlayer == other.gameObject)
                    {
                        return;
                    }
                    //�^���N�����j����
                    OnCollisitonPlayerOrEnemyAI(other);
                    //�����R���W�������I���ɂ���
                    ActiveCollision();

                    //�J������U��������
                    m_virtualCamera.GenerateImpulse();

                    //�v���C���[�̗̑͂�����������
                    m_saveData.GetSetHitPoint--;

                    break;
                case TagName.Enemy:
                    if(m_dropPlayer == other.gameObject || m_dropPlayer.tag == TagName.Enemy)
                    {
                        return;
                    }
                    //�^���N�����j����
                    OnCollisitonPlayerOrEnemyAI(other);
                    //�����R���W�������I���ɂ���
                    ActiveCollision();

                    //�J������U��������
                    m_virtualCamera.GenerateImpulse();

                    break;
            }
        }

        public void SetDropPlayer(GameObject dropPlayer)
        {
            m_dropPlayer = dropPlayer;
        }

        //�v���C���[�ɏՓ˂����Ƃ��̏���
        void OnCollisitonPlayerOrEnemyAI(Collider other)
        {
            //���񂾏ꏊ�Ɂ~���S�}�[�N�I�u�W�F�N�g�𐶐�����B
            GameObject deathMark = Instantiate(
            m_deathMarkPrefab,
            new Vector3(
                other.gameObject.transform.position.x,
                -0.4f,
                other.gameObject.transform.position.z
                ),
            Quaternion.identity
            );
            deathMark.name = "DeathMark";

            //�����Ɋ������܂ꂽ�v���C���[�����ł�����
            Destroy(other.gameObject);
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_renderer = GetComponent<Renderer>();
            //�o�[�`�����J����
            m_virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Cinemachine.CinemachineImpulseSource>();
        }
    }
}
