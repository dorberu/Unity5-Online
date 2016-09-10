using UnityEngine;
using System.Collections;

public class manage : MonoBehaviour {

	private bool keyLock;

	void Start () {
		keyLock = false;
		//PhotonRealtimeのサーバーへ接続、ロビーへ入室
		PhotonNetwork.ConnectUsingSettings(null);
	}

	//ロビーに入室した
	void OnJoinedLobby(){
		//とりあえずどこかのルームへ入室する
		PhotonNetwork.JoinRandomRoom();
	}

	//ルームへ入室した
	void OnJoinedRoom(){
		//入室が完了したことを出力し、キーロック解除
		Debug.Log("ルームへ入室しました");
		keyLock = true;
	}

	//ルームの入室に失敗
	void OnPhotonRandomJoinFailed(){
		//自分でルームを作成して入室
		PhotonNetwork.CreateRoom(null);
	}
	
	void FixedUpdate () {
		//左クリックが押されたらオブジェクトを読み込み
		if(Input.GetMouseButtonDown(0) && keyLock){
			GameObject mySyncObj = PhotonNetwork.Instantiate("Cube",new Vector3(9.0f,0f,0f),Quaternion.identity,0);
			//動きをつけるためにリジッドボディを取得し、力を加える
			Rigidbody mySyncObjRB = mySyncObj.GetComponent<Rigidbody>();
			mySyncObjRB.isKinematic = false;
			float rndPow = Random.Range(1.0f,5.0f);
			mySyncObjRB.AddForce(Vector3.left * rndPow, ForceMode.Impulse);
		}
	}

}
