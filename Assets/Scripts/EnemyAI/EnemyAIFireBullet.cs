using UnityEngine;

/// <summary>
/// 敵AIが弾を発射する処理
/// </summary>
namespace nsTankLab
{
public class EnemyAIFireBullet : MonoBehaviour
{
    [SerializeField, TooltipAttribute("弾プレファブオブジェクト")] GameObject m_bulletPrefab = null;
    //弾オブジェクトを格納するオブジェクト
    GameObject m_bulletsBox = null;

    [SerializeField, TooltipAttribute("タンクデータベース")] TankDataBase m_tankDataBase = null;

    SaveData m_saveData = null;

    //発射したタンク番号
    int m_myTankNum = 0;

    //フィールド上に生成されている弾の数
    int m_bulletNum = 0;

    int m_timer = 0;
    const int BULLET_FIRE_DELAY = 120;

    void Start()
    {
        m_bulletsBox = GameObject.Find("Bullets");
        m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //初手ゲーム開始と同時に撃ってしまわないように初めにディレイをかけてからスタートさせる
        m_timer = BULLET_FIRE_DELAY;
    }

    void Update()
    {
        //Rayを作成
        Ray ray = new Ray(transform.root.position, transform.forward);
        //Rayのデバック表示
        Debug.DrawRay(transform.root.position, transform.forward * 5.0f, Color.red);

        //プレイヤーに衝突したときの処理
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                //弾発射
                BulletInstantiate();
            }
        }

        //弾を発射した後のディレイをかける
        //タイマーが作動し0になったら自動でタイマー作動終了
        if (m_timer != 0)
        {
            m_timer--;
        }
    }

    //弾生成処理
    void BulletInstantiate()
    {
        //連射できる回数以上は発射しないようにする
        if (m_bulletNum >= m_tankDataBase.GetTankLists()[m_saveData.GetSelectTankNum(m_myTankNum)].GetRapidFireNum())
        {
            return;
        }

        //発射準備ができていないと発射しないようにする
        if (m_timer != 0)
        {
            return;
        }
        //タイマー作動
        m_timer = BULLET_FIRE_DELAY;

        //弾を生成
        GameObject m_bulletObject = Instantiate(
        m_bulletPrefab,
        transform.position,
        new Quaternion(0.0f, transform.rotation.y, 0.0f, transform.rotation.w)
        );

        //生成される弾の名前変更
        m_bulletObject.name = "EnemyBullet";

        //ヒエラルキー上がごちゃごちゃになってしまうのを防ぐため、親を用意してまとめておく。
        m_bulletObject.transform.parent = m_bulletsBox.transform;

        //生成される弾はタンクと切り離すため、発射したタンクオブジェクトデータを弾スクリプトに渡しておく。
        m_bulletObject.GetComponent<BulletCollision>().SetFireTankObject(gameObject);
    }

    //フィールド上に生成されている弾の数を減らす処理
    public void ReduceBulletNum()
    {
        m_bulletNum = Mathf.Clamp(m_bulletNum-1,0,m_bulletNum);
    }
}
}