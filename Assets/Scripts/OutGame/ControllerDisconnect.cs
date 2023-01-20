using UnityEngine;

/// <summary>
/// �R���g���[���[���ؒf���ꂽ�Ƃ��ɌĂ΂�鏈��
/// </summary>
namespace nsTankLab
{
    public class ControllerDisconnect : MonoBehaviour
    {
        ControllerData m_controllerData = null;
        SaveData m_saveData = null;

        [SerializeField]GameObject m_countDownObject = null;

        void Start()
        {
            m_controllerData = GameObject.Find("SaveData").GetComponent<ControllerData>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //�R���g���[���[��4�Ȃ����Ă��Ȃ���ԂɂȂ�����A
            if(m_controllerData.GetConnectGamepad() < 4)
            {
                //�R���g���[���[���ؒf����Ă��鏈��

                //�Q�[���i�s���~�߂�
                m_saveData.GetSetmActiveGameTime = false;

                Debug.Log("�R���g���[���[���ؒf����܂����B\n�Đڑ����Ă��������B");
            }
            else if(m_countDownObject is null)
            {
                //�Q�[���i�s��i�߂�
                m_saveData.GetSetmActiveGameTime = true;
            }
        }
    }
}