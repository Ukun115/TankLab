using UnityEngine;

/// <summary>
/// �{�^���̃}�e���A����ύX���鏈��
/// </summary>
public class ButtonMaterialChange : MonoBehaviour
{
    //�q�I�u�W�F�N�g�B������z��
    Transform[] m_childrenObject;

    [SerializeField, TooltipAttribute("�u���b�N�{�^���̃}�e���A��")] Material[] m_blockButtonMaterial = null;

    //�J���[�`�F���W���ł��邩�ǂ���
    bool m_ableColorChange = false;

    //�}�e���A����߂�����
    public void ReturnChangeMaterial()
    {
        if (m_childrenObject is not null && m_ableColorChange)
        {
            //�O��}�E�X�J�[�\�����������Ă����u���b�N�{�^���I�u�W�F�N�g�����Ƃ̐F�ɖ߂�
            //�}�e���A���̃J���[�`�F���W����
            DecideMaterialColor("Gray_Normal (Instance)", "Gray_Dark (Instance)", 0, false);
        }
    }
    //�}�e���A����ς��鏈��
    public void ChangeMaterial(RaycastHit hit)
    {
        // �q�I�u�W�F�N�g�B������z��̏�����
        m_childrenObject = new Transform[hit.collider.gameObject.transform.childCount];

        //�q�I�u�W�F�N�g���擾
        for (int childrenNum = 0; childrenNum < hit.collider.gameObject.transform.childCount; childrenNum++)
        {
            m_childrenObject[childrenNum] = hit.collider.gameObject.transform.GetChild(childrenNum); // GetChild()�Ŏq�I�u�W�F�N�g���擾
        }

        //�}�e���A���̃J���[�`�F���W����
        DecideMaterialColor("Gray_Light (Instance)", "Gray_Normal (Instance)", 1, true);
    }

    //�}�e���A���̃J���[�`�F���W����
    void DecideMaterialColor(string materialName1, string materialName2, int materialNum, bool ableColorChange)
    {
        //�u���b�N�{�^���̐F(�}�e���A��)��ύX����
        for (int childrenNum = 0; childrenNum < m_childrenObject.Length; childrenNum++)
        {
            //���蓖�Ă��Ă���}�e���A���ɂ���ĕύX��̃}�e���A�������肷��
            if (m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material.name == materialName1)
            {
                m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum];
            }
            else if (m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material.name == materialName2)
            {
                m_childrenObject[childrenNum].gameObject.GetComponent<Renderer>().material = m_blockButtonMaterial[materialNum + 1];
            }
        }
        m_ableColorChange = ableColorChange;
    }
}
