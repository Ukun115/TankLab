using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideTank : MonoBehaviour
{
    public void SetCharacter(string character)
    {
        //�I�������^���N��ۑ����Ă���
        GameObject.Find("SaveData").GetComponent<SaveData>().GetSetSelectTankName = character;

        //�X�e�[�W�I���V�[���ɑJ��
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectStageScene");
    }
}