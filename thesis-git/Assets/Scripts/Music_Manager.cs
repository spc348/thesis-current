using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Music_Manager : MonoBehaviour
{

    public AudioMixer mixer;
    public AudioMixerSnapshot Voice, Ambient;
    public static Music_Manager MusicRef;
    private AudioMixerSnapshot[] snapshots;
    private float[] weights;
    public float TransitionTime;

    void Awake()
    {
        MusicRef = this;
    }

	// Use this for initialization
	void Start ()
	{
        snapshots = new AudioMixerSnapshot[2];
	    snapshots[0] = Ambient;
	    snapshots[1] = Voice;

        weights = new float[2];
	    weights[0] = .3f;
	    weights[1] = .3f;

	    TransitionTime = .5f;

        mixer.TransitionToSnapshots(snapshots, weights, TransitionTime);

        SwitchToAmbient();
	}

    public void SwitchToVoice()
    {
        Voice.TransitionTo(.05f);
    }

    public void SwitchToAmbient()
    {
        Ambient.TransitionTo(.05f);
    }
}
