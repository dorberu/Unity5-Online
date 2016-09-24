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
	static public GameObject team1baseBullet;
	static public GameObject team2baseBullet;

	// 勝敗用判定
	static public bool finishedGame = false;	// 勝敗が確定されたか
	static public int team1Rest = 2;			// チーム１の残り撃破数
	static public int team2Rest = 2;			// チーム２の残り撃破数
	static public float base1Rest = 9999.0f;	// チーム１の拠点の残りHP
	static public float base2Rest = 9999.0f;	// チーム２の拠点の残りHP
	static public float timeRest = 400.0f;		// ゲームの残り時間
	static public int gameResult = 0;			// 勝利チームID

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
		// 勝敗用
		finishedGame = false;
		team1Rest = 2;
		team2Rest = 2;
		base1Rest = 9999.0f;
		base2Rest = 9999.0f;
		gameResult = 0;
	}
}
