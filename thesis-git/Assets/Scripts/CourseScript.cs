using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CourseScript : MonoBehaviour
{

    public Dictionary<int, Vector3> Positions;
    public Dictionary<string, AudioClip> ClipLibrary;
    public List<GameObject> MarkersList;
    public GameObject[] LoadedMarkers, Audiotriggers;
    public GameObject Player, CheckPrefab, Notifier;
    public AudioSource NotifierAudio;
    public int NextMarker, PresentMarker, PreviousMarker, ApproachedMarker;

    public AudioTrigger AudioScript;

    public bool LifeFlag, AtCheckPoint;
    static int courseLength;
    public GameObject M1, M2, M3, M4, M5, M6, Cat;
    GameObject[] Markers;
    public float DistToNext;
    bool landmarkFlag;
    bool LostFlag;

    public static CourseScript CourseRef;

    public static int CourseLength
    {
        get
        {
            return courseLength;
        }
        set
        {
            courseLength = value;
        }
    }

    void Awake()
    {
        courseLength = 7;
        CourseRef = this;
    }

    void Update()
    {
        if (PreferenceSelections.PrefsSelected)
        {
            courseLength = PreferenceSelections.InstanceAttributes.CourseLength;
        }
    }

    void Start()
    {
        NextMarker = 0;
        PresentMarker = 0;
        LifeFlag = false;
        landmarkFlag = false;
        AtCheckPoint = false;
        LostFlag = false;
        NotifierAudio = Notifier.GetComponent<AudioSource>();
    }

    public IEnumerator PromptRoutine(int prompt, float duration)
    {
        PanelOrganizer.PanelsRef.InitUtilPanel(prompt);

        yield return new WaitForSeconds(duration);

        PanelOrganizer.PanelsRef.ClearUtils();
    }

    void LateUpdate()
    {
        if (GameManager.GameOn)
        {
            if (AtCheckPoint)
            {
                PresentMarker = ApproachedMarker;
                NextMarker = ChartNextMove(PresentMarker);
                if (CheckOffTrack() && !landmarkFlag)
                {
                    PreferenceSelections.PrefsRef.SkippedLandmark += 1;
                    StartCoroutine(PromptRoutine(0, PanelOrganizer.PanelFade));
                    landmarkFlag = true;
                }
            }
            else
            {
                PreviousMarker = PresentMarker;
                landmarkFlag = false;
            }

            if (InitDistanceTracking() > 40f)
            {
                if (!LostFlag)
                {
                    PreferenceSelections.PrefsRef.Lost += 1;
                    StartCoroutine(PromptRoutine(1, PanelOrganizer.PanelFade));
                    LostFlag = true;
                }
                Invoke("ResetLostFlag",4f);
            }
        }
    }

    void ResetLostFlag()
    {
        LostFlag = true;
    }

    void LoadMarkerStandins() //load markers from the scene to the loaded markers array
    {
        LoadedMarkers = new GameObject[courseLength];
        LoadedMarkers[0] = M1;
        LoadedMarkers[1] = M2;
        LoadedMarkers[2] = M3;
        LoadedMarkers[3] = M4;
        LoadedMarkers[4] = M5;
        LoadedMarkers[5] = M6;
        LoadedMarkers[6] = Cat;
    }

    public void GameStart() // Booting the game
    {
        MarkersList = new List<GameObject>(); // list of markers in the game
        LoadMarkerStandins(); // using the positions of stand ins
        ClearMarkers(); // clear any relic markers
        MakeAudioLibrary(); //assemble the audio clip libary from resources folder
        MakeCourse(DesignCourse(MarkerCollection())); // insert prefab markers into position, in the designed order, with the
        // correct directional audio clips
        Player.transform.position = PlayerScript.PlayerStartPosition;
        Player.transform.rotation = PlayerScript.PlayerStartRotation; // set the player in the starting position
        ResetFlags(); //removed any relic flag information
    }

    void MakeAudioLibrary()
    {
        //load and label audio files into the game 
        ClipLibrary = new Dictionary<string, AudioClip>();

        //utilities
        ClipLibrary.Add("atThe", Resources.Load<AudioClip>("atThe"));
        /*
         * make a separate 'the' clip 
         * 
         * make a 'go to' clip
         * 
         */

        //directionals
        ClipLibrary.Add("check", Resources.Load<AudioClip>("check"));
        ClipLibrary.Add("great job", Resources.Load<AudioClip>("greatjob"));
        ClipLibrary.Add("stop", Resources.Load<AudioClip>("stop"));
        ClipLibrary.Add("turn around", Resources.Load<AudioClip>("turnaround"));
        ClipLibrary.Add("turn left", Resources.Load<AudioClip>("turnleft"));
        ClipLibrary.Add("turn right", Resources.Load<AudioClip>("turnright"));
        ClipLibrary.Add("forward", Resources.Load<AudioClip>("forward"));
        //landmarks
        ClipLibrary.Add("trashcan", Resources.Load<AudioClip>("trashcan"));
        ClipLibrary.Add("Missy", Resources.Load<AudioClip>("Missy"));
        ClipLibrary.Add("crosswalk", Resources.Load<AudioClip>("crosswalk"));
        ClipLibrary.Add("streetlamp", Resources.Load<AudioClip>("streetlamp"));
        ClipLibrary.Add("streetlight", Resources.Load<AudioClip>("streetlight"));
        ClipLibrary.Add("hydrant", Resources.Load<AudioClip>("hydrant"));
        ClipLibrary.Add("tree", Resources.Load<AudioClip>("tree"));
        //prompts
        //getting cold prompt
        //turn around prompt
        // jaywalking
        //reward
    }

    public Vector3[] MarkerCollection()
    {
        // derive position from marker stand ins

        Vector3[] markerVectors = new Vector3[courseLength];

        for (int i = 0; i < markerVectors.Length; i++)
        {
            markerVectors[i] = LoadedMarkers[i].transform.position;
        }

        return markerVectors;
    }

    Dictionary<int, Vector3> DesignCourse(Vector3[] vectors)
    {
        //order the positions of the markers for the specified course

        Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>();

        positions.Add(0, vectors[0]);
        positions.Add(1, vectors[1]);
        positions.Add(2, vectors[2]);
        positions.Add(3, vectors[3]);
        positions.Add(4, vectors[4]);
        positions.Add(5, vectors[5]);
        positions.Add(6, vectors[6]);

        return positions;
    }

    void MakeCourse(Dictionary<int, Vector3> positions)
    {
        // reference the correct audio files using easy to read labels

        string[] sevenCourseDirections = new string[courseLength];
        sevenCourseDirections[0] = "forward";
        sevenCourseDirections[1] = "turn left";
        sevenCourseDirections[2] = "turn right";
        sevenCourseDirections[3] = "turn right";
        sevenCourseDirections[4] = "turn left";
        sevenCourseDirections[5] = "turn right";
        sevenCourseDirections[6] = "great job";

        string[] sevenCourseLandmarks = new string[courseLength];
        sevenCourseLandmarks[0] = "streetlamp";
        sevenCourseLandmarks[1] = "crosswalk";
        sevenCourseLandmarks[2] = "trashcan";
        sevenCourseLandmarks[3] = "crosswalk";
        sevenCourseLandmarks[4] = "hydrant";
        sevenCourseLandmarks[5] = "Missy";
        sevenCourseLandmarks[6] = "Missy";

        foreach (KeyValuePair<int, Vector3> kvp in positions)
        {
            //make an audio checkpoint 
            GameObject marker = Instantiate(CheckPrefab, kvp.Value, Quaternion.identity) as GameObject; //generate marker prefab in the scene
            MarkersList.Add(marker);
            marker.tag = "marker";
            AudioScript = marker.GetComponent<AudioTrigger>();
            AudioScript.Direction = ClipLibrary[sevenCourseDirections[kvp.Key]];
            AudioScript.LandmarkName = ClipLibrary[sevenCourseLandmarks[kvp.Key]];
            AudioScript.Place = kvp.Key;
            AudioScript.HasPlayed = false;

            if (AudioScript.Place == courseLength - 1)
                AudioScript.Final = true;
        }
    }

    public void AtFinal()
    {
        PanelOrganizer.PanelsRef.SetPanel(PanelOrganizer.PanelsRef.EndPanel); //end the game
        GameManager.GameManagerRef.EndGame();
    }

    public void ResetFlags()
    {
        // find all the markers and clear relic flags

        Audiotriggers = GameObject.FindGameObjectsWithTag("marker");
        foreach (var i in Audiotriggers)
        {
            AudioTrigger script = i.GetComponent<AudioTrigger>();
            script.ResetPlayedFlag();
        }
    }

    // very basic course check
    public bool CheckOffTrack()
    {
        return PreviousMarker - PresentMarker > 1 || PresentMarker < PreviousMarker;
    }

    // very basic navigational check
    int ChartNextMove(int num)
    {
        return ++num;
    }

    public void Restart() //restart the game
    {
        ClearMarkers();
        ResetFlags();
    }

    public void ClearMarkers() //remove markers from last game
    {
        foreach (var i in MarkersList)
        {
            Destroy(i);
        }
    }

    public float InitDistanceTracking() // checks distance from player to next marker
    {
        Markers = GameObject.FindGameObjectsWithTag("marker"); //find current markers
        if (Markers[0] != null)
        {
            return DistToNext = Vector3.Distance(Player.transform.position, Markers[NextMarker].transform.position);
        }
        else
        {
            return 0;
        }
    }
}
