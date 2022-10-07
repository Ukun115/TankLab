using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecideTank : MonoBehaviour
{
    public void SetCharacter(string character)
    {
        //選択したタンクを保存しておく
        GameObject.Find("SaveData").GetComponent<SaveData>().GetSetSelectTankName = character;

        //ステージ選択シーンに遷移
        GameObject.Find("Transition").GetComponent<SceneSwitcher>().StartTransition("SelectStageScene");
    }
}