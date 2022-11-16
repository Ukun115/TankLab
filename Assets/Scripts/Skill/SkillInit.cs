using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// スキルの初期化処理
/// </summary>
namespace nsTankLab
{
    public class SkillInit : MonoBehaviour
    {
        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            int playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", "")) - 1;

            switch (m_saveData.GetSelectSkillNum(playerNum))
            {
                case 0:
                    //一定時間加速スキル
                    gameObject.AddComponent<AccelerationSkill>();
                    break;

                case 1:
                    //3秒前の位置に戻るスキル
                    gameObject.AddComponent<ReturnPosition>();
                    break;

                case 2:
                    //爆弾設置スキル
                    gameObject.AddComponent<DropBomb>();
                    break;
            }
        }
    }
}