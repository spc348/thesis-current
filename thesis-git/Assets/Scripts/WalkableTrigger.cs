using UnityEngine;

public class WalkableTrigger : MonoBehaviour
{
    public Renderer notifier_mesh;

    private void Start()
    {
        notifier_mesh = GameObject.FindGameObjectWithTag("notifier").GetComponent<Renderer>();
        notifier_mesh.material.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "unWalkable")
        {
            cannot_walk();
            StartCoroutine(CourseScript.CourseRef.PromptRoutine(2, PanelOrganizer.PanelFade));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "unWalkable")
        {
            can_walk();
        }
    }

    public void cannot_walk()
    {
        notifier_mesh.material.color = Color.red;
    }

    public void can_walk()
    {
        notifier_mesh.material.color = Color.green;
    }
}