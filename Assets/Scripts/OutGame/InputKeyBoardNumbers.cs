using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// �v���C���[���̃L�[�{�[�h���͏���
/// </summary>
namespace nsTankLab
{
    public class InputKeyBoardNumbers : MonoBehaviour
    {
        //�p�X���[�h���菈���X�N���v�g
        [SerializeField]DecidePassword m_decidePassword = null;

        void Update()
        {
            //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
            FireKeyboard();
        }

        //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
        void FireKeyboard()
        {
            InputKey(Keyboard.current.numpad0Key, "0");
            InputKey(Keyboard.current.numpad1Key, "1");
            InputKey(Keyboard.current.numpad2Key, "2");
            InputKey(Keyboard.current.numpad3Key, "3");
            InputKey(Keyboard.current.numpad4Key, "4");
            InputKey(Keyboard.current.numpad5Key, "5");
            InputKey(Keyboard.current.numpad6Key, "6");
            InputKey(Keyboard.current.numpad7Key, "7");
            InputKey(Keyboard.current.numpad8Key, "8");
            InputKey(Keyboard.current.numpad9Key, "9");

            //�o�b�N�X�y�[�X�L�[�ňꕶ������
            InputKey(Keyboard.current.backspaceKey, "BACK");
            //�G���^�[�L�[�Ŗ��O�m��
            InputKey(Keyboard.current.enterKey,"OK");
        }

        //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
        void InputKey(KeyControl key, string inputCharacter)
        {
            if (key.wasPressedThisFrame)
            {
                m_decidePassword.SetCharacter(inputCharacter);
            }
        }
    }
}