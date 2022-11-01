using UnityEditor;
using UnityEngine;

/// <summary>
/// Project�r���[���P�s�����ɐF�������Ď��F�����グ��G�f�B�^�g��
/// </summary>
public static class ProjectViewColor
{
    //���̑��������邱�ƂŁA�G�f�B�^�N�����ɂ��̊֐������s����B
    [InitializeOnLoadMethod]
    static void ColorChange()
    {
        //�J���[�`�F���W����
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    //�J���[�`�F���W����
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