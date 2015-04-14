using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{

    public Text LandmarksMissed, Jaywalked, AvgTimeTo, Collisions;
    public static Statistics StatisticsRef;

    void Awake()
    {
        StatisticsRef = this;
    }

    public void Load()
    {
        LandmarksMissed.text = PreferenceSelections.PrefsRef.SkippedLandmark.ToString();
        Jaywalked.text = PreferenceSelections.PrefsRef.Jaywalked.ToString();
        AvgTimeTo.text = CheckpointScript.CheckPointRef.AvgTimes().ToString();
        Collisions.text = PreferenceSelections.PrefsRef.CollidePedestrian.ToString();
    }
}
