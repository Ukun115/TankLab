using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���̃L�[�{�[�h����
/// </summary>
public class InputKeyBoardNumbers : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("5");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("6");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("7");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("8");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("9");
        }


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //�ꕶ������
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("BACK");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //���O�m��
            GameObject.Find("SceneManager").GetComponent<DecidePassword>().SetCharacter("OK");
        }
    }
}
