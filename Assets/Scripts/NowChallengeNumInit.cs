using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// チャレンジ番号更新処理
    /// </summary>
    public class NowChallengeNumInit : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TextMeshProUGUI>().text = $"{GameObject.Find("SaveData").GetComponent<SaveData>().GetSetSelectStageNum}";
        }
    }
}