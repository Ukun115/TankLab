using UnityEngine;

namespace nsTankLab
{
    public class BulletShield : MonoBehaviour
    {
        SoundManager m_soundManager = null;

        void Start()
        {
            m_soundManager = GameObject.Find("SaveData").GetComponent<SoundManager>();

            Invoke(nameof(DestroyWall), 30.0f);
        }

        void DestroyWall()
        {
            //�e���ꏏ�ɍ폜
            Destroy(transform.root.gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //�V�[���h�ɂ��������I�u�W�F�N�g���e�̏ꍇ�̂ݎ��s
                case TagName.Bullet:
                case TagName.RocketBullet:
                    Damage(other);
                    break;
            }
        }

        void Damage(Collider other)
        {
            //���j���Đ�
            m_soundManager.PlaySE("BrokenWallDestroySE");

            //�e���폜
            Destroy(other.gameObject);

            //�V�[���h���폜
            Destroy(gameObject);

            //�V�[���h3���ׂĂȂ��Ȃ�����A
            if (transform.root.childCount == 1)
            {
                DestroyWall();
            }
        }
    }
}