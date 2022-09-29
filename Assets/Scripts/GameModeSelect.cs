using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g����ʒ��̃Q�[�����[�h��I�����鏈��
/// </summary>
public class GameModeSelect : MonoBehaviour
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
        //����͂��ꂽ��A(W�L�[�������ꂽ��A)
        if (Input.GetKeyDown(KeyCode.W))
        {
            //�I��ԍ���-1����
            m_selectNum--;

            //��ԏゾ������A
            if(m_selectNum < 0)
            {
                //��ԉ��ɑI�����ڂ�
                m_selectNum = m_cursorPoint.Length-1;
            }


            //�J�[�\���I�u�W�F�N�g���ړ�������
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //�����͂��ꂽ��A(S�L�[�������ꂽ��A)
        if (Input.GetKeyDown(KeyCode.S))
        {

            //�I��ԍ���+1����
            m_selectNum++;

            //��ԉ���������A
            if (m_selectNum > m_cursorPoint.Length-1)
            {
                //��ԏ�ɑI�����ڂ�
                m_selectNum = 0;
            }

            //�J�[�\���I�u�W�F�N�g���ړ�������
            m_cursorObject.transform.position = m_cursorPoint[m_selectNum].transform.position;
        }

        //���N���b�N���ꂽ�Ƃ��A
        if (Input.GetMouseButtonDown(0))
        {
            //�I��ԍ��ɂ���ď����𕪊�
            switch(m_selectNum)
            {
                //�����_���}�b�`
                case 0:

                    //FirstPlay�̃L�[�����݂��Ȃ��ꍇ�̓V�[���J�ڐ�𖼑O���߃V�[���ɍs���悤�ɂ���
                    if (!PlayerPrefs.HasKey("FirstPlay"))
                    {
                        SceneManager.LoadScene("DecideNameScene");
                        //FirstPlay�̃L�[�ɒl�����邱�ƂŁA��x�Ƃ��̃l�X�g�������s���Ȃ��悤�ɂ���
                        PlayerPrefs.SetInt("FirstPlay", 1);
                    }
                    else
                    {
                        //�^���N�I���V�[���ɑJ��
                        SceneManager.LoadScene("SelectTankScene");
                    }

                    break;

                //�v���C�x�[�g�}�b�`
                case 1:

                    //FirstPlay�̃L�[�����݂��Ȃ��ꍇ�̓V�[���J�ڐ�𖼑O���߃V�[���ɍs���悤�ɂ���
                    if (!PlayerPrefs.HasKey("FirstPlay"))
                    {
                        SceneManager.LoadScene("DecideNameScene");
                        //FirstPlay�̃L�[�ɒl�����邱�ƂŁA��x�Ƃ��̃l�X�g�������s���Ȃ��悤�ɂ���
                        PlayerPrefs.SetInt("FirstPlay", 1);
                    }
                    else
                    {

                    }

                    break;

                //�Q�[���I��
                case 2:
                    //UnityEditor�Ńv���C���Ă���Ƃ�
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    //����ȊO
                    #else
                        Application.Quit();
                    #endif
                    break;
            }
        }
    }
}
