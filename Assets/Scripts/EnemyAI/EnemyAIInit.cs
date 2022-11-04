using UnityEngine;

/// <summary>
/// �GAI�̏���������
/// </summary>
public class EnemyAIInit : MonoBehaviour
{
    [SerializeField, TooltipAttribute("�GAI�̃^���N�^�C�v")] int m_enemyAIType = 0;

    void Start()
    {
        switch(m_enemyAIType)
        {
            case 0:
                //������L�����L��������X�N���v�g��1���̊K�w�̎q�I�u�W�F�N�g�ɃA�^�b�`����
                this.transform.Find("EnemyCannonPivot").gameObject.AddComponent<EnemyAILookDirection>();
                break;
            case 1:
                //�����_���ړ�����X�N���v�g���A�^�b�`����
                this.gameObject.AddComponent<EnemyAIRandomMovement>();
                break;
        }
    }
}
