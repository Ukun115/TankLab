using UnityEngine;

/// <summary>
/// �GAI���e�𔭎˂��鏈��
/// </summary>
namespace nsTankLab
{
public class EnemyAIFireBullet : MonoBehaviour
{
    [SerializeField, TooltipAttribute("�e�v���t�@�u�I�u�W�F�N�g")] GameObject m_bulletPrefab = null;
    //�e�I�u�W�F�N�g���i�[����I�u�W�F�N�g
    GameObject m_bulletsBox = null;

    [SerializeField, TooltipAttribute("�^���N�f�[�^�x�[�X")] TankDataBase m_tankDataBase = null;

    SaveData m_saveData = null;

    //���˂����^���N�ԍ�
    int m_myTankNum = 0;

    //�t�B�[���h��ɐ�������Ă���e�̐�
    int m_bulletNum = 0;

    int m_timer = 0;
    const int BULLET_FIRE_DELAY = 120;

    void Start()
    {
        m_bulletsBox = GameObject.Find("Bullets");
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //����Q�[���J�n�Ɠ����Ɍ����Ă��܂�Ȃ��悤�ɏ��߂Ƀf�B���C�������Ă���X�^�[�g������
        m_timer = BULLET_FIRE_DELAY;
    }

    void Update()
    {
        //Ray���쐬
        Ray ray = new Ray(transform.root.position, transform.forward);
        //Ray�̃f�o�b�N�\��
        Debug.DrawRay(transform.root.position, transform.forward * 5.0f, Color.red);

        //�v���C���[�ɏՓ˂����Ƃ��̏���
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                //�e����
                BulletInstantiate();
            }
        }

        //�e�𔭎˂�����̃f�B���C��������
        //�^�C�}�[���쓮��0�ɂȂ����玩���Ń^�C�}�[�쓮�I��
        if (m_timer != 0)
        {
            m_timer--;
        }
    }

    //�e��������
    void BulletInstantiate()
    {
        //�A�˂ł���񐔈ȏ�͔��˂��Ȃ��悤�ɂ���
        if (m_bulletNum >= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myTankNum)].GetRapidFireNum())
        {
            return;
        }

        //���ˏ������ł��Ă��Ȃ��Ɣ��˂��Ȃ��悤�ɂ���
        if (m_timer != 0)
        {
            return;
        }
        //�^�C�}�[�쓮
        m_timer = BULLET_FIRE_DELAY;

        //�e�𐶐�
        GameObject m_bulletObject = Instantiate(
        m_bulletPrefab,
        transform.position,
        new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
        );

        //���������e�̖��O�ύX
        m_bulletObject.name = "EnemyBullet";

        //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
        m_bulletObject.transform.parent = m_bulletsBox.transform;

        //���������e�̓^���N�Ɛ؂藣�����߁A���˂����^���N�I�u�W�F�N�g�f�[�^��e�X�N���v�g�ɓn���Ă����B
        m_bulletObject.GetComponent<BulletCollision>().SetFireTankObject(gameObject);
    }

    //�t�B�[���h��ɐ�������Ă���e�̐������炷����
    public void ReduceBulletNum()
    {
        m_bulletNum = Mathf.Clamp(m_bulletNum-1,0,m_bulletNum);
    }
}
}