using UnityEngine;
using System.Collections;

public class Light_Script : MonoBehaviour
{
    public Renderer lRend;
    public bool can_walk;

    // Use this for initialization
    void Start()
    {
        lRend = gameObject.GetComponent<Renderer>();
        lRend.material.color = Color.white;
    }
    public void light_on()
    {
        gameObject.GetComponent<Light>().color = Color.white;
        lRend.material.color = Color.white;
    }
    public void light_off()
    {
        gameObject.GetComponent<Light>().color = Color.red;
        lRend.material.color = Color.red;
    }
}
