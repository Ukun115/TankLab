using System.Linq;
using UnityEngine;

/// <summary>
/// 接続されているコントローラーのデータを保存している場所
/// </summary>
namespace nsTankLab
{
    public class ControllerData : MonoBehaviour
    {
        string[] controllerNames = {string.Empty};

        void Start()
    {
                SearchConnectedController();

                Debug.Log($"<color=yellow>接続されているコントローラー数:{controllerNames.Length}</color>");
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
        if (controllerNames.Length > playerNum)
        {
            if (controllerNames[playerNum - 1] is not null)
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
}