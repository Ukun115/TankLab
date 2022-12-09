using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �v���C���[���|�b�v����
/// </summary>
namespace nsTankLab
{
    public class PlayerRepop : MonoBehaviour
    {
        SaveData m_saveData = null;
        SceneSwitcher m_sceneSwitcher = null;

        bool m_isRepop = false;

        void Start()
        {
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
            m_saveData.GetSetmActiveGameTime = true;
        }

        //�Փˏ���
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case TagName.Bullet:
                    m_isRepop = true;
                    break;
            }
        }

        //�v���C���[�����S�����Ƃ�
        void OnDestroy()
        {
            if (m_isRepop)
            {
                switch (SceneManager.GetActiveScene().name)
                {
                    //�g���[�j���O�V�[��
                    case SceneName.TrainingScene:
                        m_sceneSwitcher.StartTransition(SceneName.TrainingScene, false);
                        break;
                    //�}�b�`���O�V�[��
                    case SceneName.MatchingScene:
                        m_sceneSwitcher.StartTransition(SceneName.MatchingScene, false);
                        break;
                }
            }
        }
    }
}