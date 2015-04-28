using UnityEngine;
using System.Collections;

public class npc_trigger : MonoBehaviour {

    public npc_manager npcm;
    GameObject npc;
    public int Speed = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            npc = Instantiate(npcm.npcs[Random.Range(0, 2)], transform.position, Quaternion.identity) as GameObject;
            npc.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            npc.AddComponent<SphereCollider>();
            SphereCollider sc = npc.GetComponent<SphereCollider>();
            sc.isTrigger = true;
            sc.radius = 3;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PreferenceSelections.InstanceData.CollidePedestrian += 1;
            StartCoroutine(CourseScript.CourseRef.PromptRoutine(3, PanelOrganizer.PanelFade));
            Destroy(npc);
        }
    }

}
