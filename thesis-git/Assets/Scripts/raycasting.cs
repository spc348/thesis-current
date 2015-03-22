using UnityEngine;
using System.Collections;

public class raycasting : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 right = transform.TransformDirection(Vector3.right);
        if(Physics.Raycast(transform.position, right, 100))
        {
            print("on target");
        }
        Debug.DrawRay(transform.position, right* 100, Color.red);
	}
}
