#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class SetFontFast : EditorWindow
{
    public Font newFont; // Font cho các thành phần Text
    public TMP_FontAsset newTMPFont; // Font cho các thành phần TextMeshPro

    [MenuItem("Tools/Font Changer")]
    public static void ShowWindow()
    {
        GetWindow<SetFontFast>("Font Changer");
    }

    void OnGUI()
    {
        GUILayout.Label("Change All Fonts", EditorStyles.boldLabel);
        newFont = (Font)EditorGUILayout.ObjectField("New Font", newFont, typeof(Font), false);
        newTMPFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New TMP Font", newTMPFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Change Fonts in Scene"))
        {
            ChangeFontsInScene();
        }

        if (GUILayout.Button("Change Fonts in Project"))
        {
            ChangeFontsInProject();
        }
    }

    void ChangeFontsInScene()
    {
        // Thay đổi phông chữ cho tất cả các thành phần Text trong Scene
        Text[] allTextComponents = FindObjectsOfType<Text>();
        foreach (Text text in allTextComponents)
        {
            text.font = newFont;
            EditorUtility.SetDirty(text);
        }

        // Thay đổi phông chữ cho tất cả các thành phần TextMeshPro trong Scene
        TMP_Text[] allTMPTextComponents = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text tmpText in allTMPTextComponents)
        {
            tmpText.font = newTMPFont;
            EditorUtility.SetDirty(tmpText);
        }

        AssetDatabase.SaveAssets();
    }

    void ChangeFontsInProject()
    {
        // Thay đổi phông chữ cho tất cả các Prefab trong dự án
        string[] allPrefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in allPrefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                ChangeFontInGameObject(prefab);
                PrefabUtility.SavePrefabAsset(prefab);
            }
        }

        // Thay đổi phông chữ cho tất cả các Scene trong dự án
        string[] allSceneGuids = AssetDatabase.FindAssets("t:Scene");
        foreach (string guid in allSceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            EditorSceneManager.OpenScene(path);
            ChangeFontsInScene();
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
    }

    void ChangeFontInGameObject(GameObject go)
    {
        // Thay đổi phông chữ cho tất cả các thành phần Text trong GameObject
        Text[] textComponents = go.GetComponentsInChildren<Text>(true);
        foreach (Text text in textComponents)
        {
            text.font = newFont;
            EditorUtility.SetDirty(text);
        }

        // Thay đổi phông chữ cho tất cả các thành phần TextMeshPro trong GameObject
        TMP_Text[] tmpTextComponents = go.GetComponentsInChildren<TMP_Text>(true);
        foreach (TMP_Text tmpText in tmpTextComponents)
        {
            tmpText.font = newTMPFont;
            EditorUtility.SetDirty(tmpText);
        }
    }
}
#endif
