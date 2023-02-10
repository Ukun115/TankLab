using UnityEngine;

/// <summary>
/// �g���[�j���O���[�h�̎��A�GAI�����񂾂畜������悤�ɂ��鏈��
/// </summary>
namespace nsTankLab
{
    public class EnemyAIRepop : MonoBehaviour
    {
        SceneSwitcher m_sceneSwitcher = null;

        bool m_isRepop = false;

        void Start()
        {
            m_sceneSwitcher = GameObject.Find("Transition").GetComponent<SceneSwitcher>();
        }

        //�Փˏ���
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case TagName.Bullet:
                case TagName.RocketBullet:
                    m_isRepop = true;
                    break;
            }
        }

        //���S�����Ƃ�
        void OnDestroy()
        {
            if (m_isRepop)
            {
                m_sceneSwitcher.StartTransition(SceneName.TrainingScene, false);
            }
        }
    }
}