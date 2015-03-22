using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public PlayerScript playerScript;
    public bus_movement busScript;
    public PanelOrganizer panels;
    public CourseScript course;
    public GameObject player, bus, cat;
    public static int life;
    public Crossing crossing;

	// Use this for initialization
	void Start () {
        panels = GameObject.FindGameObjectWithTag("PanelOrganizer").GetComponent<PanelOrganizer>();
        course = GameObject.FindGameObjectWithTag("CoursePlanner").GetComponent<CourseScript>();
        playerScript = player.GetComponent<PlayerScript>();
        busScript = bus.GetComponent<bus_movement>();
        StartGame();
	}
	
    public void StartGame()
    {
        life = 3;
        panels.GameStart();
        course.GameStart();
        playerScript.reset();
        StartCoroutine(busScript.drive());
    }

    public void ResetGame()
    {
        life = 3;
        panels.restart();
        course.restart();
        busScript.restart();
        busScript.drive();
        playerScript.reset();
        StartCoroutine(busScript.drive());
    }

	// Update is called once per frame
	void Update () {
	
	}
}
