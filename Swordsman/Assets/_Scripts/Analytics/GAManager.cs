using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GAManager : MonoBehaviour
{
    public static GAManager Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        GameAnalytics.Initialize();
    }
    public void GameStart()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game start");
    }
    public void LevelStart(int lvl)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level start: " + lvl);
    }
    public void LevelWin(int lvl)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,"Level: "+ lvl);
    }
    public void LevelFail(int lvl)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level: " + lvl);
    }
}
