using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DecidePlayerName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //���ɂ��ււƂ������O�ɂ��Ă���
        m_playerNameText.text = "ehehe";
    }

    void Update()
    {
        //�v���C���[������

        //���N���b�N���ꂽ�Ƃ��A
        if (Input.GetMouseButtonDown(0))
        {
            //���O�ɂł��Ȃ����O�̏ꍇ�V�[���J�ڂł��Ȃ��悤�ɂ���

            //�I������Ă������[�h�ɂ���ăV�[���J�ڐ�𕪊�

            //�^���N�I���V�[���ɑJ��
            SceneManager.LoadScene("SelectTankScene");
        }
    }
}
