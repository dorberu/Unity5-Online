using UnityEngine;
using System.Collections;
using Photon;

public class manage : Photon.PunBehaviour {

	private bool keyLock;

	// Use this for initialization
	void Start () {
		keyLock = false;
		PhotonNetwork.ConnectUsingSettings (null);
	}

	public override void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom ();
	}

	public override void OnJoinedRoom() {
		base.OnJoinedRoom ();
		Debug.Log ("In Room");
		keyLock = true;
	}

	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom (null);
	}

	void FixedUpdate () {
		if (Input.GetMouseButtonDown (0) && keyLock) {
			GameObject mySyncObj = PhotonNetwork.Instantiate (
				"Cube",
				new Vector3(9.0f, 0.0f, 0.0f),
				Quaternion.identity,
				0
			);
			Rigidbody mySyncObjRB = mySyncObj.GetComponent<Rigidbody> ();
			mySyncObjRB.isKinematic = false;
			float rndPow = Random.Range (1.0f, 5.0f);
			mySyncObjRB.AddForce (Vector3.left * rndPow, ForceMode.Impulse);
		}
	}
}
