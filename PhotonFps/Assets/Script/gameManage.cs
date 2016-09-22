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
	// スタート地点用
	private Vector2 myStartPos;
	// 勝敗情報用
	private int tc1tmp;
	private int tc2tmp;
	private float bc1tmp;
	private float bc2tmp;
	private bool sendOnce;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		myTeamID = 0;
		loadOnce = false;
		scenePV = PhotonView.Get (this.gameObject);
		tagTimer = 0.0f;
		sendOnce = false;
		tc1tmp = variableManage.team1Rest;
		tc2tmp = variableManage.team2Rest;
		bc1tmp = variableManage.base1Rest;
		bc2tmp = variableManage.base2Rest;

		// Photon RealTimeのサーバへ接続（ロビーへ入室）
		PhotonNetwork.ConnectUsingSettings (null);

		// スタート地点計算
		Vector2 rndPos = Vector2.zero;
		while (true) {
			rndPos = Random.insideUnitCircle * 150.0f;
			if (rndPos.x < -20.0f && rndPos.y > 20.0f) {
				break;
			}
		}
		myStartPos = new Vector2 ((592.0f + rndPos.x), (-592.0f + rndPos.y));
	}
	
	// Update is called once per frame
	void Update () {
		// ルームに入室が完了していたら
		if (PhotonNetwork.inRoom) {
			if (!loadOnce && myTeamID != 0) {
				loadOnce = true;
				if (myTeamID == 2) {
					myStartPos = myStartPos * -1.0f;
				}
				GameObject myPlayer = PhotonNetwork.Instantiate (
					"character/t01",
					new Vector3 (myStartPos.x, 24.0f, myStartPos.y),
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

			// 自分がマスタークライアントなら、変更があった際に情報送信
			if (PhotonNetwork.isMasterClient) {
				if ((variableManage.team1Rest != tc1tmp)
					|| (variableManage.team2Rest != tc2tmp)
					|| (variableManage.base1Rest != bc1tmp)
					|| (variableManage.base2Rest != bc2tmp))
				{
					scenePV.RPC ("sendCurrentStatus",
						PhotonTargets.All,
						tc1tmp,
						tc2tmp,
						bc1tmp,
						bc2tmp);
				}
			}
			// 撃破されたとき、マスタークライアントへ情報を送信
			if (variableManage.currentHealth <= 0.0f) {
				if (!sendOnce) {
					sendOnce = true;
					scenePV.RPC ("sendDestruction",
						PhotonNetwork.masterClient,
						variableManage.myTeamID);
				}
			} else {
				sendOnce = false;
			}
			// 勝敗を確定
			if(PhotonNetwork.isMasterClient && !variableManage.finishedGame) {
				if (variableManage.team1Rest <= 0
					|| variableManage.base1Rest <= 0.0f) {
					variableManage.finishedGame = true;
					variableManage.gameResult = 2;
					scenePV.RPC (
						"syncFinished",
						PhotonTargets.Others,
						variableManage.gameResult);
				} else if (variableManage.team2Rest <= 0
					|| variableManage.base2Rest <= 0.0f) {
					variableManage.finishedGame = true;
					variableManage.gameResult = 1;
					scenePV.RPC (
						"syncFinished",
						PhotonTargets.Others,
						variableManage.gameResult
					);
				}
			}
		}
	}

	public override void OnJoinedLobby () {
		PhotonNetwork.JoinRandomRoom ();
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
	}

	public override void OnConnectionFail (DisconnectCause cause) {
		Debug.Log ("Disconnected to Photon Realtime.");
		base.OnConnectionFail (cause);
		SceneManager.LoadScene ("mainMenu");
	}

	// ルームに誰か別のプレイヤーが入室したとき
	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) {
		Debug.Log ("Player Connected");
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
				if (myTeamID == 1) {
					allocateNumber = 1;
				} else {
					allocateNumber = 2;
				}
				scenePV.RPC ("allocateTeam", newPlayer, allocateNumber);
			}
			// 現在の戦況を送信
			scenePV.RPC(
				"sendCurrentStatus",
				newPlayer,
				variableManage.team1Rest,
				variableManage.team2Rest,
				variableManage.base1Rest,
				variableManage.base2Rest
			);
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

	// 自分が撃破されたことを送信する
	[PunRPC]
	void sendDestruction (int tID) {
		if (tID == 1) {
			tc1tmp -= 1;
			if (tc1tmp < 0) {
				tc1tmp = 0;
			}
		} else {
			tc2tmp -= 1;
			if (tc2tmp < 0) {
				tc2tmp = 0;
			}
		}
	}

	// 現在の対戦状況を送信する
	[PunRPC]
	void sendCurrentStatus (int tc1, int tc2, float bc1, float bc2) {
		variableManage.team1Rest = tc1;
		variableManage.team2Rest = tc2;
		variableManage.base1Rest = bc1;
		variableManage.base1Rest = bc2;
	}

	// ゲーム終了を通知する
	[PunRPC]
	void syncFinished (int winID) {
		variableManage.finishedGame = true;
		variableManage.gameResult = winID;
	}
}
