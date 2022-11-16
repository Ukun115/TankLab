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

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            int playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", "")) - 1;

            switch (m_saveData.GetSelectSkillNum(playerNum))
            {
                case 0:
                    //��莞�ԉ����X�L��
                    gameObject.AddComponent<AccelerationSkill>();
                    break;

                case 1:
                    //3�b�O�̈ʒu�ɖ߂�X�L��
                    gameObject.AddComponent<ReturnPosition>();
                    break;

                case 2:
                    //���e�ݒu�X�L��
                    gameObject.AddComponent<DropBomb>();
                    break;
            }
        }
    }
}