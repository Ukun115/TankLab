using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ���[�U�[�̃Z�[�u�f�[�^���Ǘ�����X�N���v�g
/// </summary>
public class SaveData : MonoBehaviour
{
    //�I���X�e�[�W�ԍ�
    int m_selectStageNum = 0;
    //�I���^���N�ԍ�
    int m_selectTankNum = 0;

    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //�ȑO�̃v���C�Ŗ��O���o�^����Ă����ꍇ�A
        if(PlayerPrefs.HasKey("PlayerName"))
        {
            //�o�^����Ă������O��\��������
            m_playerNameText.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            m_playerNameText.text = "NoName";
        }

        //�V�[���J�ڂ��Ă����̃I�u�W�F�N�g�͔j�����ꂸ�ɕێ������܂܂ɂ���
        DontDestroyOnLoad(this);
    }
}