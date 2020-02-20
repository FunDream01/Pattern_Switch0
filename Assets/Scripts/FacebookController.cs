using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookController : MonoBehaviour
{



     void Start()
    {DontDestroyOnLoad(this.gameObject);}


    public void LogLevelStart(int level)
    {
        var facebookParams = new Dictionary<string, object>();
        facebookParams[AppEventParameterName.ContentID] = "LevelStarted";
        facebookParams[AppEventParameterName.Description] = "User has loaded the level.";
        facebookParams[AppEventParameterName.Success] = "1";
        facebookParams[AppEventParameterName.Level] = level;

        FB.LogAppEvent(
            "StartedLevel",
            parameters: facebookParams
        );

    }



    public void LogLevelSuccess(int level)
    {
        var facebookParams = new Dictionary<string, object>();
        facebookParams[AppEventParameterName.ContentID] = "Level Succeeded";
        facebookParams[AppEventParameterName.Description] = "User has completed the level.";
        facebookParams[AppEventParameterName.Success] = "1";
        facebookParams[AppEventParameterName.Level] = level;

        FB.LogAppEvent(
            "SucceededLevel",
            parameters: facebookParams
        );

    }


    public void LogLevelFailure(int level)
    {
        var facebookParams = new Dictionary<string, object>();
        facebookParams[AppEventParameterName.ContentID] = "Level Failed";
        facebookParams[AppEventParameterName.Description] = "User has failed the level.";
        facebookParams[AppEventParameterName.Success] = "1";
        facebookParams[AppEventParameterName.Level] = level;

        FB.LogAppEvent(
            "FailedLevel",
            parameters: facebookParams
        );

    }


    void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

}
