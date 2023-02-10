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
            //親も一緒に削除
            Destroy(transform.root.gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //シールドにあたったオブジェクトが弾の場合のみ実行
                case TagName.Bullet:
                case TagName.RocketBullet:
                    Damage(other);
                    break;
            }
        }

        void Damage(Collider other)
        {
            //撃破音再生
            m_soundManager.PlaySE("BrokenWallDestroySE");

            //弾を削除
            Destroy(other.gameObject);

            //シールドを削除
            Destroy(gameObject);

            //シールド3つすべてなくなったら、
            if (transform.root.childCount == 1)
            {
                DestroyWall();
            }
        }
    }
}