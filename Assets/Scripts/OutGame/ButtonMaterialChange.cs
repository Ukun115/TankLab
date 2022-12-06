using UnityEngine;

/// <summary>
/// �{�^���̃}�e���A����ύX���鏈��
/// </summary>
namespace nsTankLab
{
    public class ButtonMaterialChange : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�u���b�N�{�^���̃}�e���A��")] Material[] m_blockButtonMaterial = null;

        //�q�I�u�W�F�N�g�}�e���A���B������z��
        MeshRenderer[] m_childMeshRenderer = { null };

        //�J���[�`�F���W���ł��邩�ǂ���
        bool m_ableColorChange = false;

        //�}�e���A����ς��鏈��
        public void ChangeMaterial(RaycastHit hit)
        {
            // �q�I�u�W�F�N�g�}�e���A���B������z��̏�����
            m_childMeshRenderer = new MeshRenderer[hit.collider.gameObject.transform.childCount];

            //�q�I�u�W�F�N�g�̃}�e���A�����擾
            for (int childrenNum = 0; childrenNum < hit.collider.gameObject.transform.childCount; childrenNum++)
            {
                m_childMeshRenderer[childrenNum] = hit.collider.gameObject.transform.GetChild(childrenNum).gameObject.GetComponent<MeshRenderer>();
            }

            //�}�e���A���̃J���[�`�F���W����
            DecideMaterialColor("Gray_Light (Instance)", "Gray_Normal (Instance)", 1, true);
        }

        //�}�e���A����߂�����
        public void ReturnChangeMaterial()
        {
            if (m_childMeshRenderer is not null && m_ableColorChange)
            {
                //�O��}�E�X�J�[�\�����������Ă����u���b�N�{�^���I�u�W�F�N�g�����Ƃ̐F�ɖ߂�
                //�}�e���A���̃J���[�`�F���W����
                DecideMaterialColor("Gray_Normal (Instance)", "Gray_Dark (Instance)", 0, false);
            }
        }

        //�}�e���A���̃J���[�`�F���W����
        void DecideMaterialColor(string materialName1, string materialName2, int materialNum, bool ableColorChange)
        {
            //�u���b�N�{�^���̐F(�}�e���A��)��ύX����
            for (int childrenNum = 0; childrenNum < m_childMeshRenderer.Length; childrenNum++)
            {
                //���蓖�Ă��Ă���}�e���A���ɂ���ĕύX��̃}�e���A�������肷��
                if (m_childMeshRenderer[childrenNum].material.name == materialName1)
                {
                    m_childMeshRenderer[childrenNum].material = m_blockButtonMaterial[materialNum];
                }
                else if (m_childMeshRenderer[childrenNum].material.name == materialName2)
                {
                    m_childMeshRenderer[childrenNum].material = m_blockButtonMaterial[materialNum + 1];
                }
            }
            m_ableColorChange = ableColorChange;
        }
    }
}