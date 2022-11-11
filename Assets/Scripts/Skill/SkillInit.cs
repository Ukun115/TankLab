using UnityEngine;

/// <summary>
/// �X�L���̏���������
/// </summary>
namespace nsTankLab
{
    public class SkillInit : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�X�L���̃^�C�v")] int m_skillType = 0;

        void Start()
        {
            switch (m_skillType)
            {
                case 0:
                    //��莞�ԉ���
                    gameObject.AddComponent<AccelerationSkill>();
                    break;
                case 1:
                    //3�b�O�̈ʒu�ɖ߂�
                    gameObject.AddComponent<ReturnPosition>();
                    break;
            }
        }
    }
}