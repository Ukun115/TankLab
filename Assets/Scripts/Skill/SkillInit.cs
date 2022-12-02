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

        int m_playerNum = 0;

        //選ばれるスキルの種類
        enum EnSelectSkillType
        {
            enAccelerationSkill,    //一定時間加速スキル
            enReturnPositionSkill,  //少し前の位置に戻るスキル
            enDropBombSkill         //爆弾設置スキル
        }

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            m_playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", ""));

            //プレイヤーに選択されたスキルを追加
            AddSkill();
        }

        //プレイヤーに選択されたスキルを追加する処理
        void AddSkill()
        {
            switch (m_saveData.GetSelectSkillNum(m_playerNum-1))
            {
                //一定時間加速スキル
                case (int)EnSelectSkillType.enAccelerationSkill:
                    gameObject.AddComponent<AccelerationSkill>();
                    break;

                //少し前の位置に戻るスキル
                case (int)EnSelectSkillType.enReturnPositionSkill:
                    gameObject.AddComponent<ReturnPosition>();
                    break;

                //爆弾設置スキル
                case (int)EnSelectSkillType.enDropBombSkill:
                    gameObject.AddComponent<DropBomb>();
                    break;
            }
        }
    }
}