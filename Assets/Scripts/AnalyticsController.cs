using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
public class AnalyticsController
{
    // Start is called before the first frame update
    public AnalyticsController()
    {
        GameAnalytics.Initialize();
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
