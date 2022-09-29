using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �^���N�I����ʒ��̃^���N��I�����鏈��
/// </summary>
public class TankSelect : MonoBehaviour
{
    //�J�[�\���ƂȂ�I�u�W�F�N�g
    [SerializeField] GameObject m_cursorObject = null;
    //�J�[�\���̃|�W�V�����ƂȂ�|�C���g
    [SerializeField] GameObject[] m_cursorPoint = null;
    //���ݑI������Ă���ԍ�
    int m_selectNum = 0;

    void Start()
    {
        //�J�[�\���I�u�W�F�N�g�̈ʒu�������ʒu�ɒu��
        m_cursorObject.transform.position = m_cursorPoint[0].transform.position;
    }

    void Update()
    {
        //�����͂��ꂽ��A(A�L�[�������ꂽ��A)
        if (Input.GetKeyDown(KeyCode.A))
        {
            //�I��ԍ���-1����
            m_selectNum--;

            //��ԍ���������A
            if(m_selectNum < 0)
            {
                //��ԉE�ɑI�����ڂ�
                m_selectNum = m_cursorPoint.Length-1;
            }


            //�J�[�\���I�u�W�F�N�g���ړ�������
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //�E���͂��ꂽ��A(D�L�[�������ꂽ��A)
        if (Input.GetKeyDown(KeyCode.D))
        {

            //�I��ԍ���+1����
            m_selectNum++;

            //��ԉE��������A
            if (m_selectNum > m_cursorPoint.Length-1)
            {
                //��ԍ��ɑI�����ڂ�
                m_selectNum = 0;
            }

            //�J�[�\���I�u�W�F�N�g���ړ�������
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //���N���b�N���ꂽ�Ƃ��A
        if (Input.GetMouseButtonDown(0))
        {
            //�X�e�[�W�I���V�[���ɑJ��
            SceneManager.LoadScene("SelectStageScene");
        }
    }
}
