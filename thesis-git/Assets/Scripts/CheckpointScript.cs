using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointScript : MonoBehaviour
{
    PanelOrganizer panels;
    GameObject Player;
    public GameObject[] Markers;
    public static int checkpointsHit;
    public float dist_to_next;
    public Text text;
    public CourseScript course;
    public int checkPointPlace, lastPoint, next_expected_place;
    public bool atPoint;

    // Use this for initialization
    void Start()
    {
        atPoint = false;
        panels = GameObject.FindGameObjectWithTag("PanelOrganizer").GetComponent<PanelOrganizer>();
        checkpointsHit = 0;
        course = GameObject.FindGameObjectWithTag("CoursePlanner").GetComponent<CourseScript>();
        course.atCheckPoint = false;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //course.LastMarker = checkPointPlace;
        if(panels.activePanel == panels.gamePanel)
        {
            init_distance_tracking();
        }
    }

    public void atFinal()
    {
        course.atFinal();
    }

    public void init_distance_tracking()
    {
        Markers = GameObject.FindGameObjectsWithTag("marker");
        if (Markers[0] != null)
        {
            dist_to_next = Vector3.Distance(Player.transform.position, Markers[next_expected_place].transform.position);
        }
    }

    public void hitCheckpoint()
    {
        checkpointsHit++;
    }

    public void ToggleCheckPoint()
    {
        atPoint = !atPoint;
    }
}
