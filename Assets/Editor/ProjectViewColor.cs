using UnityEditor;
using UnityEngine;

/// <summary>
/// Projectビューを１行おきに色分けして視認性を上げるエディタ拡張
/// </summary>
public static class ProjectViewColor
{
    //この属性をつけることで、エディタ起動時にこの関数を実行する。
    [InitializeOnLoadMethod]
    static void ColorChange()
    {
        //カラーチェンジ処理
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    //カラーチェンジ処理
    static void OnGUI(string guid, Rect selectionRect)
    {
        var index = (int)(selectionRect.y - 4) / 16;

        if (index % 2 == 0)
        {
            return;
        }

        var pos = selectionRect;
        pos.x = 0;
        pos.xMax = selectionRect.xMax;

        var color = GUI.color;
        GUI.color = new Color(0, 0, 0, 0.5f);
        GUI.Box(pos, string.Empty);
        GUI.color = color;
    }
}