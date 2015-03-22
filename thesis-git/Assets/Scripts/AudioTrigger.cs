using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    public CourseScript script;
    public PlayerScript movement;
    public GameObject player;
    public CheckpointScript scoreboard;
    public AudioClip clip;
    private AudioSource source;
    public bool hasPlayed, final;
    public int audio_routine_length;
    public bool atCheckPoint;
    public PanelOrganizer panels;
    public int place;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerScript>();
        panels = GameObject.FindGameObjectWithTag("PanelOrganizer").GetComponent<PanelOrganizer>();
        script = GameObject.FindGameObjectWithTag("CoursePlanner").GetComponent<CourseScript>();
        source = GetComponent<AudioSource>();
        hasPlayed = false;
    }

    void Start()
    {
        atCheckPoint = false;
        audio_routine_length = 2;
    }

    IEnumerator playAudioRoutine(AudioClip clip)
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(clip);

            yield return new WaitForSeconds(clip.length + audio_routine_length);

            movement.enabled = !movement.enabled;
        }
    }

    public void reset_played_flag()
    {
        hasPlayed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !source.isPlaying && other.gameObject.activeSelf)
        {

            Debug.Log(place);
            atCheckPoint = true;
            script.atCheckPoint = true;
            movement.enabled = !movement.enabled;
            StartCoroutine(playAudioRoutine(clip));

            script.currentMarker = place;


            panels.init_game_subpanel(place);
            hasPlayed = true;

            if (final)
            {
                panels.atFinal();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            script.atCheckPoint = false;
            panels.clear_subpanels();
        }
        Debug.Log(place + " " + System.DateTime.Now.ToLongTimeString());
    }
}
