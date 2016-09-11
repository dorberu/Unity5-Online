using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon;

public class gameManage : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		PhotonNetwork.ConnectUsingSettings (null);
	}
	
	// Update is called once per frame
	void Update () {
	
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
		base.OnPhotonRandomJoinFailed (codeAndMsg);
		PhotonNetwork.CreateRoom (null);
	}

	public override void OnJoinedRoom () {
		base.OnJoinedRoom ();
		GameObject myPlayer = PhotonNetwork.Instantiate ("character/t01", new Vector3(440.0f, 30.0f, -560.0f), Quaternion.identity, 0);
	}

	public override void OnConnectionFail (DisconnectCause cause) {
		Debug.Log ("Disconnected to Photon Realtime.");
		base.OnConnectionFail (cause);
		SceneManager.LoadScene ("mainMenu");
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			PhotonNetwork.Disconnect ();
		} else {
			SceneManager.LoadScene ("mainMenu");
		}
	}
}
