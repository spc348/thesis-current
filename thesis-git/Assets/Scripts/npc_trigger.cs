using UnityEngine;
using System.Collections;

public class npc_trigger : MonoBehaviour {

    public npc_manager npcm;
    GameObject npc;
    public int speed;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            npc = Instantiate(npcm.npcs[Random.Range(0, 2)], transform.position, Quaternion.identity) as GameObject;
            npc.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            npc.AddComponent<SphereCollider>();
            SphereCollider sc = npc.GetComponent<SphereCollider>();
            sc.radius = 3;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PreferenceSelections.InstanceData.CollidePedestrian += 1;
        }
    }

}
