using UnityEngine;
using System.Collections;

public class weaponManage : MonoBehaviour {

	public float wep01atkPow;	// 攻撃力
	public float wep01rate;	// 発射間隔
	public int wep01noa;	// 装弾数
	public float wep01spd;	// 弾速
	public string wep01name;	// 弾丸の名前
	private int wep01currentNum;	// 現在の残り装弾数
	private float reloadTimer;	// 充填までの時間

	public PhotonView myPV;
	public GameObject fireMouse;

	// Use this for initialization
	void Start () {
		reloadTimer = 0.0f;
		wep01currentNum = wep01noa;
	}
	
	// Update is called once per frame
	void Update () {
		if (myPV.isMine) {
			// Lキーで武器発射
			if (Input.GetKeyDown(KeyCode.L)) {
				variableManage.fireWeapon = true;
			}

			reloadTimer += Time.deltaTime;
			if (reloadTimer > wep01rate && variableManage.fireWeapon && wep01currentNum > 0) {
				reloadTimer = 0.0f;
				wep01currentNum -= 1;
				myPV.RPC (
					"fireBullet",
					PhotonTargets.All,
					wep01spd,
					wep01name,
					wep01atkPow,
					fireMouse.transform.position,
					fireMouse.transform.TransformDirection (Vector3.forward)
				);
			}

			// ボタンをfalseに
			variableManage.fireWeapon = false;
		}
	}

	[PunRPC]
	void fireBullet (float spd, string name, float pow, Vector3 pos, Vector3 targetAngle) {
		GameObject loadedBullet = GameObject.Instantiate (
			Resources.Load(name),
			pos,
			Quaternion.identity
		) as GameObject;
		loadedBullet.GetComponent<Rigidbody> ().AddForce (targetAngle * spd, ForceMode.VelocityChange);
		loadedBullet.GetComponent<mainShell> ().pow = pow;
	}
}
