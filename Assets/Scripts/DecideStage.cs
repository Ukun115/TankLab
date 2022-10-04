using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideStage : MonoBehaviour
{
    [SerializeField] GameObject m_cursorObject = null;
    [SerializeField] GameObject[] m_cursorPosition = null;

    public void SetCursorPosition(string character)
    {
        switch (character)
        {
            case "STAGE1":
                //�J�[�\���ړ�
                m_cursorObject.transform.position = m_cursorPosition[0].transform.position;
                break;
            case "STAGE2":
                //�J�[�\���ړ�
                m_cursorObject.transform.position = m_cursorPosition[1].transform.position;
                break;
        }
    }

    public void SetCharacter(string character)
    {
        switch (character)
        {
            case "STAGE1":
                //�X�e�[�W1�ɑJ��
                SceneManager.LoadScene("Stage1");
                break;
            case "STAGE2":
                //�X�e�[�W2�ɑJ��
                SceneManager.LoadScene("Stage2");
                break;
        }
    }
}