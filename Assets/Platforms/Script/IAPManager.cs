﻿//##### Create by onlygatz@naver.com #####
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
////using UnityEngine.Purchasing;

////iap
//public class IAPManager : MonoBehaviour, IStoreListener
//{
//    static public IAPManager Instance = null;
//    public bool isReady = false;
    
//    private static IStoreController m_StoreController;          // The Unity Purchasing system.
//    private static IExtensionProvider m_StoreExtensionProvider;

//    //public static string kProductIDConsumable = "consumable";
//    //public static string kProductIDNonConsumable = "nonconsumable";
//    //public static string kProductIDSubscription = "subscription";

//    // Apple App Store-specific product identifier for the subscription product.
//    //private static string kProductNameAppleSubscription = "m.ntonsGP.ds";

//    // Google Play Store-specific product identifier subscription product.
//    //private static string kProductNameGooglePlaySubscription = "m.ntonsGP.ds";

//    private List<ProductDefinition> productList = null;    

//    public delegate void CallBack();
//    private CallBack callBack = null;

//    private string productID = string.Empty;
//    private string purchaseMsg = string.Empty;
//    private string msg = string.Empty;

//    void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        // If we haven't set up the Unity Purchasing reference
//        if (m_StoreController == null)
//        {
//            // Begin to configure our connection to Purchasing
//            InitializePurchasing();
//        }
//    }

//    void SetProductList()
//    {
//        if(productList == null) productList = new List<ProductDefinition>();

//        string shopID = "SH0000";
//        ProductDefinition pd = new ProductDefinition(shopID.ToLower(), ProductType.Consumable);
//        //Debug.unityLogger.Log("SetProductList : " + pd.id);
//        productList.Add(pd);
//    }

//    public void InitializePurchasing()
//    {
//        // If we have already connected to Purchasing ...
//        if (IsInitialized())
//        {
//            // ... we are done here.
//            return;
//        }

//        // Create a builder, first passing in a suite of Unity provided stores.
//        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//        // Add a product to sell / restore by way of its identifier, associating the general identifier
//        // with its store-specific identifiers.
//        //builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
//        // Continue adding the non-consumable product.
//        //builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
//        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
//        // if the Product ID was configured differently between Apple and Google stores. Also note that
//        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
//        // must only be referenced here. 
//        //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
//        //        { kProductNameAppleSubscription, AppleAppStore.Name },
//        //        { kProductNameGooglePlaySubscription, GooglePlay.Name },
//        //    });

//        this.SetProductList();
//        builder.AddProducts(productList);

//        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
//        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
//        UnityPurchasing.Initialize(this, builder);       
//        isReady = true;
//    }

//    private bool IsInitialized()
//    {
//        // Only say we are initialized if both the Purchasing references are set.
//        return m_StoreController != null && m_StoreExtensionProvider != null;
//    }

//    public void BuyConsumable(string shopID, CallBack cb = null)
//    {
//        // Buy the consumable product using its general identifier. Expect a response either 
//        // through ProcessPurchase or OnPurchaseFailed asynchronously.
//        //BuyProductID(kProductIDConsumable);

//        productID = shopID.ToLower();
//        callBack = cb;
//        this.BuyProductID();
//    }

//    public void BuyNonConsumable()
//    {
//        // Buy the non-consumable product using its general identifier. Expect a response either 
//        // through ProcessPurchase or OnPurchaseFailed asynchronously.
//        //BuyProductID(kProductIDNonConsumable);
//    }

//    public void BuySubscription()
//    {
//        // Buy the subscription product using its the general identifier. Expect a response either 
//        // through ProcessPurchase or OnPurchaseFailed asynchronously.
//        // Notice how we use the general product identifier in spite of this ID being mapped to
//        // custom store-specific identifiers above.
//        //BuyProductID(kProductIDSubscription);
//    }

//    void BuyProductID()
//    {
//        Debug.unityLogger.Log("BuyProductID : " + productID + " / " + callBack.Method.Name);

//        if(!isReady) return;
        
//        purchaseMsg = string.Empty;

//        // If Purchasing has been initialized ...
//        if (IsInitialized())
//        {
//            // ... look up the Product reference with the general product identifier and the Purchasing 
//            // system's products collection.
//            //Product product = m_StoreController.products.WithID(productId);
//            Product product = m_StoreController.products.WithID(productID);

//            // If the look up found a product for this device's store and that product is ready to be sold ... 
//            if (product != null && product.availableToPurchase)
//            {
//                Debug.unityLogger.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
//                // asynchronously.
//                m_StoreController.InitiatePurchase(product);
//                purchaseMsg = "SUCCESS";
//                msg = "Buy Success";
//            }
//            // Otherwise ...
//            else
//            {
//                // ... report the product look-up failure situation  
//                Debug.unityLogger.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//                purchaseMsg = "FAIL";
//                msg = "Buy Failed";
//            }
//        }
//        // Otherwise ...
//        else
//        {
//            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
//            // retrying initiailization.
//            Debug.unityLogger.Log("BuyProductID FAIL. Not initialized.");
//            purchaseMsg = "FAIL";
//            msg = "Buy Failed";
//        }

//        Debug.unityLogger.Log(purchaseMsg);
//    }

//    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
//    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
//    public void RestorePurchases()
//    {
//        // If Purchasing has not yet been set up ...
//        if (!IsInitialized())
//        {
//            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
//            Debug.unityLogger.Log("RestorePurchases FAIL. Not initialized.");
//            return;
//        }

//        // If we are running on an Apple device ... 
//        if (Application.platform == RuntimePlatform.IPhonePlayer ||
//            Application.platform == RuntimePlatform.OSXPlayer)
//        {
//            // ... begin restoring purchases
//            Debug.unityLogger.Log("RestorePurchases started ...");

//            // Fetch the Apple store-specific subsystem.
//            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
//            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
//            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
//            apple.RestoreTransactions((result) => {
//                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
//                // no purchases are available to be restored.
//                Debug.unityLogger.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//            });
//        }
//        // Otherwise ...
//        else
//        {
//            // We are not running on an Apple device. No work is necessary to restore purchases.
//            Debug.unityLogger.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//        }
//    }

//    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//    {
//        // Purchasing has succeeded initializing. Collect our Purchasing references.
//        Debug.unityLogger.Log("OnInitialized: PASS");

//        // Overall Purchasing system, configured with products for this application.
//        m_StoreController = controller;
//        // Store specific subsystem, for accessing device-specific store features.
//        m_StoreExtensionProvider = extensions;
//    }

//    public void OnInitializeFailed(InitializationFailureReason error)
//    {
//        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
//        Debug.unityLogger.Log("OnInitializeFailed InitializationFailureReason:" + error);
//    }

//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//    {
//        Debug.unityLogger.Log(purchaseMsg);

//#if UNITY_EDITOR
//        // A consumable product has been purchased by this user.
//        if (String.Equals(args.purchasedProduct.definition.id, productID, StringComparison.Ordinal))
//        {
//            Debug.unityLogger.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//            // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
//            //ScoreManager.score += 100;

//            Debug.unityLogger.Log(callBack);
//            if (callBack != null)
//            {
//                Debug.unityLogger.Log(callBack.Method.Name);
//                callBack.Invoke();
//            }
//        }
//        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
//        else
//        {
//            Debug.unityLogger.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//            productID = string.Empty;
//            purchaseMsg = string.Empty;
//            callBack = null;
//        }

//        Debug.unityLogger.Log(purchaseMsg);
//#else
//        //server check
//        //if (ServerManager.Instance.CheckValidationPurchase(args.purchasedProduct.receipt))
//        //{
//            // A consumable product has been purchased by this user.
//            if (String.Equals(args.purchasedProduct.definition.id, productID, StringComparison.Ordinal))
//            {
//                Debug.unityLogger.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
//                //ScoreManager.score += 100;

//                Debug.unityLogger.Log(callBack);
//                if (callBack != null)
//                {
//                    Debug.unityLogger.Log(callBack.Method.Name);
//                    callBack.Invoke();
//                }
//            }
//        //}
//        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
//        else
//        {
//            Debug.unityLogger.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//            productID = string.Empty;
//            purchaseMsg = string.Empty;
//            callBack = null;
//        }

//        Debug.unityLogger.Log(purchaseMsg);
//#endif

//        // Return a flag indicating whether this product has completely been received, or if the application needs 
//        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
//        // saving purchased products to the cloud, and when that save is delayed. 
//        return PurchaseProcessingResult.Complete;
//    }

//    //public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//    //{
//    //    // A consumable product has been purchased by this user.
//    //    if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
//    //    {
//    //        Debug.unityLogger.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//    //        // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
//    //        //ScoreManager.score += 100;
//    //    }
//    //    // Or ... a non-consumable product has been purchased by this user.
//    //    else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
//    //    {
//    //        Debug.unityLogger.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//    //        // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
//    //    }
//    //    // Or ... a subscription product has been purchased by this user.
//    //    else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
//    //    {
//    //        Debug.unityLogger.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//    //        // TODO: The subscription item has been successfully purchased, grant this to the player.
//    //    }
//    //    // Or ... an unknown product has been purchased by this user. Fill in additional products here....
//    //    else
//    //    {
//    //        Debug.unityLogger.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//    //    }

//    //    // Return a flag indicating whether this product has completely been received, or if the application needs 
//    //    // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
//    //    // saving purchased products to the cloud, and when that save is delayed. 
//    //    return PurchaseProcessingResult.Complete;
//    //}


//    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//    {
//        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
//        // this reason with the user to guide their troubleshooting actions.
//        Debug.unityLogger.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//    }

//    public string GetPurchaseMsg()
//    {
//        return purchaseMsg;
//    }
//}