using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// �J�E���g�_�E������
    /// </summary>
    public class CountDown : MonoBehaviour
    {
        TextMeshProUGUI m_countDownText = null;

        int m_timer = 0;

        SaveData m_saveData = null;

        SoundManager m_soundManager = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            m_countDownText = GetComponent<TextMeshProUGUI>();

            //�^���N��e�������Ȃ��悤�ɂ���
            m_saveData.GetSetmActiveGameTime = false;

            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            //BGM����U�~�߂�
            m_soundManager.StopBGM();

            //�Q�[���J�n�����Đ�����
            m_soundManager.PlaySE("GameStartGingleSE");

            m_countDownText.text = "LEADY...";
        }

        void Update()
        {
            switch(m_timer)
            {
                case 240:
                    m_countDownText.text = "GO!!";
                    break;
                case 300:
                    //�^���N��e��������悤�ɂ���
                    m_saveData.GetSetmActiveGameTime = true;
                    //�J�E���g�_�E���X�N���v�g���폜
                    Destroy(gameObject);

                    switch (m_saveData.GetSetSelectGameMode)
                    {
                        //�`�������W���[�h
                        case "CHALLENGE":
                            //�`�������W���[�h��BGM���Đ�����
                            m_soundManager.PlayBGM("GameSceneBGM01");
                            break;
                        //���[�J���v���C
                        //�I�����C���v���C�̏ꍇ
                        case "LOCALMATCH":
                        case "RANDOMMATCH":
                        case "PRIVATEMATCH":
                            //���[�J���v���C���[�h�A�I�����C�����[�h��BGM���Đ�����
                            m_soundManager.PlayBGM("GameSceneBGM02");
                            break;
                    }
                    break;
            }

            m_timer++;
        }
    }
}