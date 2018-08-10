using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomButton))]
[CanEditMultipleObjects]
public class CustomButtonEditor : Editor
{
    private static GUISkin skin;
    private Dictionary<string, string> titleDict = new Dictionary<string, string>();
    private Dictionary<string, bool> showDict = new Dictionary<string, bool>();
    private CustomButton lastTarget;

    public override void OnInspectorGUI()
    {
        if (skin == null)
        {
            skin = EditorGUIUtility.Load("skin.guiskin") as GUISkin;
        }

        CustomButton customButton = (CustomButton)target;
        if (lastTarget != customButton)
        {
            lastTarget = customButton;
            // 频繁的反射，肯定会影响性能，需要做缓存
            CacheEditorInfo(customButton);

            Debug.Log("[CustomButtonEditor] CacheEditorInfo");
        }

        DrawCustomEditor(serializedObject, customButton);

        //targetMenuButton.acceptsPointerInput = EditorGUILayout.Toggle("Accepts pointer input", targetMenuButton.acceptsPointerInput);

        //DrawDefaultInspector();
        //GUILayout.BeginVertical("CustomButtonEditor", "window");
        //base.OnInspectorGUI();
        //GUILayout.EndVertical();

        // 编辑了数据后，需要存盘
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void DrawCustomEditor(SerializedObject obj, CustomButton customButton)
    {
        EditorGUI.BeginChangeCheck();
        obj.Update();

        // Loop through properties and create one field (including children) for each top level property.
        SerializedProperty property = obj.GetIterator();
        bool expanded = true;
        while (property.NextVisible(expanded))
        {
            using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
            {
                DrawTitle(property, customButton);
                // TODO: 通过ShowIfAttribute 和 HideIfAttribute 控制字段的显示

                if (property.name == "eventTypes" && false)
                {
                    // TODO:
                }
                else
                {
                    EditorGUILayout.PropertyField(property, true);
                }
            }
            expanded = false;
        }

        obj.ApplyModifiedProperties();
        EditorGUI.EndChangeCheck();
    }

    private void DrawTitle(SerializedProperty property, CustomButton customButton)
    {
        string title = null;

        if (titleDict.TryGetValue(property.name, out title))
        {
            EditorGUILayout.LabelField(title, skin.label);
        }
    }

    private void CacheEditorInfo(CustomButton customButton)
    {
        titleDict.Clear();
        showDict.Clear();

        SerializedProperty property = serializedObject.GetIterator();
        bool expanded = true;
        while (property.NextVisible(expanded))
        {
            using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
            {
                var field = customButton.GetType().GetField(property.name);
                if (field != null)
                {
                    object[] attrs = field.GetCustomAttributes(typeof(ETitleAttribute), false);
                    if (attrs.Length > 0)
                    {
                        string title = ((ETitleAttribute)attrs[0]).Title;
                        titleDict.Add(property.name, title);
                    }
                }

                // TODO: 添加 ShowIfAttribute 和 HideIfAttribute 的缓存
            }
            expanded = false;
        }
    }
}

public class CustomButtonEditorUtility
{
    [MenuItem("GameObject/UI/CustomButton")]
    public static void AddCustomButton()
    {
        GameObject o = new GameObject();
        o.AddComponent<CustomButton>();
        o.name = "CustomButton";
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponentInParent<Canvas>() != null)
        {
            o.transform.SetParent(Selection.activeGameObject.transform, false);
            Selection.activeGameObject = o;
        }
        else if (Selection.activeGameObject != null)
        {
            //selected GameObject is not child of canvas:
        }
        else
        {
            if (GameObject.FindObjectOfType<Canvas>() == null)
            {
                EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas");
            }
            Canvas c = GameObject.FindObjectOfType<Canvas>();
            o.transform.SetParent(c.transform, false);
            Selection.activeGameObject = o;
        }
    }
}