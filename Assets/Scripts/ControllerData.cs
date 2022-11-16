using System.Linq;
using UnityEngine;

/// <summary>
/// �ڑ�����Ă���R���g���[���[�̃f�[�^��ۑ����Ă���ꏊ
/// </summary>
namespace nsTankLab
{
    public class ControllerData : MonoBehaviour
    {
        string[] controllerNames = {string.Empty};

        void Start()
    {
                SearchConnectedController();

                Debug.Log($"<color=yellow>�ڑ�����Ă���R���g���[���[��:{controllerNames.Length}</color>");
        }

    void Update()
    {
        SearchConnectedController();
    }

    void SearchConnectedController()
    {
        // �ڑ�����Ă���R���g���[���[�̖��O�𒲂ׂ�
        controllerNames = Input.GetJoystickNames();

        //��U���X�g�ɕϊ�
        var list = controllerNames.ToList();

        //�󔒂̗v�f�͔r�����Ă����B
        //�ǂ����R���g���[���[�𔲂��������Ă�����󔒂̗v�f���o�Ă��Ă��܂��o�O������݂���...
        list.RemoveAll(item => item == "");

        //�z��ɕϊ�
        controllerNames = list.ToArray();
    }

    //�v���C���\�ԍ��ƈ�v����R���g���[���[���ڑ�����Ă��邩�ǂ���
    public bool GetIsConnectedController(int playerNum)
    {
        if (controllerNames.Length > playerNum)
        {
            if (controllerNames[playerNum - 1] is not null)
            {
                return true;
            }
        }
        return false;
    }

    //�ڑ�����Ă���R���g���[���[��
    public int GetConnectedControllerNum()
    {
        return controllerNames.Length;
    }
    }
}