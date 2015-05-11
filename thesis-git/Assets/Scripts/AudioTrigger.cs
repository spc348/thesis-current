using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger : MonoBehaviour
{
	public GameObject Player;
	public CheckpointScript Scoreboard;
	public AudioClip Direction;
	public AudioClip LandmarkName;
	AudioSource source;
	public bool HasPlayed, Final;
	public bool AtCheckPoint, TokenFlag;
	public int Place;
	public float Duration;

	static float audioRoutineLength;
	static float repeatPanelTime;

	public static AudioTrigger AudioRef;

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
		AudioRef = this;
		Player = GameObject.FindGameObjectWithTag ("Player");
		source = GetComponent<AudioSource> ();
		HasPlayed = false;
	}

	void Start ()
	{
		AtCheckPoint = false;
		audioRoutineLength = 2f;
		repeatPanelTime = 8f;
	}

	void FlipPlayedBool ()
	{
		HasPlayed = !HasPlayed;
	}

	IEnumerator playAudioRoutine (AudioClip direction, AudioClip landmark)
	{
		if (!source.isPlaying) {
            Music_Manager.MusicRef.SwitchToVoice();
			source.PlayOneShot (direction);
			yield return new WaitForSeconds (direction.length);

			source.PlayOneShot (CourseScript.CourseRef.ClipLibrary ["atThe"]);
			yield return new WaitForSeconds (CourseScript.CourseRef.ClipLibrary ["atThe"].length);

			source.PlayOneShot (landmark);
			yield return new WaitForSeconds (landmark.length + PreferenceSelections.InstanceAttributes.PlayAudioTime);

			PanelOrganizer.PanelsRef.ClearSubpanels ();
			PlayerScript.PlayerRef.enabled = true;
            Music_Manager.MusicRef.SwitchToAmbient();
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
            CourseScript.CourseRef.AtCheckPoint = true;

			CourseScript.CourseRef.ApproachedMarker = Place;

			if (!HasPlayed) {
                CheckpointScript.CheckPointRef.ReachedNewMarker = true;
				PlayerScript.PlayerRef.enabled = false;
				StartCoroutine (playAudioRoutine (Direction, LandmarkName));
				PanelOrganizer.PanelsRef.InitGameSubpanel (Place);
                PanelOrganizer.PanelsRef.AddToken(Place);
				HasPlayed = true;
			}


			Invoke ("FlipPlayedBool", repeatPanelTime);

		    if (Final)
		        PanelOrganizer.PanelsRef.AtFinal();
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
            CheckpointScript.CheckPointRef.ReachedNewMarker = true;
            CourseScript.CourseRef.AtCheckPoint = false;
			PanelOrganizer.PanelsRef.ClearSubpanels ();
		}
	}
}
