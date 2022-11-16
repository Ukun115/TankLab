using UnityEngine;
using TMPro;

/// <summary>
/// ���U���g��ʂ̏���������
/// </summary>
namespace nsTankLab
{
public class ResultInit : MonoBehaviour
{
    //�����v���C���[
    int m_winPlayer = 0;
    //�����e�L�X�g
    TextMeshProUGUI m_winText = null;
    //�����e�L�X�g�J���[(1:1P��,2:2P��,3:3P��,4:4P��)
    Color[] m_winTextColor = { new Color(0.0f, 0.5f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.5f, 1.0f), new Color(1.0f, 0.5f, 0.15f, 1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f) };

        SaveData m_saveData = null;

    void Start()
    {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //�����v���C���[�\��
        m_winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
            //�`�������W���[�h�œGAI�����������ꍇ
            if (m_winPlayer == 5)
            {
                m_winText.text = "Game Over!!";
            }
            //�`�������W���[�h�Ń`�������W�����ׂăN���A�����ꍇ
            else if (m_winPlayer == 6)
            {
                m_winText.text = "Challenge Clear!!";
            }
            //�����ꂩ�̃v���C���[�����������ꍇ
            else
            {
                m_winText.text = $"{m_winPlayer}P Win!!";
                //�����v���C���[�ɂ���ăJ���[�`�F���W
                m_winText.color = m_winTextColor[m_winPlayer - 1];
            }

        //�R�b��Ƀ^�C�g����ʂɖ߂�
        Invoke(nameof(BackTitleScene), 3f);
    }

    //�����v���C���[��ݒ肷��Z�b�^�[
    public void SetWinPlayer(int winPlayer)
    {
        m_winPlayer = winPlayer;
    }

    //�^�C�g���V�[���ɖ߂鏈��
    void BackTitleScene()
    {
            m_saveData.SaveDataInit();
       GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("TitleScene");
    }
}
}