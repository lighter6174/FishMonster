using UnityEngine;

//[CreateAssetMenu()]
public class GameConfig : ScriptableObject
{
    [Header("--- 基本配置 ---")]
    public int VideoCoinChancePerDay = 50;

    [Header("--- Tipbox ---")]
    public GameObject TipboxPrefab;

    public float TipboxShowTime = 1f;
    public float TipboxFadeTime = 1.5f;

    private void OnEnable()
    {
    }

    public static GameConfig Load()
    {
        GameConfig cfg = Resources.Load<GameConfig>("GameConfig");
        return cfg;
    }

#if UNITY_EDITOR

    [UnityEditor.MenuItem("Tools/游戏配置")]
    public static void ShowGameConfig()
    {
        UnityEditor.Selection.activeObject = Game.Config;
    }

#endif
}