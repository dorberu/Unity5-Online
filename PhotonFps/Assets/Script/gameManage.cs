using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon;

public class gameManage : Photon.PunBehaviour {

	// Photon用環境変数
	ExitGames.Client.Photon.Hashtable myPlayerHash;
	ExitGames.Client.Photon.Hashtable myRoomHash;
	private PhotonView scenePV;
	// 敵味方判別
	public int myTeamID;
	private float tagTimer;
	private bool loadOnce;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		myTeamID = 0;
		loadOnce = false;
		scenePV = PhotonView.Get (this.gameObject);
		tagTimer = 0.0f;
		// Photon RealTimeのサーバへ接続（ロビーへ入室）
		PhotonNetwork.ConnectUsingSettings (null);
	}
	
	// Update is called once per frame
	void Update () {
		// ルームに入室が完了していたら
		if (PhotonNetwork.inRoom) {
			if (!loadOnce && myTeamID != 0) {
				loadOnce = true;
				GameObject myPlayer = PhotonNetwork.Instantiate (
					"character/t01",
					new Vector3 (440.0f, 30.0f, -560.0f),
					Quaternion.identity, 0);
				myPlayer.transform.LookAt (Vector3.zero);
				variableManage.myTeamID = myTeamID;
			}
			// 3秒に1回タグ付け
			tagTimer += Time.deltaTime;
			if (tagTimer > 3.0f) {
				giveEnemyFlag ();
				tagTimer = 0.0f;
			}
		}
	}

	public override void OnJoinedLobby () {
		PhotonNetwork.JoinRandomRoom ();
		Debug.Log ("OnJoinedLobby End.");
	}
		
	public override void OnFailedToConnectToPhoton (DisconnectCause cause) {
		Debug.Log ("Failed to Enter Lobby.");
		base.OnFailedToConnectToPhoton (cause);
		SceneManager.LoadScene ("mainMenu");
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
		Debug.Log ("Failed to Enter Room.");
		myRoomHash = new ExitGames.Client.Photon.Hashtable ();
		myRoomHash.Add ("time", 0);
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;
		roomOptions.MaxPlayers = 8;
		roomOptions.CustomRoomProperties = myRoomHash;
		PhotonNetwork.CreateRoom (Random.Range(1.0f, 100.0f).ToString(), roomOptions, null);
		myTeamID = 1;
	}

	public override void OnJoinedRoom () {
		base.OnJoinedRoom ();
		Debug.Log ("OnJoinedRoom End.");
	}

	public override void OnConnectionFail (DisconnectCause cause) {
		Debug.Log ("Disconnected to Photon Realtime.");
		base.OnConnectionFail (cause);
		SceneManager.LoadScene ("mainMenu");
	}

	// ルームに誰か別のプレイヤーが入室したとき
	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
		Debug.Log ("OnPhotonPlayerConnected.");
		if (PhotonNetwork.isMasterClient) {
			// メンバー振り分け処理
			int allocateNumber = 0;
			GameObject[] team1Players = GameObject.FindGameObjectsWithTag ("Player");
			GameObject[] team2Players = GameObject.FindGameObjectsWithTag ("enemy");
			if ((team1Players.Length - 1) >= team2Players.Length) {
				if (myTeamID == 1) {
					allocateNumber = 2;
				} else {
					allocateNumber = 1;
				}
				scenePV.RPC ("allocateTeam", newPlayer, allocateNumber);
			} else {
				if (myTeamID == 2) {
					allocateNumber = 2;
				} else {
					allocateNumber = 1;
				}
				scenePV.RPC ("allocateTeam", newPlayer, allocateNumber);
			}
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			PhotonNetwork.Disconnect ();
		} else {
			SceneManager.LoadScene ("mainMenu");
		}
	}

	// 敵に対してタグ付けを行う
	void giveEnemyFlag () {
		// チームIDが定義されていたら
		if (myTeamID != 0) {
			int enID = 1;
			if (myTeamID == 1) { enID = 2; }
			GameObject[] allFriendPlayer = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in allFriendPlayer) {
				if (player == null || player.GetComponent<characterStatus> () == null) {
					continue;
				}
				int id = player.GetComponent<characterStatus> ().myTeamID;
				if (id == enID) {
					player.tag = "enemy";
				}
			}
		}
	}

	[PunRPC]
	void allocateTeam (int teamID) {
		if (myTeamID == 0) {
			myTeamID = teamID;
		}
	}
}
