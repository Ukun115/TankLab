using UnityEngine;
using TMPro;

namespace nsTankLab
{
    /// <summary>
    /// �`�������W�ԍ��X�V����
    /// </summary>
    public class NowChallengeNumInit : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TextMeshProUGUI>().text = $"{GameObject.Find("SaveData").GetComponent<SaveData>().GetSetSelectStageNum}";
        }
    }
}