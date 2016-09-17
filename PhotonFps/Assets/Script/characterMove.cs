using UnityEngine;
using System.Collections;

public class characterMove : MonoBehaviour {

	public float maxSpd;
	public float cornering;
	public float basePower;
	public float maxHealth;

	public Rigidbody myRigid;
	public PhotonView myPV;
	public Camera myCam;

	private bool isStandBy;
	private float standByTimer;
	private GameObject hitObject;
	private float revivalTimer;

	// Use this for initialization
	void Start () {
		// 自分が積み込んだオブジェクトではない場合
		if (!myPV.isMine) {
			myRigid.isKinematic = true;
			myCam.transform.gameObject.SetActive (false);
			Destroy (this);
		}

		// 準備中フェーズへ
		StandBy ();

		revivalTimer = 0.0f;
		variableManage.currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		// 状態遷移
		PhaseChange ();

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

		// 被弾処理
		if (hitObject != null) {
			mainShell hitShell = hitObject.GetComponent<mainShell> ();
			variableManage.currentHealth -= hitShell.pow;
			if (variableManage.currentHealth < 0) {
				variableManage.currentHealth = 0;
			}
			hitObject = null;
		}

		// HPが0になったとき
		if (variableManage.currentHealth == 0.0f) {
			revivalTimer += Time.deltaTime;
			variableManage.controlLock = true;
			if (revivalTimer > 10.0f) {
				revivalTimer = 0.0f;
				variableManage.controlLock = false;
				variableManage.currentHealth = maxHealth;
			}
		}

		// 姿勢制御
		float xAngle = transform.rotation.eulerAngles.x;
		float zAngle = transform.rotation.eulerAngles.z;
		if (xAngle > 180.0f) xAngle = xAngle - 360.0f;
		if (zAngle > 180.0f) zAngle = zAngle - 360.0f;
		if (xAngle > 30.0f) xAngle = 30.0f;
		else if (xAngle < -30.0f) xAngle = -30.0f;
		if (zAngle > 30.0f) zAngle = 30.0f;
		else if (zAngle < -30.0f) zAngle = -30.0f;
		transform.rotation = Quaternion.Euler (new Vector3(xAngle, transform.rotation.eulerAngles.y, zAngle));
	}

	void FixedUpdate () {
		if (!variableManage.controlLock) {
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

		// 姿勢制御
		Vector3 raycastStartPos = new Vector3(transform.position.x, (transform.position.y + 1.0f), transform.position.z);
		RaycastHit rhit;
		if (!Physics.Raycast (raycastStartPos, transform.TransformDirection(-Vector3.up), out rhit, 3.0f)) {
			myRigid.AddForce (Vector3.up * -50.0f);
		}
	}

	void OnCollisionEnter (Collision col) {
		// bulletレイヤーに処理を限定
		if (col.gameObject.layer == 9) {
			hitObject = col.gameObject;
		}
	}

	void PhaseChange () {
		if (isStandBy) {
			if (standByTimer > 0.0f) {
				standByTimer -= Time.deltaTime;
			} else {
				myRigid.constraints = RigidbodyConstraints.None;
				isStandBy = false;
			}
		}
	}

	void StandBy () {
		isStandBy = true;
		standByTimer = 5.0f;
		myRigid.constraints = RigidbodyConstraints.FreezeRotation;
	}
}
