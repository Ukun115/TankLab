using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ���[�U�[�̃Z�[�u�f�[�^���Ǘ�����X�N���v�g
/// </summary>
public class SaveData : MonoBehaviour
{
    //�I�����C�����[�h���ǂ���
    bool m_isOnline = false;

    int m_playerNum = 0;

    //�I���X�e�[�W��
    string m_selectStageName = "Stage1";
    //�I���^���N��
    string m_selectTankName = "Tank1";
    //���͂��ꂽ�p�X���[�h
    string m_inputPassword = "----";

    [SerializeField] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //30fps�ŌŒ肷��B
        Application.targetFrameRate = 60;

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

    //�I�����ꂽ�X�e�[�W���v���p�e�B
    public string GetSetSelectStageName
    {
        //�Q�b�^�[
        get { return m_selectStageName; }
        //�Z�b�^�[
        set { m_selectStageName = value; }
    }

    //�I�����ꂽ�^���N���v���p�e�B
    public string GetSetSelectTankName
    {
        //�Q�b�^�[
        get { return m_selectTankName; }
        //�Z�b�^�[
        set { m_selectTankName = value; }
    }

    //�I�����C�����[�h�t���O�v���p�e�B
    public bool GetSetIsOnline
    {
        //�Q�b�^�[
        get { return m_isOnline; }
        //�Z�b�^�[
        set { m_isOnline = value; }
    }

    //���͂��ꂽ�p�X���[�h�v���p�e�B
    public string GetSetInputPassword
    {
        //�Q�b�^�[
        get { return m_inputPassword; }
        //�Z�b�^�[
        set { m_inputPassword = value; }
    }

    //�v���C���[�ԍ�
    public int GetSetPlayerNum
    {
        //�Q�b�^�[
        get { return m_playerNum; }
        //�Z�b�^�[
        set { m_playerNum = value; }
    }
}