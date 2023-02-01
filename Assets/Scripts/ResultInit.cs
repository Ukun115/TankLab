using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
        [SerializeField]TextMeshProUGUI m_winText = null;
        //�����e�L�X�g�J���[(1:1P��,2:2P��,3:3P��,4:4P��)
        Color[] m_winTextColor = { new Color(0.0f, 0.5f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.5f, 1.0f), new Color(1.0f, 0.5f, 0.15f, 1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f) };

        SaveData m_saveData = null;

        SceneSwitcher m_sceneSwitcher = null;

        //���摜
        [SerializeField] Sprite[] m_starSprite = null;

        [SerializeField] List<StarList> m_starList = new List<StarList>();

        void Start()
        {
            //�R���|�[�l���g�擾�܂Ƃ�
            GetComponents();

            //�����v���C���[�\��
            //�`�������W���[�h�œGAI�����������ꍇ
            if (m_winPlayer == 5)
            {
                m_winText.text = "Game Over!!";

                //6�b��Ƀ^�C�g����ʂɖ߂�
                Invoke(nameof(BackTitleScene), 6.0f);
            }
            //�`�������W���[�h�Ń`�������W�����ׂăN���A�����ꍇ
            else if (m_winPlayer == 6)
            {
                m_winText.text = "Clear All Challenge!!";

                //�`�������W�N���A�������Ƃ�ۑ�����
                PlayerPrefs.SetInt("ChallengeClear",1);
                PlayerPrefs.Save();

                //5�b��Ƀ^�C�g����ʂɖ߂�
                Invoke(nameof(BackTitleScene), 5.0f);
            }
            //�����ꂩ�̃v���C���[�����������ꍇ(���[�J��)
            else if(SceneManager.GetActiveScene().name == SceneName.LocalGameScene || SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
            {
                m_winText.text = $"{m_winPlayer}P Win!!";
                //�����v���C���[�ɂ���ăJ���[�`�F���W
                m_winText.color = m_winTextColor[m_winPlayer - 1];

                //�X�^�[�摜���X�V
                UpdateStarImage();

                //�X�^�[���Q�������A
                if (m_saveData.GetStar(m_winPlayer - 1, 1))
                {
                    //�R�b��Ƀ^�C�g����ʂɖ߂�
                    Invoke(nameof(BackTitleScene), 3.0f);
                }
                else
                {
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case SceneName.LocalGameScene:
                            //3�b��ɍēx���[�J���Q�[���V�[���ɑJ�ڂ���B
                            Invoke(nameof(BackLocalGameScene), 3.0f);
                            break;
                        case SceneName.OnlineGameScene:
                            //3�b��ɍēx�I�����C���Q�[���V�[���ɑJ�ڂ���B
                            Invoke(nameof(BackOnlineGameScene), 3.0f);
                            break;
                    }
                }
            }
        }

        //�X�^�[�摜���X�V���鏈��
        void UpdateStarImage()
        {
            for (int starNum = 0; starNum < 2; starNum++)
            {
                //�X�^�[���擾���Ă��Ȃ�������A
                if (!m_saveData.GetStar(m_winPlayer - 1, starNum))
                {
                    //�X�^�[�擾�ς݂ɂ���
                    m_saveData.ActiveStar(m_winPlayer - 1, starNum);
                    //�X�^�[�摜������
                    m_starList[m_winPlayer - 1].GetStarUiList(starNum).sprite = m_starSprite[m_winPlayer-1];

                    return;
                }
            }
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
            m_sceneSwitcher.StartTransition(SceneName.TitleScene);
        }

        //�ēx���[�J���Q�[���V�[���ɖ߂鏈��
        void BackLocalGameScene()
        {
            m_sceneSwitcher.StartTransition(SceneName.LocalGameScene);
        }

        //�ēx�I�����C���Q�[���V�[���ɖ߂鏈��
        void BackOnlineGameScene()
        {
            m_sceneSwitcher.StartTransition(SceneName.OnlineGameScene);
        }

        //�R���|�[�l���g�擾
        void GetComponents()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        }
    }

    [System.Serializable]
    public class StarList
    {
        [SerializeField] List<Image> m_starUI = new List<Image>();

        public Image GetStarUiList(int starNum)
        {
            return m_starUI[starNum];
        }
    }
}