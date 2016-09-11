using UnityEngine;
using System.Collections;

public class characterMove : MonoBehaviour {

	public float maxSpd;
	public float cornering;
	public float basePower;

	public Rigidbody myRigid;
	public PhotonView myPV;
	public Camera myCam;

	// Use this for initialization
	void Start () {
		// 自分が積み込んだオブジェクトではない場合
		if (!myPV.isMine) {
			myRigid.isKinematic = true;
			myCam.transform.gameObject.SetActive (false);
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!Application.isMobilePlatform) {
			if (Input.GetKey (KeyCode.W)) {
				variableManage.movingYaxis = 1;
			} else if (Input.GetKey (KeyCode.S)) {
				variableManage.movingYaxis = -1;
			} else {
				variableManage.movingYaxis = 0;
			}
			if (Input.GetKey (KeyCode.A)) {
				variableManage.movingXaxis = 1;
			} else if (Input.GetKey (KeyCode.D)) {
				variableManage.movingXaxis = -1;
			} else {
				variableManage.movingXaxis = 0;
			}
		}
	}

	void FixedUpdate () {
		if (variableManage.movingYaxis != 0) {
			// 移動処理
			if (myRigid.velocity.magnitude < maxSpd) {
				myRigid.AddForce (transform.TransformDirection (Vector3.forward) * basePower * 11f * variableManage.movingYaxis);
			}
			// 旋回処理
			if (myRigid.angularVelocity.magnitude < (myRigid.velocity.magnitude * 0.3f)) {
				myRigid.AddTorque (transform.TransformDirection (Vector3.up) * cornering * variableManage.movingXaxis * -90.0f);
			} else {
				myRigid.angularVelocity = (myRigid.velocity.magnitude * 0.3f) * myRigid.angularVelocity.normalized;
			}
		}
	}
}
