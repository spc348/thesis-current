using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointScript : MonoBehaviour
{
    public float[] Times;
    public int CurrentPlace;
    public bool flag;

    public static CheckpointScript CheckPointRef;

    void Awake()
    {
        CheckPointRef = this;
    }

    void Start()
    {
        Times = new float[CourseScript.CourseLength];
        CurrentPlace = 0;
        flag = false;
    }

    void LateUpdate()
    {
        if (CourseScript.CourseRef.AtCheckPoint && !CourseScript.CourseRef.CheckOffTrack())
        {
            if (!flag)
            {
                CurrentPlace++;
                flag = true;
            }
        }
        else if(PanelOrganizer.PanelsRef.ActivePanel == PanelOrganizer.PanelsRef.GamePanel && GameManager.GameOn)
        {
            Times[CurrentPlace] += Time.deltaTime;

            flag = false;
        }
    }

    public float AvgTimes()
    {
        float n = 0;
        for (int i = 0; i < Times.Length; i++)
        {
            n += Times[i];     
        }
        return n/Times.Length;
    }


}
