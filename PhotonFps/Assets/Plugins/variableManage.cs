﻿using UnityEngine;
using System.Collections;

public class variableManage : MonoBehaviour {

	static public int movingXaxis;
	static public int movingYaxis;
	static public bool fireWeapon;
	static public GameObject lockonTarget;
	static public bool lockoned;
	static public float currentHealth;
	static public bool controlLock;

	// Use this for initialization
	void Start () {
		initializeVariable ();
	}
	
	static public void initializeVariable () {
		movingXaxis = 0;
		movingYaxis = 0;
		fireWeapon = false;
		lockoned = false;
		controlLock = false;
		currentHealth = 10f;
	}
}
