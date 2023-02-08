using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

        [SerializeField] OnlineMatchingMaker m_onlineMatchingMaker = null;

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
                        //���|�b�v
                        m_sceneSwitcher.StartTransition(SceneName.TrainingScene, false);
                        break;
                    //�}�b�`���O�V�[��
                    case SceneName.MatchingScene:
                        //�}�b�`���O�������Ă�����A���|�b�v�����͍s��Ȃ��B
                        if (!m_onlineMatchingMaker.IsMatched())
                        {
                            m_onlineMatchingMaker.DestroyGameObject();
                            m_sceneSwitcher.StartTransition(SceneName.MatchingScene, false);
                        }
                        break;
                }
            }
        }
    }
}