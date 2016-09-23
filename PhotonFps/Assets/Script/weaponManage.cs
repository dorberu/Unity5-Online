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
	// ロックオン用
	public GameObject xRotObject;
	public GameObject yRotObject;
	private float lockCalcTimer;

	// Use this for initialization
	void Start () {
		reloadTimer = 0.0f;
		wep01currentNum = wep01noa;
		lockCalcTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (myPV.isMine && !variableManage.controlLock) {
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

			// ロックオン関連
			lockCalcTimer += Time.deltaTime;
			if (lockCalcTimer > 0.5f) {
				lockCalcTimer = 0.0f;
				GameObject[] allEnemy = GameObject.FindGameObjectsWithTag ("enemy");
				variableManage.lockoned = false;
				float bestDist = 9999.0f;
				foreach (GameObject obj in allEnemy) {
					float tmpDist = Vector3.Distance (obj.transform.position, this.transform.position);
					if (tmpDist < 180.0f && obj != this.gameObject) {
						if (tmpDist < bestDist) {
							bestDist = tmpDist;
							variableManage.lockonTarget = obj;
							variableManage.lockoned = true;
						}
					}
				}
			}

			// 機体をロックオン対象へと向かせる
			if (variableManage.lockoned && variableManage.lockonTarget != null) {
				Vector3 tf = this.transform.TransformDirection (Vector3.forward);
				Vector3 tf2 = yRotObject.transform.TransformDirection (Vector3.forward);

				// ロック対象へのY角度計算
				Vector2 targetYpos = new Vector2 (
					                     variableManage.lockonTarget.transform.position.x,
					                     variableManage.lockonTarget.transform.position.z
				                     );
				Vector2 myYpos = new Vector2 (
					                 fireMouse.transform.position.x,
					                 fireMouse.transform.position.z
				                 );
				float yAngl = Vector2.Angle (new Vector2 (tf.x, tf.z), (targetYpos - myYpos));
				Vector3 tr = this.transform.TransformDirection (Vector3.right);
				float yDot = Vector2.Dot (new Vector2 (tr.x, tr.z).normalized, (targetYpos - myYpos).normalized);

				// ロック対象へのX軸計算
				Vector2 targetXpos = new Vector2 (
					                     variableManage.lockonTarget.transform.position.y,
					                     variableManage.lockonTarget.transform.position.z
				                     );
				Vector2 myXpos = new Vector2 (
					                 xRotObject.transform.position.y,
					                 xRotObject.transform.position.z
				                 );
				float xAngl = Vector2.Angle (new Vector2 (tf2.y, tf2.z), (targetXpos - myXpos));
				Vector3 tu = this.transform.TransformDirection (Vector3.up);
				float xDot = Vector2.Dot (new Vector2 (tu.y, tu.z).normalized, (targetXpos - myXpos).normalized);

				// 角度制限および左右計算
				if (yAngl > 80.0f) {
					yAngl = 80.0f;
				}
				if (yDot < 0.0f) {
					yAngl = yAngl * -1.0f;
				}
				if (xAngl > 12.0f) {
					xAngl = 12.0f;
				}
				if (xDot < 0.0f) {
					xAngl = xAngl * -1.0f;
				}

				// 角度適用
				yRotObject.transform.localRotation = Quaternion.Slerp (
					yRotObject.transform.localRotation,
					Quaternion.Euler (new Vector3 (0.0f, yAngl, 0.0f)),
					0.01f
				);
				xRotObject.transform.localRotation = Quaternion.Slerp (
					xRotObject.transform.localRotation,
					Quaternion.Euler (new Vector3 (xAngl, 0.0f, 0.0f)),
					0.01f
				);
			} else {
				yRotObject.transform.localRotation = Quaternion.Slerp (
					yRotObject.transform.localRotation,
					Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f)),
					0.01f
				);
				xRotObject.transform.localRotation = Quaternion.Slerp (
					xRotObject.transform.localRotation,
					Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f)),
					0.01f
				);
			}
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
