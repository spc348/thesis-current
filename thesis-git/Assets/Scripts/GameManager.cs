using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public PreferenceSelections Preferences;
	public PlayerScript PlayerScript;
	public bus_movement BusScript;
	public PanelOrganizer Panels;
	public CourseScript Course;
	public GameObject Player, Bus, Cat;
	public static int Life;
	public Crossing Crossing;

	void Start ()
	{
		Panels = GameObject.FindGameObjectWithTag ("PanelOrganizer").GetComponent<PanelOrganizer> ();
		Course = GameObject.FindGameObjectWithTag ("CoursePlanner").GetComponent<CourseScript> ();
		PlayerScript = Player.GetComponent<PlayerScript> ();
		BusScript = Bus.GetComponent<bus_movement> ();
	}
	
	public void StartGame ()
	{
		Life = 3;
		Panels.GameStart ();
		Course.GameStart ();
		PlayerScript.Reset ();
		StartCoroutine (BusScript.drive ());
	}

	public void ResetGame ()
	{
		Preferences.MakePlayerInstace ();
		Life = 3;
		Panels.Restart ();
		Course.Restart ();
		BusScript.restart ();
		BusScript.drive ();
		PlayerScript.Reset ();
		StartCoroutine (BusScript.drive ());
	}
}
