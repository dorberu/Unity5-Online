using UnityEngine;
using System.Collections;

public class characterStatus : MonoBehaviour {

	public int myTeamID;
	private PhotonView myPV;
	private float idSendTimer;

	// Use this for initialization
	void Start () {
		myTeamID = 0;
		myPV = PhotonView.Get (this.gameObject);
		idSendTimer = 0.0f;
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
	}

	[PunRPC]
	void syncMyID (int myID) {
		myTeamID = myID;
	}
}
