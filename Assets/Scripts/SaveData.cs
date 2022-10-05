using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ���[�U�[�̃Z�[�u�f�[�^���Ǘ�����X�N���v�g
/// </summary>
public class SaveData : MonoBehaviour
{
    //�I���X�e�[�W��
    string m_selectStageName = "";
    //�I���^���N��
    string m_selectTankName = "";

    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //30fps�ŌŒ肷��B
        Application.targetFrameRate = 30;

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

    //�I�����ꂽ�X�e�[�W���Z�b�^�[
    public void SetSelectStageName(string stageName)
    {
        m_selectStageName = stageName;
    }

    //�I�����ꂽ�^���N���Z�b�^�[
    public void SetSelectTankName(string tankName)
    {
        m_selectTankName = tankName;
    }
}