using UnityEngine;

/// <summary>
/// “GAI‚ÌŒü‚¢‚Ä‚¢‚é•ûŒü‚ğŒˆ‚ß‚éˆ—
/// </summary>
namespace nsTankLab
{
    public class EnemyAILookDirection : MonoBehaviour
    {
        //‰ñ“]‘¬“x
        float m_rotationSpeed = 0.5f;

        SaveData m_saveData = null;

        void Start()
        {
            m_saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }

        void Update()
        {
            //ƒQ[ƒ€is‚ª~‚Ü‚Á‚Ä‚¢‚é‚Æ‚«‚ÍÀs‚µ‚È‚¢
            if (!m_saveData.GetSetmActiveGameTime)
            {
                return;
            }

            //‚½‚Ü‚É‰ñ“]‚·‚é•ûŒü‚ğ‹t‚É‚·‚é
            if (Random.Range(1, 250) == 1)
            {
                m_rotationSpeed *= -1f;
            }

            //Œü‚«‚ğ‰ñ“]‚³‚¹‚é
            transform.Rotate(0.0f, m_rotationSpeed, 0.0f);
        }
    }
}