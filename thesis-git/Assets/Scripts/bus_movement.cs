using UnityEngine;
using System.Collections;

public class bus_movement : MonoBehaviour
{		
		public float speed, distance;
		public Vector3 start, finish;

		// Use this for initialization
		void Start ()
		{
				start = gameObject.transform.position;
				distance = Vector3.Distance (start, finish);
		}
		public IEnumerator drive ()
		{
				while (Vector3.Distance (finish, start) > .05f) {
						transform.position = Vector3.Lerp (transform.position, finish, speed * Time.deltaTime);
						
						yield return null;
				}
					
				Destroy (gameObject);
		
		}		
		public void restart ()
		{
				gameObject.transform.position = start;
		}

		void Update ()
		{
				if (Input.GetMouseButtonDown (1))
						StartCoroutine (drive ());
		}
		
}
