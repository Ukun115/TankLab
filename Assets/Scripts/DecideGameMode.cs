using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideGameMode : MonoBehaviour
{
    public void SetCharacter(string character)
    {
        switch (character)
        {
            case "RANDOMMATCH":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    SceneManager.LoadScene("DecideNameScene");
                }
                //�ʏ�J��
                else
                {
                    //�^���N�V�[���ɑJ��
                    SceneManager.LoadScene("SelectTankScene");
                }

                break;

            case "PRIVATEMATCH":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    SceneManager.LoadScene("DecideNameScene");
                }
                //�ʏ�J��
                else
                {

                }

                break;

            case "EXIT":
                //�Q�[���I��
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif

                break;
        }
    }
}
