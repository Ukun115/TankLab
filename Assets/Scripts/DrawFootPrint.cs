using UnityEngine;

/// <summary>
/// �@�̂̑��Ղ�\�������鏈��
/// </summary>
public class DrawFootPrint : MonoBehaviour
{
    //���Ճv���t�@�u
    [SerializeField] GameObject m_footPrintPrefab;
    //�^�C�}�[
    float m_timer = 0;
    //���Ղ�\��������Ԋu
    [SerializeField] float m_drawInterval = 0;
    //���ՃQ�[���I�u�W�F�N�g���i�[����Q�[���I�u�W�F�N�g
    GameObject m_footPrintsBox = null;

    void Start()
    {
        m_footPrintsBox = GameObject.Find("Footprints");
    }

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_drawInterval)
        {
            //���Ղ𐶐����鏈��
            InstantiateFootPrint();
        }
    }

    //���Ղ𐶐����鏈��
    void InstantiateFootPrint()
    {
        //���ՃI�u�W�F�N�g�𐶐�
        GameObject footPrintObject = Instantiate(m_footPrintPrefab, new Vector3(transform.position.x, -0.45f, transform.position.z), transform.rotation);
        //���ՃI�u�W�F�N�g�͑�ʂɐ�������A
        //�q�G�����L�[�オ�����Ⴒ����ɂȂ��Ă��܂��̂�h�����߁A�e��p�ӂ��Ă܂Ƃ߂Ă����B
        footPrintObject.transform.parent = m_footPrintsBox.transform;

        m_timer = 0;
    }
}