using UnityEngine;

public class ToastMessage : MonoBehaviour
{
    private string toastString;
    private string input;
    private AndroidJavaObject currentActivity;
    private AndroidJavaClass UnityPlayer;
    private AndroidJavaObject context;

    public static ToastMessage Instance;

    private void Awake()
    {
        Debug.Log("ToastMessage Awake");
        Instance = this;
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }
    }

    public void showToastOnUiThread(string toastString)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            this.toastString = toastString;
            currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
        }
    }

    private void showToast()
    {
        Debug.Log(this + ": Running on UI thread");

        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }
}