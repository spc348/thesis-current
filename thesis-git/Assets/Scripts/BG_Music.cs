using System.Collections.Generic;
using UnityEngine;

public class BG_Music : MonoBehaviour
{
    public static BG_Music musicRef;
    static AudioSource source;
    private List<AudioClip> happy, neutral;
    public static bool isHappy;
    public int randMin, randMax;
    public float Volume;

    private void Awake()
    {
        musicRef = this;
    }

    // Use this for initialization
    private void Start()
    {
        source = GetComponent<AudioSource>();
        happy = new List<AudioClip>();
        neutral = new List<AudioClip>();
        LoadMusic();
        Volume = .5f;
        source.volume = Volume;
        randMax = happy.Count;
        randMin = 0;
        isHappy = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.GameOn)
        {
            if (isHappy)
                PlayHappy();
            else
                PlayNeutral();
        }
        else
        {
            source.Stop();
        }
    }

    public static void ToggleHappy()
    {
        isHappy = true;
        source.Stop();
    }

    public static void ToggleNeutral()
    {
        isHappy = false;
        source.Stop();
    }

    public void PlayHappy()
    {
        if (!source.isPlaying)
        {
            source.clip = happy[Random.Range(randMin, randMax)];
            source.Play();
        }
    }

    public void PlayNeutral()
    {
        if (!source.isPlaying)
        {
            source.clip = neutral[Random.Range(randMin, randMax)];
            source.Play();
        }
    }

    private void ChangeSpeed(float speed)
    {
        if (speed > 3) speed = 3;

        if (speed < -3) speed = -3;

        source.pitch = speed;
    }

    private void LoadMusic()
    {
        happy.Add(Resources.Load<AudioClip>("frere"));
        happy.Add(Resources.Load<AudioClip>("shosta"));
        happy.Add(Resources.Load<AudioClip>("twinkle"));
        happy.Add(Resources.Load<AudioClip>("brahms"));
        happy.Add(Resources.Load<AudioClip>("oldman"));
        neutral.Add(Resources.Load<AudioClip>("gentle"));
        neutral.Add(Resources.Load<AudioClip>("vio1"));
        neutral.Add(Resources.Load<AudioClip>("vio2"));
        neutral.Add(Resources.Load<AudioClip>("vio3"));
        neutral.Add(Resources.Load<AudioClip>("vio4"));
    }
}