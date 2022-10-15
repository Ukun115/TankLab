using UnityEngine;

/// <summary>
/// �v���C���[���̃L�[�{�[�h���͏���
/// </summary>
public class InputKeyBoardNumbers : MonoBehaviour
{
    //�p�X���[�h���菈���X�N���v�g
    DecidePassword m_decidePassword = null;

    void Start()
    {
        m_decidePassword = GameObject.Find("SceneManager").GetComponent<DecidePassword>();
    }

    void Update()
    {
        //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
        FireKeyboard();
    }

    //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
    void FireKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("5");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("6");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("7");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("8");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePassword.SetCharacter("9");
        }

        //�o�b�N�X�y�[�X
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //�ꕶ������
            m_decidePassword.SetCharacter("BACK");
        }

        //�G���^�[�L�[
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //���O�m��
            m_decidePassword.SetCharacter("OK");
        }
    }
}
