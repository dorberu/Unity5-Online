using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class characterMove : NetworkBehaviour {
	
	//移動関連
	private bool moveForward;
	private bool moveBack;
	private bool moveLeft;
	private bool moveRight;
	//視点関連
	public Transform myCam;
	private float moveRotationY;
	private float moveRotationX;
	//投げる処理関連
	private float throwTimer;
	public GameObject ballPrefab;
	public GameObject fireMouse;
	//ほか
	private Rigidbody myRigid;
	
	void Start () {
		//ローカルプレイヤー判定
		if(!isLocalPlayer){
			Destroy(myCam.gameObject);
		}
		//
		myRigid = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if(isLocalPlayer){
			//キーボード入力の取得
			if(Input.GetKey(KeyCode.W)){
				moveForward = true;
				moveBack = false;
			}else if(Input.GetKey(KeyCode.S)){
				moveForward = false;
				moveBack = true;
			}else{
				moveForward = false;
				moveBack = false;
			}
			if(Input.GetKey(KeyCode.A)){
				moveLeft = true;
				moveRight = false;
			}else if(Input.GetKey(KeyCode.D)){
				moveLeft = false;
				moveRight = true;
			}else{
				moveLeft = false;
				moveRight = false;
			}
			//マウス入力の取得
			moveRotationY = Input.GetAxis("Mouse X");
			moveRotationX = Input.GetAxis("Mouse Y");
			//視点変更
			this.transform.Rotate(Vector3.up,moveRotationY * 8.0f);
			myCam.transform.Rotate(Vector3.right,moveRotationX * -3.0f);
			//投げる処理
			throwTimer += Time.deltaTime;
			if(throwTimer > 0.5f && Input.GetMouseButtonDown(0)){
				Cmd_throwBall();
				throwTimer = 0f;
			}
			//落下した後、復帰する
			if(transform.position.y < -10.0f){
				transform.position = new Vector3(0f,10f,0f);
				myRigid.velocity = Vector3.zero;
			}
		}
	}
	
	void FixedUpdate(){
		//前後左右への移動処理
		if(moveForward){
			myRigid.AddForce(transform.TransformDirection(Vector3.forward) * 1.0f);
		}else if(moveBack){
			myRigid.AddForce(transform.TransformDirection(Vector3.forward) * -1.0f);
		}
		if(moveLeft){
			myRigid.AddForce(transform.TransformDirection(Vector3.left) * 1.0f);
		}else if(moveRight){
			myRigid.AddForce(transform.TransformDirection(Vector3.left) * -1.0f);
		}
		//回転慣性削除
		myRigid.angularVelocity = Vector3.zero;
	}
	
	[Command]
	public void Cmd_throwBall(){
		GameObject obj = 
			(GameObject)Instantiate(ballPrefab,fireMouse.transform.position,Quaternion.identity);
		ballManage bms = obj.GetComponent<ballManage>();
		bms.firePos = transform.TransformDirection(Vector3.forward);
		NetworkServer.Spawn(obj);
	}
	
}
