using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

		public float speed;
		public float rotSpeed;
		public Vector3 playerStartPosition;
		public Quaternion playerStartRotation;
		
		void Start ()
		{
				playerStartPosition = transform.position;
				playerStartRotation = transform.rotation;
		}

		public void reset ()
		{
				transform.position = playerStartPosition;
				transform.rotation = playerStartRotation;
		}
		
		void FixedUpdate ()
		{
				Move ();
		}

		void Move ()
		{
				float forwardMovement = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
				float turnMovement = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;

				transform.Translate (Vector3.forward * forwardMovement);
				transform.Translate (Vector3.right * turnMovement);

				if (Input.GetKey (KeyCode.Q)) {
						transform.Rotate (Vector3.up * -rotSpeed * Time.deltaTime);
				}

				if (Input.GetKey (KeyCode.E)) {
						transform.Rotate (Vector3.up * rotSpeed * Time.deltaTime);
				}
        
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
						Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
						transform.Translate (0, 0, touchDeltaPosition.y * speed * Time.deltaTime);
						transform.Rotate (0, touchDeltaPosition.x * rotSpeed * Time.deltaTime, 0);
				}

		}
}