using UnityEngine;

/// <summary>
/// 敵AIの初期化処理
/// </summary>
namespace nsTankLab
{
    public class EnemyAIInit : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("敵AIのタンクタイプ")] int m_enemyAIType = 0;

        void Start()
        {
            switch (m_enemyAIType)
            {
                case 0:
                    //周りをキョロキョロするスクリプトを1つ下の階層の子オブジェクトにアタッチする
                    transform.Find("EnemyCannonPivot").gameObject.AddComponent<EnemyAILookDirection>();
                    break;
                case 1:
                    //ランダム移動するスクリプトをアタッチする
                    gameObject.AddComponent<EnemyAIRandomMovement>();
                    break;
            }
        }
    }
}