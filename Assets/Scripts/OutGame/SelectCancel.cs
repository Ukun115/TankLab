using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///�I�����Ă���^���N���L�����Z�����鏈��
/// </summary>
namespace nsTankLab
{
    public class SelectCancel : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�v���C���[�ԍ�"), Range(1,4)]int m_playerNum = 0;
        [SerializeField, TooltipAttribute("�^���N���X�L����")]string m_tankOrSkill = string.Empty;

        //�Z�[�u�f�[�^
        SaveData m_saveData = null;

        bool m_isPressed = false;

        ControllerData m_controllerData = null;

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();
        }

        void Update()
        {
            m_isPressed = false;

            // �Q�[���p�b�h���ڑ�����Ă�����Q�[���p�b�h�ł̑���
            if (m_controllerData.GetGamepad(m_playerNum) is not null)
            {
                switch (m_tankOrSkill)
                {
                    case "Tank":
                        //LB�{�^��
                         m_isPressed = m_controllerData.GetGamepad(m_playerNum).leftTrigger.wasPressedThisFrame;
                        break;
                    case "Skill":
                        //RB�{�^��
                        m_isPressed = m_controllerData.GetGamepad(m_playerNum).rightTrigger.wasPressedThisFrame;
                        break;
                }
            }
            else
            {
                switch (m_tankOrSkill)
                {
                    case "Tank":
                        //�E�N���b�N����
                        m_isPressed = Mouse.current.rightButton.wasPressedThisFrame;
                        break;
                    case "Skill":
                        //�E�N���b�N����
                        m_isPressed = Mouse.current.rightButton.wasPressedThisFrame;
                        break;
                }
            }

            //�{�^���������ꂽ��A
            if (m_isPressed)
            {
                //�I�����L�����Z��
                Cancel();
            }
        }

        //�I���L�����Z������
        void Cancel()
        {
            switch (m_tankOrSkill)
            {
                case "Tank":
                    //�ۑ�����Ă����^���N���L�����Z��
                    m_saveData.SetSelectTankName(m_playerNum, null);
                    break;
                case "Skill":
                    //�ۑ�����Ă����X�L�����L�����Z��
                    m_saveData.SetSelectSkillName(m_playerNum, null);
                    break;
            }
            //UI�\�����\���ɂ���
            gameObject.SetActive(false);
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
        }
    }
}