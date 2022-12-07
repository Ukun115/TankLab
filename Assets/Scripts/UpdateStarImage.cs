using UnityEngine;
using UnityEngine.UI;

namespace nsTankLab
{
    /// <summary>
    /// スター画像更新処理
    /// </summary>
    public class UpdateStarImage : MonoBehaviour
    {
        SaveData m_saveData = null;

        //星画像
        [SerializeField] Sprite[] m_starSprite = null;

        //スターの主プレイヤー
        [SerializeField] int m_playerNum = 0;

        //スターの番号
        [SerializeField] int m_starNum = 0;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            //スターを取得していたら、
            if (m_saveData.GetStar(m_playerNum - 1, m_starNum-1))
            {
                //スター画像をつける
                GetComponent<Image>().sprite = m_starSprite[m_playerNum-1];

                return;
            }
        }
    }
}