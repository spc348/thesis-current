using UnityEngine;
using System.Collections.Generic;

public class CourseScript : MonoBehaviour
{
	// For Unity3d, being public means that I can see these variables in real time in the editor
	public Dictionary<int, Vector3> Positions;
	public Dictionary<string, AudioClip> ClipLibrary;
	public PanelOrganizer Panels;
	public List<GameObject> MarkersList;
	public GameObject[] LoadedMarkers, Audiotriggers;
	public GameObject Player, CheckPrefab;
	public int NextMarker, PresentMarker, PreviousMarker, ApproachedMarker;
	public AudioTrigger AudioScript;
	public bool LifeFlag, AtCheckPoint;
	static int courseLength;
	public PlayerScript PlayerScript;
	public GameObject M1, M2, M3, M4, M5, M6, Cat;
	private GameObject[] Markers;
	public float DistToNext;
	public PreferenceSelections Prefs;

	// Part of the the design of the game is to give caretakers choices in personalizing the game for their students
	// Here the length of the course is alterable
	public static int CourseLength {
		get {
			return courseLength;
		}
		set {
			courseLength = value;
		}
	}

	void Awake ()
	{
		// Find and cache attachments to requisite classes

		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerScript = Player.GetComponent<PlayerScript> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerScript = Player.GetComponent<PlayerScript> ();
		Panels = GameObject.FindGameObjectWithTag ("PanelOrganizer").GetComponent<PanelOrganizer> ();
		courseLength = 7;
	}

	void Start ()
	{
		if (PreferenceSelections.PrefsSelected) {
			courseLength = Prefs._PlayerInstance.CourseLength;
		}
		//Initializations
		NextMarker = 0;
		PresentMarker = 0;
		LifeFlag = false;
		AtCheckPoint = false;
		Player.transform.position = PlayerScript.PlayerStartPosition;

	}

	void LateUpdate ()
	{
		// In progress design of navigational logic
		if (AtCheckPoint) { // at a landmark
			PresentMarker = ApproachedMarker; // observe current location from marker
			NextMarker = ChartNextMove (PresentMarker); // determine where to go next
			if (ComparePlace () && !LifeFlag) { // determine if not going in the right direction and record errors
				Panels.SubLife (); 
				LifeFlag = true;
			}
		} else { // 
			PreviousMarker = PresentMarker; // away from the landmark, iterate to the new nearest marker
			LifeFlag = false;
		}
		
		
		if (Panels.ActivePanel == Panels.GamePanel) { // if in the game
			InitDistanceTracking (); //check distance to the next desired marker
		}
	}

	void loadMarkerStandins () //load markers from the scene to this script
	{
		LoadedMarkers = new GameObject[courseLength];
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
		loadMarkerStandins (); // using the positions of stand ins
		ClearMarkers (); // clear any relic markers
		MakeAudioLibrary (); //assemble the audio clip libary from resources folder
		MakeCourse (DesignCourse (Markercollection ())); // insert prefab markers into position, in the designed order, with the
		// correct directional audio clips
		Player.transform.position = PlayerScript.PlayerStartPosition;
		Player.transform.rotation = PlayerScript.PlayerStartRotation; // set the player in the starting position
		ResetFlags (); //removed any relic flag information
	}
	
	void MakeAudioLibrary ()
	{

		//load and label audio files into the game 

		ClipLibrary = new Dictionary<string, AudioClip> ();

		//directionals
		ClipLibrary.Add ("check", Resources.Load<AudioClip> ("check"));
		ClipLibrary.Add ("great job", Resources.Load<AudioClip> ("greatjob"));
		ClipLibrary.Add ("stop", Resources.Load<AudioClip> ("stop"));
		ClipLibrary.Add ("turn around", Resources.Load<AudioClip> ("turnaround"));
		ClipLibrary.Add ("turn left", Resources.Load<AudioClip> ("turnleft"));
		ClipLibrary.Add ("turn right", Resources.Load<AudioClip> ("turnright"));
		ClipLibrary.Add ("forward", Resources.Load<AudioClip> ("forward"));
		//landmarks
		ClipLibrary.Add ("trashcan", Resources.Load<AudioClip> ("trashcan"));
		ClipLibrary.Add ("Missy", Resources.Load<AudioClip> ("Missy"));
		ClipLibrary.Add ("crosswalk", Resources.Load<AudioClip> ("crosswalk"));
		ClipLibrary.Add ("streetlamp", Resources.Load<AudioClip> ("streetlamp"));
		ClipLibrary.Add ("streetlight", Resources.Load<AudioClip> ("streetlight"));
		ClipLibrary.Add ("hydrant", Resources.Load<AudioClip> ("hydrant"));
		ClipLibrary.Add ("tree", Resources.Load<AudioClip> ("tree"));

	}

	public Vector3[] Markercollection ()
	{
		// derive position from marker stand ins

		Vector3[] markerVectors = new Vector3[courseLength];
		
		for (int i = 0; i < markerVectors.Length; i++) {
			markerVectors [i] = LoadedMarkers [i].transform.position;
		}
		
		return markerVectors;
	}

	Dictionary<int, Vector3> DesignCourse (Vector3[] vectors)
	{
		//order the positions of the markers for the specified course

		Dictionary<int, Vector3> positions = new Dictionary<int, Vector3> ();
		
		positions.Add (0, vectors [0]);
		positions.Add (1, vectors [1]);
		positions.Add (2, vectors [2]);
		positions.Add (3, vectors [3]);
		positions.Add (4, vectors [4]);
		positions.Add (5, vectors [5]);
		positions.Add (6, vectors [6]);
		
		return positions;
	}

	void MakeCourse (Dictionary<int, Vector3> positions)
	{
		// reference the correct audio files using easy to read labels

		string[] directions = new string[courseLength];
		directions [0] = "forward";
		directions [1] = "turn left";
		directions [2] = "turn right";
		directions [3] = "turn left";
		directions [4] = "stop";
		directions [5] = "turn right";
		directions [6] = "turn left";

		string[] landmarks = new string[courseLength];
		landmarks [0] = "trashcan";
		landmarks [1] = "tree";
		landmarks [2] = "crosswalk";
		landmarks [3] = "streetlight";
		landmarks [4] = "streetlamp";
		landmarks [5] = "hydrant";
		landmarks [6] = "Missy";
		
		foreach (KeyValuePair<int, Vector3> kvp in positions) {
			//make an audio checkpoint 
			GameObject marker = Instantiate (CheckPrefab, kvp.Value, Quaternion.identity) as GameObject; //generate marker prefab in the scene
			MarkersList.Add (marker); //save each created marker
			marker.tag = "marker"; //attach a tag to find it with
			AudioScript = marker.GetComponent<AudioTrigger> (); //cache the script attached to the marker.
			AudioScript.Clip = ClipLibrary [directions [kvp.Key]]; //attach correct directional audio clip to the marker
			AudioScript.LandmarkName = ClipLibrary [landmarks [kvp.Key]]; //attach landmark name audio clip 
			AudioScript.Place = kvp.Key; // give it a number for debugging
			AudioScript.HasPlayed = false; //baseline all flags
			
			if (AudioScript.Place == courseLength - 1) //find the last marker
				AudioScript.Final = true; 
		}
	}

	public void AtFinal ()
	{
		Panels.SetPanel (Panels.EndPanel); //end the game

	}

	public void ResetFlags ()
	{
		// find all the markers and clear relic flags

		Audiotriggers = GameObject.FindGameObjectsWithTag ("marker");
		foreach (var i in Audiotriggers) {
			AudioTrigger script = i.GetComponent<AudioTrigger> ();
			script.ResetPlayedFlag ();
		}
	}


	// very basic course check
	bool ComparePlace ()
	{
		return PreviousMarker - PresentMarker > 1 || PresentMarker < PreviousMarker;
	}
	// very basic navigational check
	int ChartNextMove (int num)
	{
		return ++num;
	}


	public void Restart () //restart the game
	{
		ClearMarkers ();
		ResetFlags ();
	}

	public void ClearMarkers () //remove markers from last game
	{
		foreach (var i in MarkersList) {
			Destroy (i);
		}
	}

	public void InitDistanceTracking () // checks distance from player to next marker
	{
		Markers = GameObject.FindGameObjectsWithTag ("marker"); //find current markers
		if (Markers [0] != null && PreferenceSelections.PrefsSelected) { //if they are present
			// track distance to the next marker
			DistToNext = Vector3.Distance (Player.transform.position, Markers [NextMarker].transform.position); 
		}
	}
}
