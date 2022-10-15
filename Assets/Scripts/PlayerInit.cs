using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// �v���C���[�̏���������
/// </summary>
public class PlayerInit : MonoBehaviourPun
{
    //1P2P�̋@�̂̃}�e���A���J���[(1P:Blue,2P:Red)
    [SerializeField] Material[] m_tankColor = new Material[2];
    //�}�e���A���ԍ�
    int m_materialNum = 0;
    //�v���C���[�I�u�W�F�N�g��
    string m_objectName = "";

    SaveData m_saveData = null;

    void Start()
    {
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�I�����C����������A
        if (m_saveData.GetSetIsOnline && !photonView.IsMine && SceneManager.GetActiveScene().name == "OnlineGameScene")
        {
            if (m_saveData.GetSetPlayerNum == 0)
            {
                //��������Q�[���I�u�W�F�N�g�̖��O��Player2�ɂ���
                m_objectName = "2P";
                //�J���[��ݒ肷��(��)
                m_materialNum = 1;
            }
            else
            {
                //��������Q�[���I�u�W�F�N�g�̖��O��Player1�ɂ���
                m_objectName = "1P";
                //�J���[��ݒ肷��(��)
                m_materialNum = 0;
            }
        }
        //�I�t���C����������A
        else
        {
            //��������Q�[���I�u�W�F�N�g�̖��O��Player1or2�ɂ���
            m_objectName =(m_saveData.GetSetPlayerNum + 1) + "P";
            //�J���[��ݒ肷��(��or��)
            m_materialNum = m_saveData.GetSetPlayerNum;
        }

        //�^���N�̖��O�ƃJ���[��ύX
        this.gameObject.name = m_objectName;
        this.gameObject.GetComponent<MeshRenderer>().material = m_tankColor[m_materialNum];
    }
}
