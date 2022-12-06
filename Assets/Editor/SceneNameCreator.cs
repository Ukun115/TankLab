using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �V�[������萔�ŊǗ�����N���X���쐬����X�N���v�g
/// </summary>
public static class SceneNameCreator
{
    // �����ȕ������Ǘ�����z��
    static readonly string[] INVALUD_CHARS =
    {
        " ", "!", "\"", "#", "$",
        "%", "&", "\'", "(", ")",
        "-", "=", "^",  "~", "\\",
        "|", "[", "{",  "@", "`",
        "]", "}", ":",  "*", ";",
        "+", "/", "?",  ".", ">",
        ",", "<"
    };

    const string ITEM_NAME = "Tools/Create/Scene Name";    // �R�}���h��
    const string PATH = "Assets/SceneName.cs";        // �t�@�C���p�X

    static readonly string FILENAME = Path.GetFileName(PATH);                   // �t�@�C����(�g���q����)
    static readonly string FILENAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(PATH);   // �t�@�C����(�g���q�Ȃ�)

    /// <summary>
    /// �V�[������萔�ŊǗ�����N���X���쐬���܂�
    /// </summary>
    [MenuItem(ITEM_NAME)]
    public static void Create()
    {
        //�V�[������萔�ŊǗ�����N���X���쐬�ł��Ȃ��Ƃ��͎��s���Ȃ�
        if (!CanCreate())
        {
            return;
        }

        //�X�N���v�g�쐬
        CreateScript();

        EditorUtility.DisplayDialog(FILENAME, "�쐬���������܂���", "OK");
    }

    /// <summary>
    /// �X�N���v�g���쐬���܂�
    /// </summary>
    public static void CreateScript()
    {
        var builder = new StringBuilder();

        builder.AppendLine("/// <summary>");
        builder.AppendLine("/// �V�[������萔�ŊǗ�����N���X");
        builder.AppendLine("/// </summary>");
        builder.AppendFormat("public static class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
        builder.AppendLine("{");

        foreach (var n in EditorBuildSettings.scenes
            .Select(c => Path.GetFileNameWithoutExtension(c.path))
            .Distinct()
            .Select(c => new { var = RemoveInvalidChars(c), val = c }))
        {
            builder.Append("\t").AppendFormat(@"public const string {0} = ""{1}"";", n.var, n.val).AppendLine();
        }

        builder.AppendLine("}");

        var directoryName = Path.GetDirectoryName(PATH);
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        File.WriteAllText(PATH, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }

    /// <summary>
    /// �V�[������萔�ŊǗ�����N���X���쐬�ł��邩�ǂ������擾���܂�
    /// </summary>
    [MenuItem(ITEM_NAME, true)]
    public static bool CanCreate()
    {
        return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
    }

    /// <summary>
    /// �����ȕ������폜���܂�
    /// </summary>
    public static string RemoveInvalidChars(string str)
    {
        Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
        return str;
    }
}