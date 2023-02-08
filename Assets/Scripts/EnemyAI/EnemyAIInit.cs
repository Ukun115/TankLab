using UnityEngine;

/// <summary>
/// �GAI�̏���������
/// </summary>
namespace nsTankLab
{
    public class EnemyAIInit : MonoBehaviour
    {
        enum EnEnemyAIType
        {
            enFixing,           //�Œ肳��Ă���^�C�v
            enRandomMovement,   //�����_���ړ�����^�C�v
        }
        [SerializeField, TooltipAttribute("�GAI�̃^���N�^�C�v")] EnEnemyAIType m_enemyAIType = EnEnemyAIType.enFixing;
        //�X�L��
        [SerializeField] bool m_addBombSkill = false;
        [SerializeField] bool m_addBackShieldSkill = false;

        void Start()
        {
            switch (m_enemyAIType)
            {
                case EnEnemyAIType.enFixing:
                    //������L�����L��������X�N���v�g��1���̊K�w�̎q�I�u�W�F�N�g�ɃA�^�b�`����
                    transform.Find("EnemyCannonPivot").gameObject.AddComponent<EnemyAILookDirection>();
                    break;

                case EnEnemyAIType.enRandomMovement:
                    //�����_���ړ�����X�N���v�g���A�^�b�`����
                    gameObject.AddComponent<EnemyAIRandomMovement>();
                    break;
            }

            //���e�ݒu�X�L��
            if(m_addBombSkill)
            {
                //���e��ݒu����X�N���v�g���A�^�b�`����
                gameObject.AddComponent<EnemyAIBomb>();
            }
            //�o�b�N�V�[���h�X�L��
            if (m_addBackShieldSkill)
            {
                gameObject.transform.Find("EnemyCannonPivot/EnemyCannon/BackShieldPosition").gameObject.AddComponent<EnemyAICreateBackShield>();
            }
        }
    }
}