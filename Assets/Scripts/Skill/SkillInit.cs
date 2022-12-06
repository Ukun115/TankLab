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
            enDropBombSkill,        //爆弾設置スキル
            enCreateShieldSkill,    //シールド生成スキル
            enBackShieldSkill,      //バックシールドスキル
            enRocketBulletSkill,    //ロケット弾スキル
        }
        EnSelectSkillType m_selectSkillType = EnSelectSkillType.enAccelerationSkill;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            m_playerNum = int.Parse(Regex.Replace(transform.root.name, @"[^1-4]", string.Empty));

            //プレイヤーに選択されたスキルを追加
            AddSkill();
        }

        //プレイヤーに選択されたスキルを追加する処理
        void AddSkill()
        {
            m_selectSkillType = (EnSelectSkillType)m_saveData.GetSelectSkillNum(m_playerNum - 1);

            switch (m_selectSkillType)
            {
                //一定時間加速スキル
                case EnSelectSkillType.enAccelerationSkill:
                    gameObject.AddComponent<AccelerationSkill>();
                    break;

                //少し前の位置に戻るスキル
                case EnSelectSkillType.enReturnPositionSkill:
                    gameObject.AddComponent<ReturnPosition>();
                    break;

                //爆弾設置スキル
                case EnSelectSkillType.enDropBombSkill:
                    gameObject.AddComponent<DropBomb>();
                    break;
                //シールド生成スキル
                case EnSelectSkillType.enCreateShieldSkill:
                    gameObject.transform.Find("PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition").gameObject.AddComponent<CreateShield>();
                    break;
                //バックシールドスキル
                case EnSelectSkillType.enBackShieldSkill:
                    gameObject.transform.Find("PlayerCannonPivot/PlayerCannon/BackShieldPosition").gameObject.AddComponent<CreateBackShield>();
                    break;
                //ロケット弾
                case EnSelectSkillType.enRocketBulletSkill:
                    gameObject.transform.Find("PlayerCannonPivot/PlayerCannon/PlayerFireBulletPosition").gameObject.AddComponent<RocketBullet>();
                    break;
            }
        }
    }
}