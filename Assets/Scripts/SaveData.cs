using UnityEngine;
using TMPro;

/// <summary>
/// ���[�U�[�̃Z�[�u�f�[�^���Ǘ�����X�N���v�g
/// </summary>
public class SaveData : MonoBehaviour
{
    //�I�����C�����[�h���ǂ���
    bool m_isOnline = false;

    //�I�����ꂽ�Q�[�����[�h
    string m_selectGameMode = "";

    //�v���C���[�ԍ�
    int m_playerNum = 0;

    //�I���X�e�[�W��
    string m_selectStageName = "Stage1";

    //�I���^���N��
    string[] m_selectTankName = new string[4];
    //�I���X�L����
    string[] m_selectSkillName = new string[4];
    //���͂��ꂽ�p�X���[�h
    string m_inputPassword = "----";

    [SerializeField, TooltipAttribute("�v���C���[���e�L�X�g")] TextMeshProUGUI m_playerNameText = null;

    void Start()
    {
        //60fps�ŌŒ肷��B
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

    //�I�����ꂽ�Q�[�����[�h�v���p�e�B
    public string GetSetSelectGameMode
    {
        //�Q�b�^�[
        get { return m_selectGameMode; }
        //�Z�b�^�[
        set { m_selectGameMode = value; }
    }

    //�I�����ꂽ�X�e�[�W���v���p�e�B
    public string GetSetSelectStageName
    {
        //�Q�b�^�[
        get { return m_selectStageName; }
        //�Z�b�^�[
        set { m_selectStageName = value; }
    }

    //�I�����ꂽ�^���N���Q�b�^�[
    public string GetSelectTankName(int playerNum)
    {
        return m_selectTankName[playerNum];
    }
    //�I�����ꂽ�^���N���Z�b�^�[
    public void SetSelectTankName(int playerNum, string tankNum)
    {
        m_selectTankName[playerNum-1] = tankNum;
    }
    //�I�����ꂽ�X�L�����Q�b�^�[
    public string GetSelectSkillName(int playerNum)
    {
        return m_selectSkillName[playerNum];
    }
    //�I�����ꂽ�X�L�����Z�b�^�[
    public void SetSelectSkillName(int playerNum, string skillNum)
    {
        m_selectSkillName[playerNum - 1] = skillNum;
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

//#error version
//��C#����o�[�W�����̊m�F