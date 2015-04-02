using System.Collections;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
	public CourseScript Script;
	public PlayerScript Movement;
	public GameObject Player;
	public CheckpointScript Scoreboard;
	public AudioClip Clip;
	public AudioClip LandmarkName;
	private AudioSource source;
	public bool HasPlayed, Final;
	public bool AtCheckPoint;
	public PanelOrganizer Panels;
	public int Place, Duration; 
	static float audioRoutineLength;
	static float repeatPanelTime;
	public PreferenceSelections Prefs;

	public static float AudioRoutineLength {
		get {
			return audioRoutineLength;
		}
		set {
			audioRoutineLength = value;
		}
	}

	public static float RepeatPanelTime {
		get {
			return repeatPanelTime;
		}
		set {
			repeatPanelTime = value;
		}
	}

	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		Movement = Player.GetComponent<PlayerScript> ();
		Panels = GameObject.FindGameObjectWithTag ("PanelOrganizer").GetComponent<PanelOrganizer> ();
		Script = GameObject.FindGameObjectWithTag ("CoursePlanner").GetComponent<CourseScript> ();
		source = GetComponent<AudioSource> ();
		Prefs = GameObject.FindGameObjectWithTag ("Prefs").GetComponent<PreferenceSelections> ();
		HasPlayed = false;
	}

	void Start ()
	{
		AtCheckPoint = false;
	}


	void FlipPlayedBool ()
	{
		HasPlayed = !HasPlayed;
	}

	IEnumerator playAudioRoutine (AudioClip clip)
	{
		if (!source.isPlaying) {
			source.PlayOneShot (clip);

			yield return new WaitForSeconds (clip.length + Prefs._PlayerInstance.PlayAudioTime);
			Panels.ClearSubpanels ();
			Movement.enabled = true;
		}
	}

	public void ResetPlayedFlag ()
	{
		HasPlayed = false;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && !source.isPlaying && other.gameObject.activeSelf) {

			Debug.Log (Place);
			AtCheckPoint = true;
			Script.AtCheckPoint = true;

			Script.ApproachedMarker = Place;

			if (!HasPlayed) {
				Movement.enabled = false;
				StartCoroutine (playAudioRoutine (Clip));
				Panels.InitGameSubpanel (Place);
				HasPlayed = true;
			}

			Invoke ("FlipPlayedBool", Prefs._PlayerInstance.ShowPanelTime); 

			if (Final) {
				Panels.AtFinal ();
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			Script.AtCheckPoint = false;
			Panels.ClearSubpanels ();
		}
		Debug.Log (Place + " " + System.DateTime.Now.ToLongTimeString ());
	}
}
