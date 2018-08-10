using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private readonly Stack<Menu> menuStack = new Stack<Menu>();

    public MainMenu MainMenuPrefab;
    public OptionsMenu OptionsMenuPrefab;

    public MainScreen MainScreenPrefab;
    public GameScreen GameScreenPrefab;
    public GameOverScreen GameOverScreenPrefab;
    public ReliveScreen ReliveScreenPrefab;
    public ShopScreen ShopScreenPrefab;

    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("MenuManager Awake");
        Instance = this;
    }

    private void Start()
    {
        // TODO: 用一个字典代替GetPrefab函数
    }

    public void OpenMenu<T>() where T : Menu
    {
        var prefab = GetPrefab<T>();

        var instance = Instantiate(prefab, transform);

        if (menuStack.Count > 0)
        {
            menuStack.Peek().gameObject.SetActive(false);
        }
        menuStack.Push(instance.GetComponent<Menu>());
    }

    private T GetPrefab<T>() where T : Menu
    {
        if (typeof(T) == typeof(MainMenu))
        {
            return MainMenuPrefab as T;
        }
        if (typeof(T) == typeof(OptionsMenu))
        {
            return OptionsMenuPrefab as T;
        }
        if (typeof(T) == typeof(MainScreen))
        {
            return MainScreenPrefab as T;
        }
        if (typeof(T) == typeof(GameScreen))
        {
            return GameScreenPrefab as T;
        }
        if (typeof(T) == typeof(GameOverScreen))
        {
            return GameOverScreenPrefab as T;
        }
        if (typeof(T) == typeof(ReliveScreen))
        {
            return ReliveScreenPrefab as T;
        }
        if (typeof(T) == typeof(ShopScreen))
        {
            return ShopScreenPrefab as T;
        }
        throw new MissingReferenceException();
    }

    public void CloseMenu()
    {
        var instance = menuStack.Pop();
        Destroy(instance.gameObject);

        if (menuStack.Count > 0)
        {
            menuStack.Peek().gameObject.SetActive(true);
        }
    }

    public void CloseMenu<T>(T t)
    {
        var instance = menuStack.Pop();
        while (true)
        {
            if (instance == null)
                break;
            if (t.Equals(instance))
            {
                Destroy(instance.gameObject);
                break;
            }
            Destroy(instance.gameObject);
            instance = menuStack.Pop();
        }

        if (menuStack.Count > 0)
        {
            menuStack.Peek().gameObject.SetActive(true);
        }
    }
}

public abstract class Menu<T> : Menu where T : Menu<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = (T)this;
    }

    protected virtual void OnDestory()
    {
        Instance = null;
    }

    protected static void Open()
    {
        if (Instance != null)
        {
            return;
        }
        MenuManager.Instance.OpenMenu<T>();
    }

    protected static void Close()
    {
        if (Instance == null)
        {
            return;
        }
        MenuManager.Instance.CloseMenu(Instance);
    }
}

public abstract class Menu : MonoBehaviour
{
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackPressed();
        }
    }

    public abstract void OnBackPressed();
}

public abstract class SimpleMenu<T> : Menu<T> where T : SimpleMenu<T>
{
    public static void Show()
    {
        Open();
    }

    public static void Hide()
    {
        Close();
    }
}

public abstract class ComplexMenu : Menu<ComplexMenu>
{
    public static void Show(string foo)
    {
        Open();
    }

    public static void Hide(int result)
    {
        Close();
    }
}

public class MainMenu : Menu<MainMenu>
{
    public override void OnBackPressed()
    {
        Application.Quit();
    }
}

public class OptionsMenu : Menu<OptionsMenu>
{
    public override void OnBackPressed()
    {
        MenuManager.Instance.CloseMenu();
    }
}