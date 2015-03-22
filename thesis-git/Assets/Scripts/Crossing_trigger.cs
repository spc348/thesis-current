using UnityEngine;
using System.Collections;

public class Crossing_trigger : MonoBehaviour
{
    public bool can_walk;
    public WalkableTrigger notifier;

    void Start()
    {
        notifier = GameObject.FindGameObjectWithTag("body").GetComponent<WalkableTrigger>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (can_walk)
            {
                notifier.can_walk();
            }
            else
            {
                notifier.cannot_walk();
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (can_walk)
            {
                notifier.can_walk();
            }
            else
            {
                notifier.cannot_walk();
            }
        }
    }




}
