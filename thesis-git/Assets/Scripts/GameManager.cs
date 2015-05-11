using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Tokens;
    public static bool GameOn;
    public static GameManager GameManagerRef;
    public bus_movement BusScript;
    public Crossing Crossing;
    public GameObject Player, Bus, Cat;

    private void Awake()
    {
        GameManagerRef = this;
        GameOn = false;
    }

    public static void SetGameOn()
    {
        GameOn = true;
    }

    public static void SetGameOff()
    {
        GameOn = false;
    }

    private void Start()
    {
        BusScript = Bus.GetComponent<bus_movement>();
    }

    public void StartGame()
    {
        PreferenceSelections.PrefsRef.MakePlayerInstance();
        Handheld.PlayFullScreenMovie("story.mp4", Color.black, FullScreenMovieControlMode.Full);
        SetGameOn();
        Tokens = 0;
        CourseScript.CourseRef.GameStart();
        PanelOrganizer.PanelsRef.GameStart();
        PlayerScript.PlayerRef.Reset();
        StartCoroutine(BusScript.drive());
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = true;
    }

    public void EndGame()
    {
        SetGameOff();
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = false;
        PreferenceSelections.PrefsRef.LogData();
        PreferenceSelections.PrefsRef.ClearData();
        ResetGame();
    }

    public void PauseGame()
    {
        SetGameOff();
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = false;
    }

    public void ResumeGame()
    {
        SetGameOn();
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = false;
        PanelOrganizer.PanelsRef.SetPanel(PanelOrganizer.PanelsRef.GamePanel);
    }

    public void ResetGame()
    {
        Handheld.PlayFullScreenMovie("story.mp4", Color.black, FullScreenMovieControlMode.Full);
        Tokens = 0;
        PlayerScript.PlayerRef.GetComponent<Collider>().enabled = true;
        PreferenceSelections.PrefsRef.MakePlayerInstance();
        BusScript.restart();
        BusScript.drive();
        StartCoroutine(BusScript.drive());
        PanelOrganizer.PanelsRef.Restart();
        CourseScript.CourseRef.Restart();
        PlayerScript.PlayerRef.Reset();
        SetGameOn();
    }
}