using UnityEngine;
using System.Collections;

public class variableManage : MonoBehaviour {

	static public int movingXaxis;
	static public int movingYaxis;
	static public bool fireWeapon;
	static public GameObject lockonTarget;
	static public bool lockoned;
	static public float currentHealth;
	static public bool controlLock;
	static public int myTeamID = 0;
	static public bool mapEnabled;

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
		mapEnabled = false;
		currentHealth = 10f;
	}
}
