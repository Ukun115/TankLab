using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordTextDisplay : MonoBehaviour
{

    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = "PassWord:" + GameObject.Find("SaveData").GetComponent<SaveData>().GetSetInputPassword;
    }
}
