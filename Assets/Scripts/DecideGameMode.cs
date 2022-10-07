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
            case "CHALLENGE":
                //�`�������W�Q�[���ɑJ��
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("ChallengeGameScene");
                break;

            case "RANDOMMATCH":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("DecideNameScene");
                }
                //�ʏ�J��
                else
                {
                    //�^���N�V�[���ɑJ��
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectTankScene");
                }

                break;

            case "PRIVATEMATCH":
                //�v���C���[����o�^���Ă��Ȃ������ꍇ�A
                if (!PlayerPrefs.HasKey("PlayerName"))
                {
                    //�v���C���[���o�^�V�[��������
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("DecideNameScene");
                }
                //�ʏ�J��
                else
                {
                    //�p�X���[�h���͉�ʂɑJ��
                    GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("InputPasswordScene");
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
