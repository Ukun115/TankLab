using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideTank : MonoBehaviour
{
    public void SetCharacter(string character)
    {
        switch (character)
        {
            case "TANK1":
            case "TANK2":
                //�X�e�[�W�I���V�[���ɑJ��
                //SceneManager.LoadScene("SelectStageScene");
                GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectStageScene");
                break;
        }
    }
}