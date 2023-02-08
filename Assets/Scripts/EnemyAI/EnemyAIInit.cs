using UnityEngine;

/// <summary>
/// 敵AIの初期化処理
/// </summary>
namespace nsTankLab
{
    public class EnemyAIInit : MonoBehaviour
    {
        enum EnEnemyAIType
        {
            enFixing,           //固定されているタイプ
            enRandomMovement,   //ランダム移動するタイプ
        }
        [SerializeField, TooltipAttribute("敵AIのタンクタイプ")] EnEnemyAIType m_enemyAIType = EnEnemyAIType.enFixing;
        //スキル
        [SerializeField] bool m_addBombSkill = false;
        [SerializeField] bool m_addBackShieldSkill = false;

        void Start()
        {
            switch (m_enemyAIType)
            {
                case EnEnemyAIType.enFixing:
                    //周りをキョロキョロするスクリプトを1つ下の階層の子オブジェクトにアタッチする
                    transform.Find("EnemyCannonPivot").gameObject.AddComponent<EnemyAILookDirection>();
                    break;

                case EnEnemyAIType.enRandomMovement:
                    //ランダム移動するスクリプトをアタッチする
                    gameObject.AddComponent<EnemyAIRandomMovement>();
                    break;
            }

            //爆弾設置スキル
            if(m_addBombSkill)
            {
                //爆弾を設置するスクリプトをアタッチする
                gameObject.AddComponent<EnemyAIBomb>();
            }
            //バックシールドスキル
            if (m_addBackShieldSkill)
            {
                gameObject.transform.Find("EnemyCannonPivot/EnemyCannon/BackShieldPosition").gameObject.AddComponent<EnemyAICreateBackShield>();
            }
        }
    }
}