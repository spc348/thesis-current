using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public bus_movement BusScript;
	public GameObject Player, Bus, Cat;
	public static int Tokens;
	public Crossing Crossing;

    public static bool GameOn;
	public static GameManager GameManagerRef;

	void Awake ()
	{
		GameManagerRef = this;
        GameOn = false;
	}

	void Start ()
	{
		BusScript = Bus.GetComponent<bus_movement> ();
	}
	
	public void StartGame ()
	{
        GameOn = true;
		Tokens = 0; 
		CourseScript.CourseRef.GameStart ();
		PanelOrganizer.PanelsRef.GameStart ();
		PlayerScript.PlayerRef.Reset ();
		StartCoroutine (BusScript.drive ());
	}

    public void EndGame()
    {
        GameOn = false;
        PreferenceSelections.PrefsRef.LogData();
        PreferenceSelections.PrefsRef.ClearData();
    }

	public void ResetGame ()
	{
        GameOn = true;
		PreferenceSelections.PrefsRef.MakePlayerInstance ();
        Tokens = 0;
		PanelOrganizer.PanelsRef.Restart ();
		CourseScript.CourseRef.Restart ();
		BusScript.restart ();
		BusScript.drive ();
		PlayerScript.PlayerRef.Reset ();
		StartCoroutine (BusScript.drive ());
	}
}
