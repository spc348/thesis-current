using UnityEngine;
using System.Collections;

public class Crossing : MonoBehaviour
{
    public Light_Script[] lights;
    public Crossing_trigger[] crossings;
    public GameObject crossing_a, crossing_b, crossing_c, crossing_d;
	public Light_Script ls_a, ls_b, ls_c, ls_d, ls_e, ls_f, ls_g, ls_h;
	public float CrossingTime;
    Crossing_trigger ct_a, ct_b, ct_c, ct_d;
	bool can_cross_1,can_cross_2,can_cross_3,can_cross_4;

    // Use this for initialization
    void Start()
    {
		can_cross_1 = false;
		can_cross_2 = true;
		can_cross_3 = false;
		can_cross_4 = true;

        lights = new Light_Script[8];
        lights[0] = ls_a;
        lights[1] = ls_b;
        lights[2] = ls_c;
        lights[3] = ls_d;
		lights[4] = ls_e;
		lights[5] = ls_f;
		lights[6] = ls_g;
		lights[7] = ls_h;

        crossings = new Crossing_trigger[4];
        ct_a = crossing_a.GetComponent<Crossing_trigger>();
        crossings[0] = ct_a;
        ct_b = crossing_b.GetComponent<Crossing_trigger>();
        crossings[1] = ct_b;
        ct_c = crossing_c.GetComponent<Crossing_trigger>();
        crossings[2] = ct_c;
        ct_d = crossing_d.GetComponent<Crossing_trigger>();
        crossings[3] = ct_d;

		InvokeRepeating("TimeToCross",CrossingTime,CrossingTime);

    }

    void Update()
    {

		if(can_cross_1)
		{
			activate_light(2);
			activate_light(3);
			ct_a.can_walk = true;
		} else{
			deactivite_light(2);
			deactivite_light(3);
			ct_a.can_walk = false;
		}

		if(can_cross_2)
		{
			activate_light(1);
			activate_light(4);
			ct_b.can_walk = true;
		} else{
			deactivite_light(1);
			deactivite_light(4);
			ct_b.can_walk = false;
		}

		if(can_cross_3)
		{
			activate_light(0);
			activate_light(7);
			ct_c.can_walk = true;
		} else{
			deactivite_light(0);
			deactivite_light(7);
			ct_c.can_walk = false;
		}

		if(can_cross_4)
		{
			activate_light(5);
			activate_light(6);
			ct_d.can_walk = true;
		} else{
			deactivite_light(5);
			deactivite_light(6);
			ct_d.can_walk = false;
		}

    }

	void TimeToCross()
	{
		
		can_cross_1 = !can_cross_1;
		can_cross_3 = !can_cross_3;
		can_cross_2 = !can_cross_2;
		can_cross_4 = !can_cross_4;



	}
    
	public void activate_light(int light_num)
    {
        lights[light_num].light_on();
    }
    
	public void deactivite_light(int light_num)
    {
        lights[light_num].light_off();
    }

}
