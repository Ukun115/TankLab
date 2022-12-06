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
            enDropBombSkill,        //���e�ݒu�X�L��
            enCreateShieldSkill,    //�V�[���h�����X�L��
            enBackShieldSkill,      //�o�b�N�V�[���h�X�L��
            enRocketBulletSkill,    //���P�b�g�e�X�L��
        }
        EnSelectSkillType m_selectSkillType = EnSelectSkillType.enAccelerationSkill;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            m_playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", string.Empty));

            //�v���C���[�ɑI�����ꂽ�X�L����ǉ�
            AddSkill();
        }

        //�v���C���[�ɑI�����ꂽ�X�L����ǉ����鏈��
        void AddSkill()
        {
            m_selectSkillType = (EnSelectSkillType)m_saveData.GetSelectSkillNum(m_playerNum - 1);

            switch (m_selectSkillType)
            {
                //��莞�ԉ����X�L��
                case EnSelectSkillType.enAccelerationSkill:
                    gameObject.AddComponent<AccelerationSkill>();
                    break;

                //�����O�̈ʒu�ɖ߂�X�L��
                case EnSelectSkillType.enReturnPositionSkill:
                    gameObject.AddComponent<ReturnPosition>();
                    break;

                //���e�ݒu�X�L��
                case EnSelectSkillType.enDropBombSkill:
                    gameObject.AddComponent<DropBomb>();
                    break;
                //�V�[���h�����X�L��
                case EnSelectSkillType.enCreateShieldSkill:
                    gameObject.transform.Find("PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition").gameObject.AddComponent<CreateShield>();
                    break;
                //�o�b�N�V�[���h�X�L��
                case EnSelectSkillType.enBackShieldSkill:
                    gameObject.transform.Find("PlayerCannonPivot/PlayerCannon/BackShieldPosition").gameObject.AddComponent<CreateBackShield>();
                    break;
                //���P�b�g�e
                case EnSelectSkillType.enRocketBulletSkill:
                    gameObject.transform.Find("PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition").gameObject.AddComponent<RocketBullet>();
                    break;
            }
        }
    }
}