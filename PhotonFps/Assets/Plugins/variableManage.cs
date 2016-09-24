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
	static public int myTeamID;
	static public bool mapEnabled;
	static public GameObject team1baseBullet;
	static public GameObject team2baseBullet;

	// 勝敗用判定
	static public bool finishedGame;	// 勝敗が確定されたか
	static public int team1Rest;			// チーム１の残り撃破数
	static public int team2Rest;			// チーム２の残り撃破数
	static public float base1Rest;	// チーム１の拠点の残りHP
	static public float base2Rest;	// チーム２の拠点の残りHP
	static public float timeRest;		// ゲームの残り時間
	static public int gameResult;			// 勝利チームID

	// 画面表示用変数
	static public int informationMessage;

	// Use this for initialization
	void Start () {
	}
	
	static public void initializeVariable () {
		movingXaxis = 0;
		movingYaxis = 0;
		fireWeapon = false;
		lockoned = false;
		controlLock = false;
		myTeamID = 0;
		mapEnabled = false;
		currentHealth = 10f;
		// 勝敗用
		finishedGame = false;
		team1Rest = 2;
		team2Rest = 2;
		base1Rest = 9999.0f;
		base2Rest = 9999.0f;
		timeRest = 400.0f;
		gameResult = 0;
		informationMessage = 0;
	}
}
