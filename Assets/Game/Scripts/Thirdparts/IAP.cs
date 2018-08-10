using UnityEngine;
using UnityEngine.Purchasing;

public class IAP : MonoBehaviour, IStoreListener
{
    private IStoreController controller = null;
    private IExtensionProvider extensions = null;
    public static IAP Instance;

    //初始化
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Billing failed to initialize!");
        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
                break;

            case InitializationFailureReason.PurchasingUnavailable:
                Debug.Log("Billing disabled!");
                break;

            case InitializationFailureReason.NoProductsAvailable:
                Debug.Log("No products available for purchase!");
                break;
        }
    }

    //购买失败通知回调
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Purchase failed: " + i.definition.id);
        Debug.Log(p);
    }

    //购买成功通知回调
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        Debug.Log("Purchase OK: " + e.purchasedProduct.definition.id);
        Debug.Log("Receipt: " + e.purchasedProduct.receipt);

        updateForTransactionWithIdentifier(e.purchasedProduct.definition.id);
        return PurchaseProcessingResult.Complete;
    }

    //通过消息发送的方式，可以多游戏逻辑通用
    private void updateForTransactionWithIdentifier(string productId)
    {
        RechargeEventData data = new RechargeEventData();
        data.identifier = productId;
        Game.EventManager.Send(EventName.PurchaseOk, data);
    }

    //发起购买
    public void Purchase(string productID)
    {
        Debug.Log("Purchase:" + productID);
        if (controller != null)
        {
            Product p = controller.products.WithID(productID);
            if (p != null && p.availableToPurchase)
            {
                controller.InitiatePurchase(p);
                Debug.Log("InitiatePurchase:" + productID);
            }
        }
        else
        {
            Debug.Log("this.controller null");
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        var module = StandardPurchasingModule.Instance();
        var builder = ConfigurationBuilder.Instance(module);
        Game.Shop.AddProduct(builder);    //添加游戏计费点信息
        UnityPurchasing.Initialize(this, builder);
        Debug.Log("IAP start");
    }
}