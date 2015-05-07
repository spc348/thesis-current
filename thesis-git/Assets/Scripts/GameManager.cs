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
        PreferenceSelections.PrefsRef.MakePlayerInstance();
        Handheld.PlayFullScreenMovie("story.mp4", Color.black, FullScreenMovieControlMode.Full);
        GameOn = true;
		Tokens = 0; 
		CourseScript.CourseRef.GameStart ();
		PanelOrganizer.PanelsRef.GameStart ();
		PlayerScript.PlayerRef.Reset ();
		StartCoroutine (BusScript.drive ());
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = true;
	}

    public void EndGame()
    {
        GameOn = false;
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = false;
        PreferenceSelections.PrefsRef.LogData();
        PreferenceSelections.PrefsRef.ClearData();
        ResetGame();
    }

    public void PauseGame()
    {
        GameOn = false;
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = false;
    }

    public void ResumeGame()
    {
        GameOn = true;
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = false;
    }

	public void ResetGame ()
    {
        Handheld.PlayFullScreenMovie("story.mp4", Color.black, FullScreenMovieControlMode.Full);
        GameOn = true;
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = true;
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
