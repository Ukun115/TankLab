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
            //親も一緒に削除
            Destroy(transform.root.gameObject);
        }

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

                    //シールド3つすべてなくなったら、
                    if(transform.root.childCount == 1)
                    {
                        DestroyWall();
                    }

                    break;
            }
        }
    }
}