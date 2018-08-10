using UnityEditor;
using UnityEngine;

//[CreateAssetMenu()]
public class EditorConfig : ScriptableObject
{
    public Texture2D arrowUp;
    public Texture2D arrowDown;

    private static EditorConfig instance;

    public static EditorConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Load();
            }
            return instance;
        }
    }

    public static EditorConfig Load()
    {
        EditorConfig cfg = EditorGUIUtility.Load("EditorConfig.asset") as EditorConfig;
        return cfg;
    }
}