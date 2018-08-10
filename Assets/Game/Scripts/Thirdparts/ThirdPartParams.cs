using System;
using System.Collections.Generic;

// TODO: 先写死，时间充裕了再改成可配置的
//[CreateAssetMenu(menuName = "Game/Create ThirdpartParams")]
public class ThirdpartParams
//: ScriptableObject
{
    [Serializable]
    public class Param
    {
        public string name;
        public string test;
        public string android;
        public string ios;
    }

    public List<Param> paramList;

    private readonly Dictionary<string, Param> paramDict = new Dictionary<string, Param>();

    private static ThirdpartParams _instance;

    public static ThirdpartParams Instance
    {
        get
        {
            //            if (!_instance)
            //                _instance = Resources.FindObjectsOfTypeAll<ThirdpartParams>().FirstOrDefault();
            //            //_instance = Instantiate(UnityEditor.AssetDatabase.LoadAssetAtPath<ThirdpartParams>("Resources/ThirdpartParams.asset"));
            //#if UNITY_EDITOR
            //            if (!_instance)
            //                InitializeFromDefault(UnityEditor.AssetDatabase.LoadAssetAtPath<ThirdpartParams>("Assets/test thirdpart params.asset"));
            //#endif
            if (_instance == null)
                _instance = new ThirdpartParams();
            return _instance;
        }
    }

    //public static void InitializeFromDefault(ThirdpartParams thirdpartParams)
    //{
    //    if (_instance) DestroyImmediate(_instance);
    //    _instance = Instantiate(thirdpartParams);
    //    _instance.hideFlags = HideFlags.HideAndDontSave;
    //}

    //private void OnEnable()
    //{
    //    paramDict.Clear();
    //    foreach (var param in paramList)
    //    {
    //        if (paramDict.ContainsKey(param.name))
    //        {
    //            Debug.Log("已经存在同类 UIViewAttribute: " + param.name);
    //            throw new Exception("已经存在同类 UIViewAttribute: " + param.name);
    //        }
    //        paramDict.Add(param.name, param);
    //    }
    //}

    public ThirdpartParams()
    {
        // 先凑合着
        paramDict.Clear();
        paramDict.Add("admob_appid", new Param
        {
            name = "admob_appid",
            test = "ca-app-pub-3940256099942544~3347511713",
            android = "ca-app-pub-1502322555229195~8897190358",
            ios = "",
        });
        paramDict.Add("admob_unitid_banner", new Param
        {
            name = "admob_unitid_banner",
            test = "ca-app-pub-3940256099942544/6300978111",
            android = "ca-app-pub-1502322555229195/6719417877",
            ios = "",
        });
        paramDict.Add("admob_unitid_interstitial", new Param
        {
            name = "admob_unitid_interstitial",
            test = "ca-app-pub-3940256099942544/1033173712",
            android = "ca-app-pub-1502322555229195/5387112762",
            ios = "",
        });
        paramDict.Add("admob_unitid_rewarded_video", new Param
        {
            name = "admob_unitid_rewarded_video",
            test = "ca-app-pub-3940256099942544/5224354917",
            android = "ca-app-pub-1502322555229195/3307744331",
            ios = "",
        });
    }

    public string Get(string name, bool isDebug = false)
    {
        Param param;
        if (!paramDict.TryGetValue(name, out param))
            return "";
        if (isDebug)
            return param.test;
#if UNITY_ANDROID
        return param.android;
#elif UNITY_IPHONE
        return param.ios;
#else
        return param.test;
#endif
    }

#if UNITY_EDITOR

    //[UnityEditor.MenuItem("Tools/ThirdpartParams")]
    //public static void ShowThirdpartParams()
    //{
    //    UnityEditor.Selection.activeObject = Instance;
    //}

#endif
}