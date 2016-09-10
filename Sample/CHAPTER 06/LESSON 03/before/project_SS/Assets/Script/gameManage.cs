using UnityEngine;
using System.Collections;

public class gameManage : MonoBehaviour {

	//Photon用変数定義
	ExitGames.Client.Photon.Hashtable myPlayerHash;
	ExitGames.Client.Photon.Hashtable myRoomHash;
	string[] roomProps = { "time"};
	private PhotonView scenePV;
	//敵味方判別
	public int myTeamID;
	private float tagTimer;
	//スタート地点用
	private Vector2 myStartPos;
	//ほか
	private bool loadOnce;

	void Start () {
		//初期設定
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		myTeamID = 0;
		loadOnce = false;
		scenePV = PhotonView.Get(this.gameObject);
		tagTimer = 0f;
		//PhotonRealtimeのサーバーへ接続
		PhotonNetwork.ConnectUsingSettings(null);
		//スタート地点計算
		Vector2 rndPos = Vector2.zero;
		while(true){
			rndPos = Random.insideUnitCircle * 150f;
			if(rndPos.x < -20f){ if(rndPos.y > 20f){ break; } }
		}
		myStartPos = new Vector2( (592f + rndPos.x) , (-592 + rndPos.y) );
	}

	void Update () {
		//ルームに入室が完了していたら
		if(PhotonNetwork.inRoom){
			//チーム分けが完了したらオブジェクトを読み込み
			if(!loadOnce && myTeamID != 0){
				loadOnce = true;
				if(myTeamID == 2){
					myStartPos = myStartPos * -1.0f;
				}
				GameObject myPlayer = PhotonNetwork.Instantiate("character/t01",new Vector3(myStartPos.x,24f,myStartPos.y),Quaternion.identity,0);
				myPlayer.transform.LookAt(Vector3.zero);
				variableManage.myTeamID = myTeamID;
			}
			//3sに一回タグ付け
			tagTimer += Time.deltaTime;
			if(tagTimer > 3.0f){
				giveEnemyFlag();
				tagTimer = 0f;
			}
		}
	}

	//ロビーへ入室完了
	void OnJoinedLobby(){
		//どこかのルームへ入室
		PhotonNetwork.JoinRandomRoom();
	}

	//ロビーへの入室が失敗
	void OnFailedToConnectToPhoton(){
		Application.LoadLevel("mainMenu");
	}

	//ルームへ入室失敗
	void OnPhotonRandomJoinFailed(){
		//自分でルームを作成
		myRoomHash = new ExitGames.Client.Photon.Hashtable();
		myRoomHash.Add("time","0");
		PhotonNetwork.CreateRoom(Random.Range(1.0f,100f).ToString(),true,true,8,myRoomHash,roomProps);
		myTeamID = 1;
	}

	//無事にルームへ入室
	void OnJoinedRoom(){
		//削除忘れ注意
	}

	//PhotonRealtimeとの接続が切断された場合
	void OnConnectionFail(){
		Application.LoadLevel("mainMenu");
	}

	//ルームに誰か別のプレイヤーが入室したとき
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer){
		//自分がマスタークライアントだったとき
		if(PhotonNetwork.isMasterClient){
			//メンバー振り分け処理
			int allocateNumber = 0;
			//現在のチーム状況を取得
			GameObject[] team1Players = GameObject.FindGameObjectsWithTag("Player");
			GameObject[] team2Players = GameObject.FindGameObjectsWithTag("enemy");
			if( (team1Players.Length - 1) >= team2Players.Length ){
				//playerの方が多い場合
				if(myTeamID == 1){ allocateNumber = 2;	}
				else{ allocateNumber = 1; }
				scenePV.RPC("allocateTeam",newPlayer,allocateNumber);
			}else{
				//enemyの方が多い場合
				if(myTeamID == 2){ allocateNumber = 2;	}
				else{ allocateNumber = 1; }
				scenePV.RPC("allocateTeam",newPlayer,allocateNumber);
			}
		}
	}

	void OnApplicationPause(bool pauseStatus){
		if(pauseStatus){
			PhotonNetwork.Disconnect();
		}else{
			Application.LoadLevel("mainMenu");
		}
	}

	//敵に対してタグ付けを行う
	void giveEnemyFlag(){
		//チームIDが定義されていたら
		if(myTeamID != 0){
			int enID = 1;
			if(myTeamID == 1){enID = 2;}
			GameObject[] allFriendPlayer = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject player in allFriendPlayer){
				int id = player.GetComponent<characterStatus>().myTeamID;
				if(id == enID){
					player.tag = "enemy";
				}
			}
		}
	}

	[RPC]
	void allocateTeam(int teamID){
		if(myTeamID == 0){
			myTeamID = teamID;
		}
	}

}
