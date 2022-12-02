using UnityEngine;
using UnityEngine.UI;

namespace nsTankLab
{
    /// <summary>
    /// ハートUIの表示更新処理
    /// </summary>
    public class HeartSpriteDisplayUpdate : MonoBehaviour
    {
        [SerializeField] Image[] m_heartImage = { null };
        [SerializeField] Sprite m_heartAvailableSprite = null;
        [SerializeField] Sprite m_heartNotAvailableSprite = null;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //ハートUI表示更新
            for (int heartNum = 0; heartNum < 3; heartNum++)
            {
                //まずとりあえず体力ありハート画像に変更
                m_heartImage[heartNum].sprite = m_heartAvailableSprite;
                //体力の減りにしたがって体力なしハート画像に変更
                if (heartNum >= m_saveData.GetSetHitPoint)
                {
                    m_heartImage[heartNum].sprite = m_heartNotAvailableSprite;
                }
            }
        }
    }
}