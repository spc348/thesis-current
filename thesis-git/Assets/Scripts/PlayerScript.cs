using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static Vector3 PlayerStartPosition;
    public static Quaternion PlayerStartRotation;
    public static PlayerScript PlayerRef;
    private Rigidbody rigidbody;
    private float touchSpeed, touchRot;
    
    public static float Speed { get; set; }
    public static float RotSpeed { get; set; }

    private void Awake()
    {
        PlayerRef = this;
    }

    private void Start()
    {
        PlayerStartPosition = transform.position;
        PlayerStartRotation = transform.rotation;
        Speed = 6f;
        RotSpeed = 72f;
        touchSpeed = Speed/15f;
        touchRot = RotSpeed/15f;
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        transform.position = PlayerStartPosition;
        transform.rotation = PlayerStartRotation;
    }

    private void FixedUpdate()
    {
        if (PreferenceSelections.PrefsSelected )
        {
            float forwardMovement = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            float turnMovement = Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime;

            transform.Translate(Vector3.forward * forwardMovement);
            transform.Translate(Vector3.right * turnMovement);

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up*-RotSpeed*Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up*RotSpeed*Time.deltaTime);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Translate(0, 0, touchDeltaPosition.y*touchSpeed*Time.fixedDeltaTime);
                transform.Rotate(0, touchDeltaPosition.x*touchRot*Time.fixedDeltaTime, 0);
            }

        }
    }
}