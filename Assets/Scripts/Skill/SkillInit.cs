using UnityEngine;

/// <summary>
/// スキルの初期化処理
/// </summary>
namespace nsTankLab
{
    public class SkillInit : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("スキルのタイプ")] int m_skillType = 0;

        void Start()
        {
            switch (m_skillType)
            {
                case 0:
                    //一定時間加速
                    gameObject.AddComponent<AccelerationSkill>();
                    break;
                case 1:
                    //3秒前の位置に戻る
                    gameObject.AddComponent<ReturnPosition>();
                    break;
            }
        }
    }
}