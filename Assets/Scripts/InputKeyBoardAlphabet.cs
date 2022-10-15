using UnityEngine;

/// <summary>
/// �v���C���[���̃L�[�{�[�h���͏���
/// </summary>
public class InputKeyBoardAlphabet : MonoBehaviour
{
    //�v���C���[������X�N���v�g
    DecidePlayerName m_decidePlayerName = null;

    void Start()
    {
        m_decidePlayerName = GameObject.Find("SceneManager").GetComponent<DecidePlayerName>();
    }

    void Update()
    {
        //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
        FireKeyBoard();
    }

    //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
    void FireKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("A");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("B");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("C");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("D");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("E");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("F");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("G");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("H");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("I");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("J");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("K");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("L");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("M");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("N");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("O");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("P");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("Q");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("R");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("S");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("T");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("U");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("V");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("W");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("X");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("Y");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //�����ꂽ�{�^���̕�����n��
            m_decidePlayerName.SetCharacter("Z");
        }

        //�o�b�N�X�y�[�X
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //�ꕶ������
            m_decidePlayerName.SetCharacter("BACK");
        }

        //�G���^�[�L�[
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //���O�m��
            m_decidePlayerName.SetCharacter("OK");
        }
    }
}
