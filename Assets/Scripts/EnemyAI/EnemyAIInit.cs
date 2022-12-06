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
            enFixing,   //固定されているタイプ
            enRandomMovement  //ランダム移動するタイプ
        }
        [SerializeField, TooltipAttribute("敵AIのタンクタイプ")] EnEnemyAIType m_enemyAIType = EnEnemyAIType.enFixing;

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
        }
    }
}