using System.Linq;
using UnityEngine;

public class ControllerData : MonoBehaviour
{
    string[] controllerNames = { "" };

    void Start()
    {
        SearchConnectedController();

        Debug.Log($"接続されているコントローラー数は{controllerNames.Length}つです。");
    }

    void Update()
    {
        SearchConnectedController();
    }

    void SearchConnectedController()
    {
        // 接続されているコントローラーの名前を調べる
        controllerNames = Input.GetJoystickNames();

        //一旦リストに変換
        var list = controllerNames.ToList();

        //空白の要素は排除しておく。
        //どうやらコントローラーを抜き差ししていたら空白の要素が出てきてしまうバグがあるみたい...
        list.RemoveAll(item => item == "");

        //配列に変換
        controllerNames = list.ToArray();
    }

    //プレイヤ―番号と一致するコントローラーが接続されているかどうか
    public bool GetIsConnectedController(int playerNum)
    {
        if (controllerNames.Length >= playerNum)
        {
            if (controllerNames[playerNum - 1] != null)
            {
                return true;
            }
        }
        return false;
    }

    //接続されているコントローラー数
    public int GetConnectedControllerNum()
    {
        return controllerNames.Length;
    }
}