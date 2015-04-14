using UnityEngine;
using System.Collections;

public class UIAudio : MonoBehaviour
{

		private AudioSource source;
		public AudioClip[] clips;


		void Start ()
		{

				source = GetComponent<AudioSource> ();
				
                clips = new AudioClip[9];

                clips[0] = Resources.Load<AudioClip>("electric_alert");
                clips[1] = Resources.Load<AudioClip>("pistachio1_click");
                clips[2] = Resources.Load<AudioClip>("pop_mouse_over");
                clips[3] = Resources.Load<AudioClip>("short_whoosh");
                clips[4] = Resources.Load<AudioClip>("wobble_alert");
                clips[5] = Resources.Load<AudioClip>("woody_click");
                clips[6] = Resources.Load<AudioClip>("xylophone_affirm1");
                clips[7] = Resources.Load<AudioClip>("short_whoosh1");
                clips[8] = Resources.Load<AudioClip>("bass_deny3");
            
            
            
		}

		public void PlayAudio (int clip)
		{
				source.PlayOneShot (clips [clip]);
		}
}
