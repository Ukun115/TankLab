using UnityEditor;
using UnityEngine;

/// <summary>
/// Hierarchyビューを１行おきに色分けして視認性を上げるエディタ拡張
/// </summary>
//この属性をつけることでエディタ起動時に処理を実行する
[InitializeOnLoad]
internal static class HierarchyViewColor
{
    const int ROW_HEIGHT = 16;
    //オフセット
    const int OFFSET_Y = -4;

    //変更した後のカラー
    static readonly Color COLOR = new Color(0, 0, 0, 0.2f);

    static HierarchyViewColor()
    {
        //カラーチェンジ処理
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    //カラーチェンジ処理
    static void OnGUI(int instanceID, Rect rect)
    {
        var index = (int)(rect.y + OFFSET_Y) / ROW_HEIGHT;

        if (index % 2 == 0)
        {
            return;
        }

        var xMax = rect.xMax;

        rect.x = 32;
        rect.xMax = xMax + 16;

        EditorGUI.DrawRect(rect, COLOR);
    }
}