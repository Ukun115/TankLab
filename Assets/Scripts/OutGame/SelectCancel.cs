using UnityEngine;

/// <summary>
///�I�����Ă���^���N���L�����Z�����鏈��
/// </summary>
public class SelectCancel : MonoBehaviour
{
    [SerializeField, TooltipAttribute("�v���C���[�ԍ�"), Range(1,4)]int m_playerNum = 0;

    [SerializeField, TooltipAttribute("�������{�^���̕�����(LT or RT)")]string m_buttonCharacter = "";

    //�Z�[�u�f�[�^
    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
    }

    void Update()
    {
        //�{�^���������ꂽ��A
        //(�������Ԃ��g�p)
        switch(m_buttonCharacter)
        {
            case "LT":
                if (Input.GetAxisRaw($"{ m_playerNum }PJoystickLT") < -0.1f)
                {
                    //�I�����L�����Z��
                    Cancel();
                }
                break;

            case "RT":
                if (Input.GetAxisRaw($"{ m_playerNum }PJoystickRT") > 0.1f)
                {
                    //�I�����L�����Z��
                    Cancel();
                }
                break;
        }
    }

    //�I���L�����Z������
    void Cancel()
    {
        //�ۑ�����Ă����^���N���L�����Z��
        m_saveData.SetSelectTankName(m_playerNum, null);
        //UI�\�����\���ɂ���
        this.gameObject.SetActive(false);
    }
}
