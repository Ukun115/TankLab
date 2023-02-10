using UnityEngine;

/// <summary>
/// �GAI���e�𔭎˂��鏈��
/// </summary>
namespace nsTankLab
{
    public class EnemyAIFireBullet : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�ʏ�e�v���t�@�u�I�u�W�F�N�g")] GameObject m_normalBulletPrefab = null;
        [SerializeField, TooltipAttribute("���P�b�g�e�v���t�@�u�I�u�W�F�N�g")]GameObject m_rocketBulletPrefab = null;
        [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;
        [SerializeField, TooltipAttribute("���ˈʒuTransform�R���|�[�l���g")] Transform m_firePositionTransform = null;

        //���P�b�g�e�����Ƃ��̓C���X�y�N�^�[��false�ɐݒ肷��
        [SerializeField]bool m_addFireRocketBulletSkill = false;

        //�e�I�u�W�F�N�g���i�[����I�u�W�F�N�g
        Transform m_bulletsBox = null;

        SaveData m_saveData = null;
        SoundManager m_soundManager = null;

        //���˂����^���N�ԍ�
        int m_myTankNum = 0;

        //�t�B�[���h��ɐ�������Ă���e�̐�
        int m_bulletNum = 0;

        float m_timer = 0.0f;
        [SerializeField]float m_bulletFireDelay = 1.5f;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            //����Q�[���J�n�Ɠ����Ɍ����Ă��܂�Ȃ��悤�ɏ��߂Ƀf�B���C�������Ă���X�^�[�g������
            m_timer = m_bulletFireDelay;
        }

        void Update()
        {
            //�Q�[���i�s���~�܂��Ă���Ƃ��͎��s���Ȃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //Ray���쐬
            Ray ray = new Ray(m_firePositionTransform.root.position, m_firePositionTransform.forward);
            //Ray�̃f�o�b�N�\��
            Debug.DrawRay(m_firePositionTransform.root.position, m_firePositionTransform.forward * 5.0f, Color.red);

            //�v���C���[�ɏՓ˂����Ƃ��̏���
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                switch(hit.collider.tag)
                {
                    case TagName.Player:
                    //�e����
                    FireBullet();
                        break;
                }
            }

            //�e�𔭎˂�����̃f�B���C��������
            //�^�C�}�[���쓮��0�ȉ��ɂȂ����玩���Ń^�C�}�[�쓮�I��
            if (m_timer >= 0)
            {
                m_timer-=Time.deltaTime;
            }
        }

        //�e���ˏ���
        void FireBullet()
        {
            if(!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //�A�˂ł���񐔈ȏ�͔��˂��Ȃ��悤�ɂ���
            if (m_bulletNum >= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myTankNum)].GetRapidFireNum())
            {
                return;
            }

            //���ˏ������ł��Ă��Ȃ��Ɣ��˂��Ȃ��悤�ɂ���
            if (m_timer > 0)
            {
                return;
            }
            //�^�C�}�[�쓮
            m_timer = m_bulletFireDelay;

            //�e����SE�Đ�
            m_soundManager.PlaySE("FireSE");

            //�e�𐶐�
            BulletInstantiate();
        }

        void BulletInstantiate()
        {
            //�ʏ�e
            if (!m_addFireRocketBulletSkill)
            {
                //�e�𐶐�
                GameObject m_bulletObject = Instantiate(
                m_normalBulletPrefab,
                m_firePositionTransform.position,
                new Quaternion(0.0f, m_firePositionTransform.rotation.y, 0.0f, m_firePositionTransform.rotation.w)
                );
                //���������e�̖��O�ύX
                m_bulletObject.name = "EnemyBullet";
                //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
                m_bulletObject.transform.parent = m_bulletsBox;
                //���������e�̓^���N�Ɛ؂藣�����߁A���˂����^���N�I�u�W�F�N�g�f�[�^��e�X�N���v�g�ɓn���Ă����B
                m_bulletObject.GetComponent<BulletCollision>().SetFireTankObject(gameObject);
            }
            //���P�b�g�e
            else
            {
                //�e�𐶐�
                GameObject m_bulletObject = Instantiate(
                m_rocketBulletPrefab,
                m_firePositionTransform.position,
                new Quaternion(0.0f, m_firePositionTransform.rotation.y, 0.0f, m_firePositionTransform.rotation.w)
                );
                //���������e�̖��O�ύX
                m_bulletObject.name = "EnemyBullet";
                //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
                m_bulletObject.transform.parent = m_bulletsBox;
            }
        }

        //�t�B�[���h��ɐ�������Ă���e�̐������炷����
        public void ReduceBulletNum()
        {
            m_bulletNum = Mathf.Clamp(m_bulletNum-1,0,m_bulletNum);
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_bulletsBox = GameObject.Find("Bullets").GetComponent<Transform>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();
        }
    }
}