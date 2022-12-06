using UnityEngine;

namespace nsTankLab
{
    public class BulletBackShield : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //シールドにあたったオブジェクトが弾の場合のみ実行
                case TagName.Bullet:
                    //弾を削除
                    Destroy(other.gameObject);
                    //シールドを削除
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