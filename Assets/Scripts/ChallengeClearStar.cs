using UnityEngine;
using UnityEngine.UI;

namespace nsTankLab
{
    public class ChallengeClearStar : MonoBehaviour
    {
        [SerializeField] Image m_challengeClearStarImage = null;

        void Start()
        {
            //チャレンジクリアしたことがあるとき、
            if (PlayerPrefs.GetInt("ChallengeClear") == 1)
            {
                //星画像を表示する
                m_challengeClearStarImage.enabled = true;
            }
            //チャレンジクリアしたことがないとき、
            else
            {
                //星画像を非表示にする
                m_challengeClearStarImage.enabled = false;
            }
        }
    }
}