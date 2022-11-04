using UnityEngine;

//“GAI‚ÌŒü‚¢‚Ä‚¢‚é•ûŒü‚ğŒˆ‚ß‚éˆ—
public class EnemyAILookDirection : MonoBehaviour
{
    //‰ñ“]‘¬“x
    float m_rotationSpeed = 0.5f;

    void Update()
    {
        //‚½‚Ü‚É‰ñ“]‚·‚é•ûŒü‚ğ‹t‚É‚·‚é
        if (Random.Range(1, 250) == 1)
        {
            m_rotationSpeed *= -1f;
        }

        //Œü‚«‚ğ‰ñ“]‚³‚¹‚é
        this.transform.Rotate(0.0f, m_rotationSpeed, 0.0f);
    }
}
