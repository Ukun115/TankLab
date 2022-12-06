using UnityEngine;

namespace nsTankLab
{
    public class BulletBackShield : MonoBehaviour
    {
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
                    break;
            }
        }

        void OnDestroy()
        {
            transform.parent.gameObject.transform.parent.GetComponent<CreateBackShield>().GoInstantiate();
        }
    }
}