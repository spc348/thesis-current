using UnityEngine;
using System.Collections;

public class WalkableTrigger : MonoBehaviour
{
    public Renderer notifier_mesh;

    void Start()
    {
        notifier_mesh = GameObject.FindGameObjectWithTag("notifier").GetComponent<Renderer>();
        notifier_mesh.material.color = Color.green;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "unWalkable")
        {
            cannot_walk();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "unWalkable")
        {
            can_walk();
        }
    }

    public void cannot_walk()
    {
        notifier_mesh.material.color = Color.red;
        PreferenceSelections.PrefsRef.Jaywalked++;
        StartCoroutine(CourseScript.CourseRef.PromptRoutine(2, PanelOrganizer.PanelFade));
    }

    public void can_walk()
    {
        notifier_mesh.material.color = Color.green;
    }
}
