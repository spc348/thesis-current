using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseScript : MonoBehaviour
{
	public static CourseScript CourseRef;
	public AudioTrigger AudioScript;
	private AudioClip clip;
	public Dictionary<string, AudioClip> ClipLibrary;
	public float DistToNext;
	private bool landmarkFlag;
	public bool LifeFlag, AtCheckPoint;
	public GameObject[] LoadedMarkers, Audiotriggers;
	private bool LostFlag;
	public GameObject M1, M2, M3, M4, M5, M6, Cat;
	private GameObject[] Markers;
	public List<GameObject> MarkersList;
	public int NextMarker, PresentMarker, PreviousMarker, ApproachedMarker;
	public AudioSource NotifierAudio;
	public GameObject Player, CheckPrefab, Notifier;
	public Dictionary<int, Vector3> Positions;
	public AudioSource source;
	public static int CourseLength { get; set; }

	private void Awake ()
	{
		CourseLength = 7;
		CourseRef = this;
	}

	private void Update ()
	{
		if (PreferenceSelections.PrefsSelected) {
			CourseLength = PreferenceSelections.InstanceAttributes.CourseLength;
		}
	}

	private void Start ()
	{
		NextMarker = 0;
		PresentMarker = 0;
		LifeFlag = false;
		landmarkFlag = false;
		AtCheckPoint = false;
		LostFlag = false;
		NotifierAudio = Notifier.GetComponent<AudioSource> ();
	}

	public IEnumerator PromptRoutine (int prompt, float duration)
	{
		PlayerScript.PlayerRef.enabled = false;

		if (!source.isPlaying) {
			switch (prompt) {
			case 0:
				Music_Manager.MusicRef.SwitchToVoice ();
				source.PlayOneShot (ClipLibrary ["turnaround"]);
				break;
			case 1:
				Music_Manager.MusicRef.SwitchToVoice ();
				source.PlayOneShot (ClipLibrary ["toofar"]);
				break;
			case 2:
				Music_Manager.MusicRef.SwitchToVoice ();
				source.PlayOneShot (ClipLibrary ["cross"]);
				break;
			case 4:
				Music_Manager.MusicRef.SwitchToVoice ();
				source.PlayOneShot (ClipLibrary ["jaywalking"]);
				break;
			default:
				break;
			}
		}

		PanelOrganizer.PanelsRef.InitUtilPanel (prompt);

		yield return new WaitForSeconds (duration);

		PanelOrganizer.PanelsRef.ClearUtils ();
		PlayerScript.PlayerRef.enabled = true;
		Music_Manager.MusicRef.SwitchToAmbient ();
	}

	private void LateUpdate ()
	{
		if (!GameManager.GameOn)
			return;
		if (AtCheckPoint) {
			PresentMarker = ApproachedMarker;
			NextMarker = ChartNextMove (PresentMarker);
			if (CheckOffTrack () && !landmarkFlag) {
				PreferenceSelections.PrefsRef.SkippedLandmark += 1;
				StartCoroutine (PromptRoutine (0, PanelOrganizer.PanelFade));
				landmarkFlag = true;
			}
		} else {
			PreviousMarker = PresentMarker;
			landmarkFlag = false;
		}
		if (InitDistanceTracking () > 40f) {
			if (!LostFlag) {
				BG_Music.ToggleNeutral ();
				PreferenceSelections.PrefsRef.Lost += 1;
				StartCoroutine (PromptRoutine (1, PanelOrganizer.PanelFade));
				LostFlag = true;
				Invoke ("ResetLostFlag", 12f);
			}
		} else {
			LostFlag = false;
		}
	}

	private void ResetLostFlag ()
	{
		LostFlag = false;
		BG_Music.ToggleHappy ();
	}

	private void LoadMarkerStandins () //load markers from the scene to the loaded markers array
	{
		LoadedMarkers = new GameObject[CourseLength];
		LoadedMarkers [0] = M1;
		LoadedMarkers [1] = M2;
		LoadedMarkers [2] = M3;
		LoadedMarkers [3] = M4;
		LoadedMarkers [4] = M5;
		LoadedMarkers [5] = M6;
		LoadedMarkers [6] = Cat;
	}

	public void GameStart () // Booting the game
	{
		MarkersList = new List<GameObject> (); // list of markers in the game
		LoadMarkerStandins (); // using the positions of stand ins
		ClearMarkers (); // clear any relic markers
		MakeAudioLibrary (); //assemble the source clip libary from resources folder
		MakeCourse (DesignCourse (MarkerCollection ()));
		Player.transform.position = PlayerScript.PlayerStartPosition;
		Player.transform.rotation = PlayerScript.PlayerStartRotation; // set the player in the starting position
		ResetFlags (); //removed any relic flag information
	}

	private void MakeAudioLibrary ()
	{
		//load and label source files into the game 
		ClipLibrary = new Dictionary<string, AudioClip> ();

		//utilities
		ClipLibrary.Add ("atThe", Resources.Load<AudioClip> ("atThe"));
		//directionals
		ClipLibrary.Add ("check", Resources.Load<AudioClip> ("check"));
		ClipLibrary.Add ("great job", Resources.Load<AudioClip> ("greatjob"));
		ClipLibrary.Add ("stop", Resources.Load<AudioClip> ("stop"));
		ClipLibrary.Add ("turnaround", Resources.Load<AudioClip> ("turnaround"));
		ClipLibrary.Add ("turn left", Resources.Load<AudioClip> ("turnleft"));
		ClipLibrary.Add ("turn right", Resources.Load<AudioClip> ("turnright"));
		ClipLibrary.Add ("forward", Resources.Load<AudioClip> ("forward"));
		ClipLibrary.Add ("goto", Resources.Load<AudioClip> ("goto"));
		//prompts
		ClipLibrary.Add ("cross", Resources.Load<AudioClip> ("crossing"));
		ClipLibrary.Add ("jaywalking", Resources.Load<AudioClip> ("jaywalker"));
		ClipLibrary.Add ("toofar", Resources.Load<AudioClip> ("toofar"));
		//reward
		//landmarks
		ClipLibrary.Add ("trashcan", Resources.Load<AudioClip> ("trashcan"));
		ClipLibrary.Add ("Missy", Resources.Load<AudioClip> ("Missy"));
		ClipLibrary.Add ("crosswalk", Resources.Load<AudioClip> ("crosswalk"));
		ClipLibrary.Add ("streetlamp", Resources.Load<AudioClip> ("streetlamp"));
		ClipLibrary.Add ("streetlight", Resources.Load<AudioClip> ("streetlight"));
		ClipLibrary.Add ("hydrant", Resources.Load<AudioClip> ("hydrant"));
		ClipLibrary.Add ("tree", Resources.Load<AudioClip> ("tree"));
		//prompts
		//reward
	}

	public Vector3[] MarkerCollection ()
	{
		// derive position from marker stand ins

		var markerVectors = new Vector3[CourseLength];

		for (var i = 0; i < markerVectors.Length; i++) {
			markerVectors [i] = LoadedMarkers [i].transform.position;
		}

		return markerVectors;
	}

	private Dictionary<int, Vector3> DesignCourse (Vector3[] vectors)
	{
		//order the positions of the markers for the specified course

		var positions = new Dictionary<int, Vector3> ();

		positions.Add (0, vectors [0]);
		positions.Add (1, vectors [1]);
		positions.Add (2, vectors [2]);
		positions.Add (3, vectors [3]);
		positions.Add (4, vectors [4]);
		positions.Add (5, vectors [5]);
		positions.Add (6, vectors [6]);

		return positions;
	}

	private void MakeCourse (Dictionary<int, Vector3> positions)
	{
		// reference the correct source files using easy to read labels

		var sevenCourseDirections = new string[CourseLength];
		sevenCourseDirections [0] = "forward";
		sevenCourseDirections [1] = "turn left";
		sevenCourseDirections [2] = "turn right";
		sevenCourseDirections [3] = "turn right";
		sevenCourseDirections [4] = "turn left";
		sevenCourseDirections [5] = "turn right";
		sevenCourseDirections [6] = "great job";

		var sevenCourseLandmarks = new string[CourseLength];
		sevenCourseLandmarks [0] = "streetlamp";
		sevenCourseLandmarks [1] = "crosswalk";
		sevenCourseLandmarks [2] = "trashcan";
		sevenCourseLandmarks [3] = "crosswalk";
		sevenCourseLandmarks [4] = "hydrant";
		sevenCourseLandmarks [5] = "Missy";
		sevenCourseLandmarks [6] = "Missy";

		foreach (var kvp in positions) {
			//make an source checkpoint 
			var marker = Instantiate (CheckPrefab, kvp.Value, Quaternion.identity) as GameObject;
			//generate marker prefab in the scene
			MarkersList.Add (marker);
			marker.tag = "marker";
			AudioScript = marker.GetComponent<AudioTrigger> ();
			AudioScript.Direction = ClipLibrary [sevenCourseDirections [kvp.Key]];
			AudioScript.LandmarkName = ClipLibrary [sevenCourseLandmarks [kvp.Key]];
			AudioScript.Place = kvp.Key;
			AudioScript.HasPlayed = false;

			if (AudioScript.Place == CourseLength - 1)
				AudioScript.Final = true;
		}
	}

	public void AtFinal ()
	{
		PanelOrganizer.PanelsRef.SetPanel (PanelOrganizer.PanelsRef.EndPanel); //end the game
		GameManager.GameManagerRef.EndGame ();
	}

	public void ResetFlags ()
	{
		// find all the markers and clear relic flags
		Audiotriggers = GameObject.FindGameObjectsWithTag ("marker");
		foreach (var i in Audiotriggers) {
			var script = i.GetComponent<AudioTrigger> ();
			script.ResetPlayedFlag ();
		}
	}

	// very basic course check
	public bool CheckOffTrack ()
	{
		return PreviousMarker - PresentMarker > 1 || PresentMarker < PreviousMarker;
	}

	// very basic navigational check
	private int ChartNextMove (int num)
	{
		return ++num;
	}

	public void Restart () //restart the game
	{
		ClearMarkers ();
		ResetFlags ();
		MakeCourse (DesignCourse (MarkerCollection ()));
	}

	public void ClearMarkers () //remove markers from last game
	{
		foreach (var i in MarkersList) {
			Destroy (i);
		}
	}

	public float InitDistanceTracking () // checks distance from player to next marker
	{
		Markers = GameObject.FindGameObjectsWithTag ("marker"); //find current markers
		if (Markers [0] == null)
			return 0;
		return DistToNext = Vector3.Distance (Player.transform.position, Markers [NextMarker].transform.position);
	}
}