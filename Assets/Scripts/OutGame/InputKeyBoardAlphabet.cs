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
        InputKey(KeyCode.A,"A");
        InputKey(KeyCode.B,"B");
        InputKey(KeyCode.C,"C");
        InputKey(KeyCode.D,"D");
        InputKey(KeyCode.E,"E");
        InputKey(KeyCode.F,"F");
        InputKey(KeyCode.G,"G");
        InputKey(KeyCode.H,"H");
        InputKey(KeyCode.I,"I");
        InputKey(KeyCode.J,"J");
        InputKey(KeyCode.K,"K");
        InputKey(KeyCode.L,"L");
        InputKey(KeyCode.M,"M");
        InputKey(KeyCode.N,"N");
        InputKey(KeyCode.O,"O");
        InputKey(KeyCode.P,"P");
        InputKey(KeyCode.Q,"Q");
        InputKey(KeyCode.R,"R");
        InputKey(KeyCode.S,"S");
        InputKey(KeyCode.T,"T");
        InputKey(KeyCode.U,"U");
        InputKey(KeyCode.V,"V");
        InputKey(KeyCode.W,"W");
        InputKey(KeyCode.X,"X");
        InputKey(KeyCode.Y,"Y");
        InputKey(KeyCode.Z,"Z");

        //�o�b�N�X�y�[�X�L�[�ňꕶ������
        InputKey(KeyCode.Backspace, "BACK");
        //�G���^�[�L�[�Ŗ��O�m��
        InputKey(KeyCode.Return,"OK");
    }

    //�L�[�{�[�h���͂��ꂽ�Ƃ��̏���
    void InputKey(KeyCode keyCode,string inputCharacter)
    {
        if (Input.GetKeyDown(keyCode))
        {
            m_decidePlayerName.SetCharacter(inputCharacter);
        }
    }
}
