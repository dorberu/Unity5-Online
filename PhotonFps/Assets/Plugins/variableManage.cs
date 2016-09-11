using UnityEngine;
using System.Collections;

public class variableManage : MonoBehaviour {

	static public int movingXaxis;
	static public int movingYaxis;
	static public bool fireWeapon;

	// Use this for initialization
	void Start () {
		initializeVariable ();
	}
	
	static public void initializeVariable () {
		movingXaxis = 0;
		movingYaxis = 0;
		fireWeapon = false;
	}
}
