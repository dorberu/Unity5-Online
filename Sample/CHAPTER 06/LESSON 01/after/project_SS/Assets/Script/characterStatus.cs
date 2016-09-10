using UnityEngine;
using System.Collections;

public class characterStatus : MonoBehaviour {

	public int myTeamID;
	private PhotonView myPV;
	private float idSendTimer;

	void Start () {
		myTeamID = 0;
		myPV = PhotonView.Get(this.gameObject);
		idSendTimer = 0f;
	}

	void Update () {
		//チームIDがデフォルトのままであれば、代入する
		if(myPV.isMine){
			idSendTimer += Time.deltaTime;
			if(idSendTimer > 3.0f){
				myTeamID = variableManage.myTeamID;
				myPV.RPC("syncMyID",PhotonTargets.Others,myTeamID);
				idSendTimer = 0f;
			}
		}
	}

	[RPC]
	void syncMyID(int myID){
		myTeamID = myID;
	}

}
