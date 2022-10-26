using System.Linq;
using UnityEngine;

public class ControllerData : MonoBehaviour
{
    string[] controllerNames = { "" };

    void Start()
    {
        SearchConnectedController();

        Debug.Log($"�ڑ�����Ă���R���g���[���[����{controllerNames.Length}�ł��B");
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
        if (controllerNames.Length >= playerNum)
        {
            if (controllerNames[playerNum - 1] != null)
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