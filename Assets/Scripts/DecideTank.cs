using UnityEngine;

/// <summary>
/// �^���N�����肷�鏈��
/// </summary>
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