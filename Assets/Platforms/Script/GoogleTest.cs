using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GoogleTest : MonoBehaviour
{
    private bool isReady = false;
    private string msg = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        return;
#endif
        //init
        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).RequestEmail().RequestIdToken().Build();
            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();

            msg = "Google Init Success";            
            isReady = true;
        }
        catch (System.Exception e)
        {
            msg = "Google Init Failed : " + e;
            Debug.unityLogger.Log(msg);            
        }
    }

    private void Update()
    {
        if(!string.IsNullOrEmpty(msg))
        {
            PlatformManager.Instance.SetState(msg, true);
            msg = string.Empty;
        }
    }

    public string GetGooleAuthToken()
    {
        if (!isReady) return null;

        string token = string.Empty;

        if (Social.localUser.authenticated)
        {
            token = ((PlayGamesPlatform)Social.Active).GetIdToken();
        }

        msg = "GetGooleAuthToken : " + token;
        Debug.unityLogger.Log(msg);        
        return token;
    }

    #region OnClick
    public void OnClickLogin()
    {
        Debug.unityLogger.Log("OnClickLogin");

        if (!isReady) return;

        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool isSuccess) =>
            {
                if (isSuccess)
                {
                    //msg = string.Format("Google Login Success\n{0}\n{1}\n{2}\n{3}\n{4}",
                    //    ((PlayGamesPlatform)Social.localUser).GetIdToken(),
                    //    ((PlayGamesPlatform)Social.localUser).GetUserEmail(),
                    //    Social.localUser.id,
                    //    Social.localUser.userName,
                    //    ((PlayGamesPlatform)Social.localUser).GetUserDisplayName()
                    //    );

                    msg = "Google Login Success";
                    Debug.unityLogger.Log(msg);
                    isReady = true;
                }
                else
                {
                    msg = "Google Login Failed";
                    Debug.unityLogger.Log(msg);
                }
            });
        }
    }

    public void OnClickLogout()
    {
        Debug.unityLogger.Log("OnClickLogout");

        if (!isReady) return;

        ((PlayGamesPlatform)Social.Active).SignOut();        

        msg = "Logout";
        Debug.unityLogger.Log(msg);
    }   

    public void OnClickToken()
    {
        Debug.unityLogger.Log("OnClickToken");

        if (!isReady) return;

        this.GetGooleAuthToken();
    }

    public void OnClickBuy()
    {
        Debug.unityLogger.Log("OnClickBuy");

        IAPManager.Instance.BuyConsumable("SH0000");
    }
    #endregion
}
