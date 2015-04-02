using System.Collections;
using UnityEngine;

public class PreferenceSelections : MonoBehaviour
{
	PlayerScript playerScript;
	AudioTrigger audioTrigger;
	PanelOrganizer panelOrganizer;
	CourseScript courseScript;

	public PlayerAttributes _PlayerInstance;

	public static bool PrefsSelected;

	void Start ()
	{
		PrefsSelected = false;
	}
	public void SetCourseLength (int length)
	{
		CourseScript.CourseLength = length;
	}
	public void SetUseTextBoolean (bool decision)
	{
		PanelOrganizer.UseTextInstruction = decision;
	}

	public void SetRepeatPanelLength (float value)
	{
		AudioTrigger.RepeatPanelTime = value;
	}

	public void SetAudioLength (float value)
	{
		AudioTrigger.AudioRoutineLength = value;
	}

	public void SetPlayerSpeed (float value)
	{
		PlayerScript.Speed = value;
	}

	public void SetPlayerRotSpeed (float value)
	{
		PlayerScript.RotSpeed = value;
	}

	public void MakePlayerInstace ()
	{
		//check for user input
		if (AudioTrigger.AudioRoutineLength > 0) {
			if (AudioTrigger.RepeatPanelTime > 0) {
				if (PlayerScript.Speed > 0) {
					if (PlayerScript.RotSpeed > 0) {
						if (CourseScript.CourseLength > 0) {
							_PlayerInstance = new PlayerAttributes (AudioTrigger.RepeatPanelTime, AudioTrigger.AudioRoutineLength,
							                             PlayerScript.Speed, PlayerScript.RotSpeed, CourseScript.CourseLength,
							                             PanelOrganizer.UseTextInstruction);
							PrefsSelected = true;
						} else { //load defaults
							CourseScript.CourseLength = 7;
						}
					} else {
						PlayerScript.RotSpeed = 50f;
					}
				} else {
					PlayerScript.Speed = 35f;
				}
			} else {
				AudioTrigger.RepeatPanelTime = 7f;
			}
		} else { 

			AudioTrigger.AudioRoutineLength = 2f;

			PanelOrganizer.UseTextInstruction = true;

			_PlayerInstance = new PlayerAttributes (AudioTrigger.RepeatPanelTime, AudioTrigger.AudioRoutineLength,
		                            PlayerScript.Speed, PlayerScript.RotSpeed, CourseScript.CourseLength,
		                            PanelOrganizer.UseTextInstruction);

			PrefsSelected = true;
		}
	}

	public class PlayerAttributes
	{

		public float ShowPanelTime;
		public float PlayAudioTime;
		public float PlayerSpeed;
		public float PlayerRotationSpeed;
		public bool UseTextInstruction;
		public int CourseLength;

		public PlayerAttributes (float showPanelTime, float playAudioTime, float playerSpeed, 
		                       float playerRotationSpeed, int courseLength, 
		                       bool useTextInstruction)
		{
			this.ShowPanelTime = showPanelTime;
			this.PlayAudioTime = playAudioTime;
			this.PlayerSpeed = playerSpeed;
			this.PlayerRotationSpeed = playerRotationSpeed;
			this.CourseLength = courseLength;
			this.UseTextInstruction = useTextInstruction;
		}
		
	}
}
