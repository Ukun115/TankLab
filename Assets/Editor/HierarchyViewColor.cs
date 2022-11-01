using UnityEditor;
using UnityEngine;

/// <summary>
/// Hierarchy�r���[���P�s�����ɐF�������Ď��F�����グ��G�f�B�^�g��
/// </summary>
//���̑��������邱�ƂŃG�f�B�^�N�����ɏ��������s����
[InitializeOnLoad]
internal static class HierarchyViewColor
{
    const int ROW_HEIGHT = 16;
    //�I�t�Z�b�g
    const int OFFSET_Y = -4;

    //�ύX������̃J���[
    static readonly Color COLOR = new Color(0, 0, 0, 0.2f);

    static HierarchyViewColor()
    {
        //�J���[�`�F���W����
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    //�J���[�`�F���W����
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