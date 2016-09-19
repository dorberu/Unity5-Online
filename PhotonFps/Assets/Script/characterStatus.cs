using UnityEngine;
using System.Collections;

public class characterStatus : MonoBehaviour {

	public int myTeamID;
	private PhotonView myPV;
	private float idSendTimer;
	// レーダー用
	public GameObject rdSphere;
	public GameObject rdCube;
	private GameObject[] rdEnTemp;
	private GameObject[] rdFrTemp;
	private float playerRefreshTimer;
	private float distRefreshTimer;

	// Use this for initialization
	void Start () {
		myTeamID = 0;
		myPV = PhotonView.Get (this.gameObject);
		idSendTimer = 0.0f;
		playerRefreshTimer = 0.0f;
		distRefreshTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (myPV.isMine) {
			idSendTimer += Time.deltaTime;
			if (idSendTimer > 3.0f) {
				myTeamID = variableManage.myTeamID;
				myPV.RPC ("syncMyID", PhotonTargets.Others, myTeamID);
				idSendTimer = 0.0f;
			}
		}
		// 味方のレーダー範囲を表示
		if (variableManage.myTeamID == myTeamID) {
			if (variableManage.mapEnabled && myTeamID != 0) {
				rdSphere.SetActive (true);
			} else {
				rdSphere.SetActive (false);
			}
		} else {
			rdSphere.SetActive (false);
		}
		// 敵をレーダーに表示
		if (myPV.isMine) {
			// 敵味方一覧を3.5秒間隔でリフレッシュ
			playerRefreshTimer += Time.deltaTime;
			if (playerRefreshTimer > 3.5f) {
				playerRefreshTimer = 0.0f;
				rdFrTemp = GameObject.FindGameObjectsWithTag ("Player");
				rdEnTemp = GameObject.FindGameObjectsWithTag ("enemy");
			}
			// 前味方のレーダー範囲に敵がいるか2秒間隔で計算
			distRefreshTimer += Time.deltaTime;
			if (distRefreshTimer > 2.0f && rdFrTemp != null && rdEnTemp != null) {
				distRefreshTimer = 0.0f;
				// 全プレイヤーを取り出す
				foreach (GameObject player in rdFrTemp) {
					// 全敵を取り出す
					foreach (GameObject enemy in rdEnTemp) {
						// 総当たりで距離を算出し、レーダー範囲であればオブジェクトをONにする
						float dist = Vector3.Distance (player.transform.position, enemy.transform.position);
						if (dist < 180.0f && variableManage.mapEnabled) {
							enemy.GetComponent<characterStatus> ().rdCube.SetActive (true);
						} else {
							enemy.GetComponent<characterStatus> ().rdCube.SetActive (false);
						}
					}
				}
			}
		}
		// マップ表示画面でなければレーダー用オブジェクトは強制OFF
		if (variableManage.mapEnabled) {
			rdCube.SetActive (true);
		} else {
			rdCube.SetActive (false);
		}
	}

	[PunRPC]
	void syncMyID (int myID) {
		myTeamID = myID;
	}
}
