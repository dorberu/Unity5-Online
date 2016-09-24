using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UImainMenu : MonoBehaviour {

	//テキスト格納
	public Text playerStatusText;
	public Text battleStartBtn;
	public Text unlockText;
	public Text unlockBtn;
	public Text lvupNum;
	//オブジェクト関連
	public GameObject unlockBtnObj;
	public GameObject lvupObj;
	// レベルアップメッセージ用
	private float mesTimer;

	void Start () {
		mesTimer = 0.0f;
	}
	

	void Update () {
		// 画面表示
		lvupNum.text = variableManage.currentLv.ToString ();
		playerStatusText.text = "PlayerClass : " + variableManage.currentLv +
			" NextClass : " + variableManage.currentExp +
			" / " + variableManage.nextExp;
		// レベルアップメッセージ
		if (variableManage.showLvupMes) {
			if (mesTimer == 0.0f) {
				lvupObj.SetActive (true);
			} else if (mesTimer > 3.0f) {
				mesTimer = 0.0f;
				variableManage.showLvupMes = false;
				lvupObj.SetActive (false);
			}
			mesTimer += Time.deltaTime;
		}
	}

	public void jumpBattleScene(){
		variableManage.initializeVariable ();
		SceneManager.LoadScene ("battle");
	}

}
