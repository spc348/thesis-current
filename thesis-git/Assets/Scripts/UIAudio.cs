using UnityEngine;
using System.Collections;

public class UIAudio : MonoBehaviour
{

		private AudioSource source;
		public AudioClip click1, click2, rollover1, rollover2, switch2, switch3;
		public AudioClip[] clips;


		void Start ()
		{

				source = GetComponent<AudioSource> ();
				clips = new AudioClip[6];

				clips [0] = click1;
				clips [1] = click2;
				clips [2] = rollover1;
				clips [3] = rollover2;
				clips [4] = switch2;
				clips [5] = switch3;
		}

		public void PlayAudio (int clip)
		{
				source.PlayOneShot (clips [clip]);
		}
}
