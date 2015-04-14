using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour
{
	public Renderer lRend;
	public bool can_walk;

	// Use this for initialization
	void Start ()
	{
		lRend = gameObject.GetComponent<Renderer> ();
		lRend.material.color = Color.white;
	}
	public void LightOn ()
	{
		gameObject.GetComponent<Light> ().color = Color.white;
		lRend.material.color = Color.white;
	}
	public void LightOff ()
	{
		gameObject.GetComponent<Light> ().color = Color.red;
		lRend.material.color = Color.red;
	}
}
