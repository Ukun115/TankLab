using UnityEngine;
using UnityEngine.SceneManagement;

namespace nsTankLab
{
    public class SelectTankUiDelete : MonoBehaviour
    {
        [SerializeField]GameObject[] m_gameObjects = null;
        [SerializeField]RayCast[] m_rayCast = null;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

            if (SceneManager.GetActiveScene().name == SceneName.SelectTankScene)
            {
                if (m_saveData.GetSetSelectGameMode != "LOCALMATCH")
                {
                    Delete();
                }
            }
        }

        void Delete()
        {
            for(int arrayNum = 0; arrayNum < m_gameObjects.Length;arrayNum++)
            {
                Destroy(m_gameObjects[arrayNum]);
            }
            for (int arrayNum = 0; arrayNum < m_rayCast.Length; arrayNum++)
            {
                Destroy(m_rayCast[arrayNum]);
            }
        }
    }
}