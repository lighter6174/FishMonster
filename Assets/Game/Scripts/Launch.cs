using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launch : MonoBehaviour
{
   // public Image progressBg;
   // public Image progressSp;

    private AsyncOperation operation;
    private float targetValue;
    public float loadingBarSpeed = 2;

    //private float progressLen = 600f;
    private float fillAmount;
    /*
    public float Progress
    {
        get { return fillAmount; }
        set
        {
            fillAmount = value;
            progressSp.fillAmount = fillAmount;
            //var size = progressSp.GetComponent<RectTransform>().sizeDelta;
            //size.x = progressLen * fillAmount;
            //progressSp.GetComponent<RectTransform>().sizeDelta = size;
        }
    }
    */
    private void Start()
    {
        //progressLen = progressBg.GetComponent<RectTransform>().sizeDelta.x;
       // Progress = 0f;
       // StartCoroutine(AsyncLoading());
        StartCoroutine(AsyncLoadModule());
        //StartCoroutine(LoadScene("ballz"));
    }

    private IEnumerator AsyncLoading()
    {
        string mainScene = "SnakeVsBlock";
        operation = SceneManager.LoadSceneAsync(mainScene);
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;

        yield return operation;
    }

    private IEnumerator AsyncLoadModule()
    {
        float interval = 0.3f;

        yield return new WaitForSeconds(interval);
       // Analysis.Instance.Init();

        yield return new WaitForSeconds(interval);
        Ads.Instance.Init();

        while (!Game.IsAllModuleInited)
        {
            yield return null;
        }
        Debug.Log("modules init finished !");
    }

    private IEnumerator LoadScene(string sceneName)
    {
        var asyncScene = SceneManager.LoadSceneAsync(sceneName);

        // this value stops the scene from displaying when it's finished loading
        asyncScene.allowSceneActivation = false;

        while (!asyncScene.isDone)
        {
            // loading bar progress
            float _loadingProgress = Mathf.Clamp01(asyncScene.progress / 0.9f) * 100;

            //progress.fillAmount = _loadingProgress;
            Debug.Log(_loadingProgress);

            // scene has loaded as much as possible, the last 10% can't be multi-threaded
            if (asyncScene.progress >= 0.9f)
            {
                // we finally show the scene
                asyncScene.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    /*
    private void Update()
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }

        if (targetValue != Progress)
        {
            //插值运算
            Progress = Mathf.Lerp(Progress, targetValue, Time.deltaTime * loadingBarSpeed);
            if (Mathf.Abs(Progress - targetValue) < 0.01f)
            {
                Progress = targetValue;
            }
        }

        //loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(Progress * 100) == 100)
        {
            if (Game.IsAllModuleInited)
            {
                //允许异步加载完毕后自动切换场景
                operation.allowSceneActivation = true;
                Debug.Log("======================================>>> allowSceneActivation = true");
            }
        }
    }
    */
/*
#if UNITY_EDITOR

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnGameLoaded()
    {
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            Destroy(go);
        }
        SceneManager.LoadScene(0);
    }

#endif*/
}