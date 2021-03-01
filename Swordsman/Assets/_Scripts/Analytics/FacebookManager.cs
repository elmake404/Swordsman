using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Include Facebook namespace
using Facebook.Unity;


public class FacebookManager : MonoBehaviour
{
    public static FacebookManager Instance;
    // Awake function from Unity's MonoBehavior
    void Awake()
    {
        Instance = this;
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
        DontDestroyOnLoad(this );
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

    public void GameStart()
    {
        var tutParms = new Dictionary<string, object>();
        tutParms["Game start"] = "The game was launched";

        FB.LogAppEvent(
            "Game start",
            parameters : tutParms);
    }
    public void LevelStart(int lvl)
    {
        var tutParms = new Dictionary<string, object>();
        tutParms["Level Namber"] = lvl.ToString();

        FB.LogAppEvent(
            "Level start",
            parameters : tutParms);
    }
    public void LevelWin(int lvl)
    {
        var tutParms = new Dictionary<string, object>();
        tutParms["Level Namber"] = lvl.ToString();

        FB.LogAppEvent(
            "Level win",
            parameters : tutParms);
    }
    public void LevelFail(int lvl)
    {
        var tutParms = new Dictionary<string, object>();
        tutParms["Level Namber"] = lvl.ToString();

        FB.LogAppEvent(
            "Level fail",
            parameters : tutParms);
    }
    public void MainMenu()
    {
        var tutParms = new Dictionary<string, object>();
        tutParms["Main menu"] = "Main menu";

        FB.LogAppEvent(
            "Main menu",
            parameters: tutParms);
    }
}
