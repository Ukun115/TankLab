using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���̃L�[�{�[�h����
/// </summary>
public class InputKeyBoardAlphabet : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("A");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("B");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("C");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("D");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("E");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("F");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("G");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("H");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("I");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("J");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("K");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("L");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("M");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("N");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("O");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("P");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("Q");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("R");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("S");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("T");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("U");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("V");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("W");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("X");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("Y");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //�����ꂽ�{�^���̕�����n��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("Z");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //�ꕶ������
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("BACK");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //���O�m��
            GameObject.Find("SceneManager").GetComponent<DecidePlayerName>().SetCharacter("OK");
        }
    }
}
