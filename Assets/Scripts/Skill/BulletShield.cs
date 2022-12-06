using UnityEngine;

namespace nsTankLab
{
    public class BulletShield : MonoBehaviour
    {
        void Start()
        {
            Invoke(nameof(DestroyWall), 5.0f);
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
                    //�e���폜
                    Destroy(other.gameObject);
                    //�V�[���h���폜
                    Destroy(gameObject);

                    //�V�[���h3���ׂĂȂ��Ȃ�����A
                    if(transform.root.childCount == 1)
                    {
                        DestroyWall();
                    }

                    break;
            }
        }
    }
}