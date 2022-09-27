using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//���U���g��ʂ̏���������
public class ResultInit : MonoBehaviour
{
    int m_winPlayer = 0;
    TextMeshProUGUI m_winText = null;

    void Start()
    {
        //�����v���C���[�\��
        m_winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        m_winText.text = m_winPlayer + "P Win!!";
        //�����v���C���[�ɂ���ăJ���[�`�F���W
        switch(m_winPlayer)
        {
            //1P�̏�����
            case 0:
                m_winText.color = new Color(1.0f,0.0f,0.5f,1.0f);
                break;
                //2P�̏�����
            case 1:
                m_winText.color = new Color(0.0f,0.5f,1.0f,1.0f);
                break;
        }
    }

    //�����v���C���[��ݒ肷��Z�b�^�[
    public void SetWinPlayer(int winPlayer)
    {
        m_winPlayer = winPlayer;
    }
}
