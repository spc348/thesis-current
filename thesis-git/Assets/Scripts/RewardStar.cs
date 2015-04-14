using UnityEngine;
using System.Collections;

public class RewardStar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 76f * Time.deltaTime);
        transform.Translate(Random.Range(0, 10f) * 10f * Time.deltaTime, Random.Range(0, 10f) * 10f * Time.deltaTime,0);
	}
}
