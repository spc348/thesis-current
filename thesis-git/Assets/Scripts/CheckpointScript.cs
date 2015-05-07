using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointScript : MonoBehaviour
{
    public float[] Times;
    public int CurrentPlace;
    public bool flag;

    public static CheckpointScript CheckPointRef;
    public bool ReachedNewMarker;

    void Awake()
    {
        CheckPointRef = this;
    }

    void Start()
    {
        Times = new float[CourseScript.CourseLength];
        CurrentPlace = 0;
        flag = false;
        ReachedNewMarker = false;
    }

    private void LateUpdate()
    {
        if (GameManager.GameOn)
        {
            if (CourseScript.CourseRef.AtCheckPoint)
            {
                if (!CourseScript.CourseRef.CheckOffTrack())
                {
                    if (!flag && ReachedNewMarker)
                    {
                        CurrentPlace++;
                        flag = true;
                    }
                }
            }
            else if (GameManager.GameOn)
            {
                if (PanelOrganizer.PanelsRef.ActivePanel == PanelOrganizer.PanelsRef.GamePanel)
                {
                    Times[CurrentPlace] += Time.deltaTime;

                    flag = false;
                    ReachedNewMarker = false;
                }
            }
        }
    }

    public float AvgTimes()
    {
        float n = 0;
        for (int i = 0; i < Times.Length; i++)
        {
            n += Times[i];
        }
        return n / Times.Length;
    }


}
