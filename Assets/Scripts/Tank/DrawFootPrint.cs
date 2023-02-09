using UnityEngine;

/// <summary>
/// �^���N�̑��Ղ�\�������鏈��
/// </summary>
namespace nsTankLab
{
    public class DrawFootPrint : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("���Ճv���t�@�u�I�u�W�F�N�g")] GameObject m_footPrintPrefab = null;
        [SerializeField, TooltipAttribute("���Ղ𐶐�����^���N�̃g�����X�t�H�[��")] Transform m_tankTransform = null;

        //���Օ\���Ԋu
        const float DRAW_INTERVAL = 0.5f;

        //���ՃQ�[���I�u�W�F�N�g���i�[����Q�[���I�u�W�F�N�g
        Transform m_footPrintsBox = null;

        SaveData m_saveData = null;

        //�^�C�}�[
        float m_timer = 0.0f;

        void Start()
        {
            m_footPrintsBox = GameObject.Find("Footprints").GetComponent<Transform>();

            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //�Q�[���i�s���~�܂��Ă���Ƃ��͎��s���Ȃ�
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            m_timer+=Time.deltaTime;

            if(m_timer > DRAW_INTERVAL)
            {
                //���Օ`��
                Draw();
                //�^�C�}�[������
                m_timer = 0;
            }
        }

        //���Օ`�揈��
        void Draw()
        {
            //���ՃI�u�W�F�N�g�𐶐�
            GameObject footPrintObject = Instantiate(
                m_footPrintPrefab,
                new Vector3(m_tankTransform.position.x, -0.45f,m_tankTransform.position.z),
                m_tankTransform.rotation
                );

            //���ՃI�u�W�F�N�g�͑�ʂɐ�������A
            //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
            footPrintObject.transform.parent = m_footPrintsBox;
        }
    }
}