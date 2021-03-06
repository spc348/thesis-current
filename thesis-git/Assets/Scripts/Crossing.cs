﻿using UnityEngine;
using System.Collections;

public class Crossing : MonoBehaviour
{
	public LightScript[] Lights;
	public Crossing_trigger[] Crossings;
	public GameObject CrossingA, CrossingB, CrossingC, CrossingD;
	public LightScript LsA, LsB, LsC, LsD, LsE, LsF, LsG, LsH;
	Crossing_trigger ct_a, ct_b, ct_c, ct_d;
	bool can_cross_1, can_cross_2, can_cross_3, can_cross_4;

	public static float CrossingTimeLocal;

	public static Crossing CrossingRef;

	public static float CrossingTime {
		get {
			return CrossingTimeLocal;
		}
		set {
			CrossingTimeLocal = value;
		}
	}

	void Awake ()
	{
		CrossingRef = this;
	}

	// Use this for initialization
	void Start ()
	{
		CrossingTimeLocal = 8;

		can_cross_1 = false;
		can_cross_2 = false;
		can_cross_3 = true;
		can_cross_4 = true;

		Lights = new LightScript[8];
		Lights [0] = LsA;
		Lights [1] = LsB;
		Lights [2] = LsC;
		Lights [3] = LsD;
		Lights [4] = LsE;
		Lights [5] = LsF;
		Lights [6] = LsG;
		Lights [7] = LsH;

		Crossings = new Crossing_trigger[4];
		ct_a = CrossingA.GetComponent<Crossing_trigger> ();
		Crossings [0] = ct_a;
		ct_b = CrossingB.GetComponent<Crossing_trigger> ();
		Crossings [1] = ct_b;
		ct_c = CrossingC.GetComponent<Crossing_trigger> ();
		Crossings [2] = ct_c;
		ct_d = CrossingD.GetComponent<Crossing_trigger> ();
		Crossings [3] = ct_d;

		InvokeRepeating ("TimeToCross", CrossingTimeLocal, CrossingTimeLocal);

	}

	void Update ()
	{

		if (can_cross_1) {
			ActivateLight (2);
			ActivateLight (3);
			ct_a.can_walk = true;
		} else {
			DeactiviteLight (2);
			DeactiviteLight (3);
			ct_a.can_walk = false;
		}

		if (can_cross_2) {
			ActivateLight (1);
			ActivateLight (4);
			ct_b.can_walk = true;
		} else {
			DeactiviteLight (1);
			DeactiviteLight (4);
			ct_b.can_walk = false;
		}

		if (can_cross_3) {
			ActivateLight (0);
			ActivateLight (7);
			ct_c.can_walk = true;
		} else {
			DeactiviteLight (0);
			DeactiviteLight (7);
			ct_c.can_walk = false;
		}

		if (can_cross_4) {
			ActivateLight (5);
			ActivateLight (6);
			ct_d.can_walk = true;
		} else {
			DeactiviteLight (5);
			DeactiviteLight (6);
			ct_d.can_walk = false;
		}

	}

	void TimeToCross ()
	{
		
		can_cross_1 = !can_cross_1;
		can_cross_3 = !can_cross_3;
		can_cross_2 = !can_cross_2;
		can_cross_4 = !can_cross_4;
	}
    
	public void ActivateLight (int lightNum)
	{
		Lights [lightNum].LightOn ();
	}
    
	public void DeactiviteLight (int lightNum)
	{
		Lights [lightNum].LightOff ();
	}

}
