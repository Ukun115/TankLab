using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// �X�L���̏���������
/// </summary>
namespace nsTankLab
{
    public class SkillInit : MonoBehaviour
    {
        SaveData m_saveData = null;

        int m_playerNum = 0;

        //�I�΂��X�L���̎��
        enum EnSelectSkillType
        {
            enAccelerationSkill,    //��莞�ԉ����X�L��
            enReturnPositionSkill,  //�����O�̈ʒu�ɖ߂�X�L��
            enDropBombSkill         //���e�ݒu�X�L��
        }

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            m_playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", ""));

            //�v���C���[�ɑI�����ꂽ�X�L����ǉ�
            AddSkill();
        }

        //�v���C���[�ɑI�����ꂽ�X�L����ǉ����鏈��
        void AddSkill()
        {
            switch (m_saveData.GetSelectSkillNum(m_playerNum-1))
            {
                //��莞�ԉ����X�L��
                case (int)EnSelectSkillType.enAccelerationSkill:
                    gameObject.AddComponent<AccelerationSkill>();
                    break;

                //�����O�̈ʒu�ɖ߂�X�L��
                case (int)EnSelectSkillType.enReturnPositionSkill:
                    gameObject.AddComponent<ReturnPosition>();
                    break;

                //���e�ݒu�X�L��
                case (int)EnSelectSkillType.enDropBombSkill:
                    gameObject.AddComponent<DropBomb>();
                    break;
            }
        }
    }
}