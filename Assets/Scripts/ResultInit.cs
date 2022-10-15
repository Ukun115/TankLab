using UnityEngine;
using TMPro;

/// <summary>
/// ���U���g��ʂ̏���������
/// </summary>
public class ResultInit : MonoBehaviour
{
    //�����v���C���[
    int m_winPlayer = 0;
    //�����e�L�X�g
    TextMeshProUGUI m_winText = null;
    //�����e�L�X�g�J���[(1:1P��,2:2P��)
    Color[] m_winTextColor = { new Color(0.0f, 0.5f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.5f, 1.0f) };

    void Start()
    {
        //�����v���C���[�\��
        m_winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();
        m_winText.text = m_winPlayer + "P Win!!";
        //�����v���C���[�ɂ���ăJ���[�`�F���W
        m_winText.color = m_winTextColor[m_winPlayer-1];
    }

    //�����v���C���[��ݒ肷��Z�b�^�[
    public void SetWinPlayer(int winPlayer)
    {
        m_winPlayer = winPlayer;
    }
}
