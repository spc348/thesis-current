using UnityEngine;

public class Crossing_trigger : MonoBehaviour
{
    public bool can_walk;
    public WalkableTrigger notifier;

    private void Start()
    {
        notifier = GameObject.FindGameObjectWithTag("body").GetComponent<WalkableTrigger>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            if (can_walk)
                notifier.can_walk();
            else
            {
                notifier.cannot_walk();
                StartCoroutine(CourseScript.CourseRef.PromptRoutine(prompt: 4, duration: PanelOrganizer.PanelFade));
            }
    }
}