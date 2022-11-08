using UnityEngine;

/// <summary>
/// �v���C���[���̃L�[�{�[�h���͏���
/// </summary>
namespace nsTankLab
{
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
        //�L�[�{�[�h��̃L�[
        InputKey(KeyCode.Alpha0, "0");
        InputKey(KeyCode.Alpha1, "1");
        InputKey(KeyCode.Alpha2, "2");
        InputKey(KeyCode.Alpha3, "3");
        InputKey(KeyCode.Alpha4, "4");
        InputKey(KeyCode.Alpha5, "5");
        InputKey(KeyCode.Alpha6, "6");
        InputKey(KeyCode.Alpha7, "7");
        InputKey(KeyCode.Alpha8, "8");
        InputKey(KeyCode.Alpha9, "9");
        //�e���L�[
        InputKey(KeyCode.Keypad0, "0");
        InputKey(KeyCode.Keypad1, "1");
        InputKey(KeyCode.Keypad2, "2");
        InputKey(KeyCode.Keypad3, "3");
        InputKey(KeyCode.Keypad4, "4");
        InputKey(KeyCode.Keypad5, "5");
        InputKey(KeyCode.Keypad6, "6");
        InputKey(KeyCode.Keypad7, "7");
        InputKey(KeyCode.Keypad8, "8");
        InputKey(KeyCode.Keypad9, "9");

        //�o�b�N�X�y�[�X�L�[�ňꕶ������
        InputKey(KeyCode.Backspace, "BACK");
        //�G���^�[�L�[�Ŗ��O�m��
        InputKey(KeyCode.Return,"OK");
    }

    //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
    void InputKey(KeyCode keyCode, string inputCharacter)
    {
        if (Input.GetKeyDown(keyCode))
        {
            m_decidePassword.SetCharacter(inputCharacter);
        }
    }
}
}