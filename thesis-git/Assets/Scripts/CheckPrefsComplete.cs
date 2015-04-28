using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckPrefsComplete : MonoBehaviour
{
	public Text Text;
	Button button;

	void Start ()
	{
		button = gameObject.GetComponent<Button> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (PreferenceSelections.PrefsSelected) {
			button.interactable = true;
			Text.text = "Press play to continue";
		} else {
			button.interactable = false;
			Text.text = "Please address all settings";
		}
	}
}
