using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceSelections : MonoBehaviour
{
    //attributes
    public static PlayerAttributes InstanceAttributes;
    public float ShowPanelTime;
    public float PlayAudioTime;
    public float PlayerSpeed;
    public float PlayerRotationSpeed;
    public bool UseTextInstruction;
    public int CourseLength;
    public int CrossingTime;

    //data
    public static PlayerData InstanceData;
    public int SkippedLandmark;
    public float AvgSecsToTarget;
    public int Jaywalked;
    public int CollidePedestrian;
    public int Lost;

    List<PlayerData> datumHistory;

    public static bool PrefsSelected;

    public static PreferenceSelections PrefsRef;

    void Awake()
    {
        PrefsSelected = false;
        PrefsRef = this;
        datumHistory = new List<PlayerData>();

        if (InstanceData == null)
        {
            InstanceData = new PlayerData(SkippedLandmark, AvgSecsToTarget, Jaywalked, CollidePedestrian, Lost);
        }
        else
        {
            LogData();
        }

        if(InstanceAttributes == null)
        {
            InstanceAttributes = new PlayerAttributes(8f, 2f, 35f, 55f, 7, true, 8f);
        }
    }

    public void LogData()
    {
        datumHistory.Add(InstanceData);
        InstanceData = new PlayerData(SkippedLandmark, AvgSecsToTarget, Jaywalked, CollidePedestrian, Lost);
    }

    public void ClearData()
    {
        SkippedLandmark = 0;
        AvgSecsToTarget = 0;
        Jaywalked = 0;
        CollidePedestrian = 0;
        Lost = 0;
    }

    public void SetCrossingTime(float length)
    {
        Crossing.CrossingTime = length;
        InstanceAttributes.CrossingTime = length;
    }
    public void SetCourseLength(int length)
    {
        CourseScript.CourseLength = length;
        InstanceAttributes.CourseLength = length;
    }
    public void SetUseSymbBoolean(bool decision)
    {
        PanelOrganizer.UseSymbInstruction = decision;
        InstanceAttributes.UseSymbols = decision;
    }
    public void SetRepeatPanelLength(float value)
    {
        AudioTrigger.RepeatPanelTime = value;
        InstanceAttributes.RepeatPanelTime = value;
    }
    public void SetAudioLength(float value)
    {
        AudioTrigger.AudioRoutineLength = value;
        InstanceAttributes.PlayAudioTime = value;
    }
    public void SetPlayerSpeed(float value)
    {
        PlayerScript.Speed = value;
        InstanceAttributes.PlayerSpeed = value;
    }
    public void SetPlayerRotSpeed(float value)
    {
        PlayerScript.RotSpeed = value;
        InstanceAttributes.PlayerRotationSpeed = value;
    }

    public void MakePlayerInstance()
    {
        InstanceAttributes = new PlayerAttributes(AudioTrigger.RepeatPanelTime, AudioTrigger.AudioRoutineLength,
                                     PlayerScript.Speed, PlayerScript.RotSpeed, CourseScript.CourseLength,
                                     PanelOrganizer.UseSymbInstruction, Crossing.CrossingTime);
        PrefsSelected = true;
    }

    public class PlayerData
    {
        //player stats
        public int SkippedLandmarks;
        public float AvgSecsToTarget;
        public int Jaywalked;
        public int CollidePedestrian;
        public int Lost;

        public PlayerData(int skipped, float timeTo, int jays, int bumps, int lost)
        {
            PreferenceSelections.InstanceData = this;
            this.SkippedLandmarks = skipped;
            this.AvgSecsToTarget = timeTo;
            this.Jaywalked = jays;
            this.CollidePedestrian = bumps;
            this.Lost = lost;

        }
    }

    public class PlayerAttributes
    {
        //player preferences
        public float RepeatPanelTime;
        public float PlayAudioTime;
        public float PlayerSpeed;
        public float PlayerRotationSpeed;
        public bool UseSymbols;
        public int CourseLength;
        public float CrossingTime;

        public PlayerAttributes(float repeatPanelTime, float playAudioTime, float playerSpeed,
                               float playerRotationSpeed, int courseLength,
                               bool useTextInstruction, float crossingTime)
        {
            PreferenceSelections.InstanceAttributes = this;
            this.RepeatPanelTime = repeatPanelTime;
            this.PlayAudioTime = playAudioTime;
            this.PlayerSpeed = playerSpeed;
            this.PlayerRotationSpeed = playerRotationSpeed;
            this.CourseLength = courseLength;
            this.UseSymbols = useTextInstruction;
            this.CrossingTime = crossingTime;
        }
    }

}
