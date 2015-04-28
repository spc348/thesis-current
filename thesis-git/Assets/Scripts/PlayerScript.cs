using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

	static float speed;
	static float rotSpeed;
    float touchSpeed;
    float touchRot;
	public static Vector3 PlayerStartPosition;
	public static Quaternion PlayerStartRotation;

	public static PlayerScript PlayerRef;
	
	public static float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}
	
	public static float RotSpeed {
		get {
			return rotSpeed;
		}
		set {
			rotSpeed = value;
		}
	}

	void Awake ()
	{
		PlayerRef = this;
	}

	void Start ()
	{
		PlayerStartPosition = transform.position;
		PlayerStartRotation = transform.rotation;
		speed = 42f;
		rotSpeed = 72f;
        touchSpeed = speed / 15f;
        touchRot = RotSpeed / 15f;
	}


	public void Reset ()
	{
		transform.position = PlayerStartPosition;
		transform.rotation = PlayerStartRotation;
	}
		
	void FixedUpdate ()
	{
		if (PreferenceSelections.PrefsSelected) {
			Move ();
		}
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
			transform.Rotate (Vector3.up * rotSpeed* Time.deltaTime);
		}
        
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
			transform.Translate (0, 0, touchDeltaPosition.y * touchSpeed * Time.fixedDeltaTime);
			transform.Rotate (0, touchDeltaPosition.x * touchRot * Time.fixedDeltaTime, 0);
		}
	}
}