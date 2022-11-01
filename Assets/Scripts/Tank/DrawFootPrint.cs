using UnityEngine;

/// <summary>
/// �^���N�̑��Ղ�\�������鏈��
/// </summary>
public class DrawFootPrint : MonoBehaviour
{
    [SerializeField, TooltipAttribute("���Ճv���t�@�u�I�u�W�F�N�g")] GameObject m_footPrintPrefab;

    //���Օ\���Ԋu
    const float DRAW_INTERVAL = 45.0f;

    [SerializeField, TooltipAttribute("���Ղ𐶐�����^���N�̃g�����X�t�H�[��")] Transform m_tankTransform = null;

    //���ՃQ�[���I�u�W�F�N�g���i�[����Q�[���I�u�W�F�N�g
    GameObject m_footPrintsBox = null;

    int m_timer = 0;

    void Start()
    {
        m_footPrintsBox = GameObject.Find("Footprints");
    }

    void Update()
    {
        m_timer++;

        if(m_timer > DRAW_INTERVAL)
        {
            //���ՃI�u�W�F�N�g�𐶐�
            GameObject footPrintObject = Instantiate(
                m_footPrintPrefab,
                new Vector3(m_tankTransform.position.x, -0.45f, m_tankTransform.position.z),
                m_tankTransform.rotation
                );

            //���ՃI�u�W�F�N�g�͑�ʂɐ�������A
            //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
            footPrintObject.transform.parent = m_footPrintsBox.transform;

            m_timer = 0;
        }
    }
}