using UnityEngine;
using System.Collections;

public class manage : MonoBehaviour {

	private bool keyLock;

	// Use this for initialization
	void Start () {
		keyLock = false;
		PhotonNetwork.ConnectUsingSettings (null);
	}

	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnJoinedRoom() {
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
