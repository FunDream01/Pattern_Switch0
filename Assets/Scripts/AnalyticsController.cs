using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
public class AnalyticsController : MonoBehaviour
{
    public static AnalyticsController instance;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LogLevelStarted(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level.ToString());
    }

    public void LogLevelFailed(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, level.ToString());
    }

    public void LogLevelSucceeded(int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, level.ToString());
    }
}
