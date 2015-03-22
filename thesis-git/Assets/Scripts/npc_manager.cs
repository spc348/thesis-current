using UnityEngine;
using System.Collections;

public class npc_manager : MonoBehaviour {

    public GameObject[] npcs;
    public GameObject npc1, npc2, npc3;

	// Use this for initialization
	void Start () {
        npcs = new GameObject[3];
        npcs[0] = npc1;
        npcs[1] = npc2;
        npcs[2] = npc3;
    }
	
	// Update is called once per frame
	void Update () {
	
	}



}