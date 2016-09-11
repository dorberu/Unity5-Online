using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIbattle : MonoBehaviour {
	
	//テキスト格納
	public Text infoText;
	public Text timerText;
	public Text wepCurrentText;
	public Text wepStandbyText;
	public Text redTeamText;
	public Text blueTeamText;
	public Text healthText;
	public Text returnText;
	//オブジェクト格納
	public GameObject returnMenu;
	public GameObject mapUIobj;
	// 仮想操作パッド関連
	private float currentXpos;
	private float currentYpos;
	private float startXpos;
	private float startYpos;
	private bool touchStart;

	void Start () {
		currentXpos = 0.0f;
		currentYpos = 0.0f;
		touchStart = false;
	}
	

	void Update () {
		// 仮想操作パッド
		for (int i = 0; i < Input.touchCount; i++) {
			// 画面の左下に指があるか判定
			if (Input.GetTouch(i).position.x < (Screen.width / 2.5f)
				&& Input.GetTouch(i).position.y < (Screen.height / 2.0f)) {
				currentXpos = Input.GetTouch (i).position.x;
				currentYpos = Input.GetTouch (i).position.y;
				if (!touchStart) {
					startXpos = currentXpos;
					startYpos = currentYpos;
					touchStart = true;
				}
			}
		}

		// 画面に触れていない時は初期化
		if (Input.touchCount == 0) {
			currentXpos = 0.0f;
			currentYpos = 0.0f;
			startXpos = 0.0f;
			startYpos = 0.0f;
			touchStart = false;
		}

		// モバイル時のみ動作
		if (Application.isMobilePlatform) {
			if ((startXpos - currentXpos) < (Screen.width * -0.05f)) {
				variableManage.movingXaxis = -1;
			} else if ((startXpos - currentXpos) > (Screen.width * 0.05f)) {
				variableManage.movingXaxis = 1;
			} else {
				variableManage.movingXaxis = 0;
			}

			if ((startYpos - currentYpos) < (Screen.height * -0.08f)) {
				variableManage.movingYaxis = -1;
			} else if ((startYpos - currentYpos) > (Screen.height * 0.08f)) {
				variableManage.movingYaxis = 1;
			} else {
				variableManage.movingYaxis = 0;
			}
		}

		// Debug
		// infoText.text = "X : " + variableManage.movingXaxis + "  Y : " + variableManage.movingYaxis;
	}

	//コンフィグ表示用ボタン
	public void configToggle(){
		if(returnMenu.GetActive()){
			returnMenu.SetActive(false);
		}else{
			returnMenu.SetActive(true);
		}
	}

	//メインメニューへ戻る
	public void returnMainMenu(){
		Application.LoadLevel("mainMenu");
	}

}
