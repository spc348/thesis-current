using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseScript : MonoBehaviour
{
    public Dictionary<int, Vector3> Positions;
    public Dictionary<string, AudioClip> clipLibrary;
    public PanelOrganizer panels;
    public List<GameObject> markers;
    public GameObject[] loadedMarkers, audiotriggers;
    public GameObject player, CheckPrefab;
	public int nextMarker, LastMarker,previousMarker,currentMarker;
    public AudioTrigger audioScript;
    public bool lifeFlag, atCheckPoint;
    public int courseLength = 7;
    public PlayerScript playerScript;
    public GameObject m1, m2, m3, m4, m5, m6, cat;
    private GameObject[] Markers;
    public float dist_to_next;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        panels = GameObject.FindGameObjectWithTag("PanelOrganizer").GetComponent<PanelOrganizer>();
        markers = new List<GameObject>();
        loadMarkers();
    }

    void Start()
    {
        nextMarker = 0;
        LastMarker = 0;
        lifeFlag = false;
        atCheckPoint = false;
        player.transform.position = playerScript.playerStartPosition;

    }

    void loadMarkers()
    {
        loadedMarkers = new GameObject[courseLength];
        loadedMarkers[0] = m1;
        loadedMarkers[1] = m2;
        loadedMarkers[2] = m3;
        loadedMarkers[3] = m4;
        loadedMarkers[4] = m5;
        loadedMarkers[5] = m6;
        loadedMarkers[6] = cat;
    }

    public Vector3[] Markercollection()
    {

        Vector3[] markerVectors = new Vector3[courseLength];

        for (int i = 0; i < markerVectors.Length; i++)
        {
            markerVectors[i] = loadedMarkers[i].transform.position;
        }

        return markerVectors;
    }

    public void GameStart()
    {
        clear_markers();
        makeAudioLibrary();
        makeCourse(designCourse(Markercollection()));
        player.transform.position = playerScript.playerStartPosition;
        player.transform.rotation = playerScript.playerStartRotation;
        reset_flags();
    }

    public void atFinal()
    {
        panels.SetPanel(panels.endPanel);

    }

    public void reset_flags()
    {
        audiotriggers = GameObject.FindGameObjectsWithTag("marker");
        foreach (var i in audiotriggers)
        {
            AudioTrigger script = i.GetComponent<AudioTrigger>();
            script.reset_played_flag();
        }
    }

    void LateUpdate()
    {
        if (atCheckPoint)
        {
            LastMarker = currentMarker;
            nextMarker = chartNextMove(LastMarker);
            if (comparePlace() && !lifeFlag)
            {
                Debug.Log("wrong way");
                panels.SubLife();
                Debug.Log("lives left " + GameManager.life);
                lifeFlag = true;
            }
        }
        else
        {
            previousMarker = LastMarker;
            lifeFlag = false;
        }


        if (panels.activePanel == panels.gamePanel)
        {
            init_distance_tracking();
        }
    }

    bool comparePlace()
    {
        return previousMarker - LastMarker > 1 || LastMarker < previousMarker;
    }

    int chartNextMove(int num)
    {
        return ++num;
    }

    void makeAudioLibrary()
    {
        clipLibrary = new Dictionary<string, AudioClip>();

        clipLibrary.Add("check", Resources.Load<AudioClip>("check"));
        clipLibrary.Add("great job", Resources.Load<AudioClip>("greatjob"));
        clipLibrary.Add("stop", Resources.Load<AudioClip>("stop"));
        clipLibrary.Add("turn around", Resources.Load<AudioClip>("turnaround"));
        clipLibrary.Add("turn left", Resources.Load<AudioClip>("turnleft"));
        clipLibrary.Add("turn right", Resources.Load<AudioClip>("turnright"));
        clipLibrary.Add("forward", Resources.Load<AudioClip>("forward"));
    }

    Dictionary<int, Vector3> designCourse(Vector3[] Vectors)
    {

        Dictionary<int, Vector3> positions = new Dictionary<int, Vector3>();

        positions.Add(0, Vectors[0]);
        positions.Add(1, Vectors[1]);
        positions.Add(2, Vectors[2]);
        positions.Add(3, Vectors[3]);
        positions.Add(4, Vectors[4]);
        positions.Add(5, Vectors[5]);
        positions.Add(6, Vectors[6]);

        return positions;
    }

    void makeCourse(Dictionary<int, Vector3> positions)
    {

        string[] directions = new string[courseLength];
        directions[0] = "forward";
        directions[1] = "turn left";
        directions[2] = "turn right";
        directions[3] = "turn left";
        directions[4] = "stop";
        directions[5] = "turn right";
        directions[6] = "turn left";

        foreach (KeyValuePair<int, Vector3> kvp in positions)
        {
            //make an audio checkpoint 
            GameObject marker = Instantiate(CheckPrefab, kvp.Value, Quaternion.identity) as GameObject;
            markers.Add(marker);
            marker.tag = "marker";
            audioScript = marker.GetComponent<AudioTrigger>();
            audioScript.clip = clipLibrary[directions[kvp.Key]];
            audioScript.place = kvp.Key;
            audioScript.hasPlayed = false;

            if (audioScript.place == courseLength - 1)
                audioScript.final = true;
        }
    }

    public void restart()
    {
        clear_markers();
        reset_flags();
    }

    public void clear_markers()
    {
        foreach (var i in markers)
        {
            Destroy(i);
        }
    }

    public void init_distance_tracking()
    {
        Markers = GameObject.FindGameObjectsWithTag("marker");
        if (Markers[0] != null)
        {
            dist_to_next = Vector3.Distance(player.transform.position, Markers[nextMarker].transform.position);
        }
    }
}
