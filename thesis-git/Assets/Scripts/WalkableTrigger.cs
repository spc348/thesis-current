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
            //Debug.Log("offcourse but color change isnt working");
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "unWalkable")
        {
            cannot_walk();
            //Debug.Log("offcourse but color change isnt working");
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

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "unWalkable")
        {
            can_walk();
            //Debug.Log("on course but color change isnt working");
        }
    }
}
