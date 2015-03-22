using UnityEngine;
using System.Collections;

public class ScreenshotBehavior : MonoBehaviour
{
		int i = 0;

		void Update ()
		{
				if (Input.GetButtonDown ("Fire1")) {
						Debug.Log ("snapshot");
						Application.CaptureScreenshot ("Screenshot" + i + ".png", 4);
						i++;
				}
		}
}
