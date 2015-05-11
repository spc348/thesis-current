using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ambient_Music : MonoBehaviour {

    List<AudioClip> tracks = new List<AudioClip>();
    int randMin = 0, randMax;
    AudioSource source;
    float Volume = .4f;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.volume = Volume;
        LoadTracks();
        randMax = tracks.Count;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.GameOn)
            PlayAmbient();
        else
        {
            source.Stop();
        }
	}

    void PlayAmbient()
    {
        if(!source.isPlaying)
        {
            source.clip = tracks[Random.Range(randMin, randMax)];
            source.Play();
        }

    }

    void LoadTracks()
    {
        tracks.Add(Resources.Load<AudioClip>("amb1"));
        tracks.Add(Resources.Load<AudioClip>("amb2"));
        tracks.Add(Resources.Load<AudioClip>("amb3"));
    }
}
