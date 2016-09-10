using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ballManage : NetworkBehaviour {

	//どの方向に発射するか
	public Vector3 firePos;
	//
	private Rigidbody myRigid;
	private bool onceSw;

	void Start () {
		myRigid = GetComponent<Rigidbody>();
		onceSw = false;
		//一定時間でオブジェクト消滅
		Destroy(this.gameObject,6.0f);
	}

	void FixedUpdate(){
		if(isServer){
			if(myRigid.velocity.magnitude < 50f && !onceSw){
				myRigid.AddForce(firePos * 10.0f,ForceMode.VelocityChange);
			}else{
				onceSw = true;
			}
		}
	}

}
