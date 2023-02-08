using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace nsTankLab
{
    /// <summary>
    /// バックシールドスキル
    /// </summary>
    public class CreateBackShield : MonoBehaviourPun
    {
        GameObject m_backShieldObject = null;

        bool m_isInstantiate = false;

        SkillCool m_skillCoolScript = null;
        const int COOL_TIME = 9;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            BackShieldInstantiate();
        }

        void Update()
        {
            //バックシールドが消えたら、
            if(m_isInstantiate)
            {
                switch (m_saveData.GetSetSelectGameMode)
                {
                    //チャレンジモード,ローカルプレイ,トレーニング
                    case "CHALLENGE":
                    case "LOCALMATCH":
                    case "TRAINING":
                        Create();
                        break;
                    //オンラインプレイ
                    case "RANDOMMATCH":
                    case "PRIVATEMATCH":
                        if (SceneManager.GetActiveScene().name == SceneName.OnlineGameScene)
                        {
                            photonView.RPC(nameof(Create), RpcTarget.All);
                            m_skillCoolScript.CoolStart(COOL_TIME);
                        }
                        else if (SceneManager.GetActiveScene().name == SceneName.MatchingScene)
                        {
                            Create();
                            m_skillCoolScript.CoolStart(COOL_TIME);
                        }
                        break;
                }
            }
        }

        [PunRPC]
        void Create()
        {
            //数秒後にバックシールドを自動生成
            Invoke(nameof(BackShieldInstantiate), COOL_TIME);

            m_isInstantiate = false;
        }

        void BackShieldInstantiate()
        {
            m_backShieldObject = Instantiate(
                Resources.Load("BackShield") as GameObject,
                transform.position,
                transform.rotation,
                transform
            );
            m_backShieldObject.name = "Shield";
        }

        public void GoInstantiate()
        {
            m_backShieldObject = null;
            m_isInstantiate = true;
        }

        public void SetSkillCoolScript(SkillCool skillCool)
        {
            m_skillCoolScript = skillCool;
        }
    }
}