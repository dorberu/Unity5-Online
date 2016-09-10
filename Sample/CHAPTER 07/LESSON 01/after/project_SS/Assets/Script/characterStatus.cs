﻿using UnityEngine;
using System.Collections;

public class characterStatus : MonoBehaviour {

	public int myTeamID;
	private PhotonView myPV;
	private float idSendTimer;
	//レーダー用
	public GameObject rdSphere;
	public GameObject rdCube;
	private GameObject[] rdEnTemp;
	private GameObject[] rdFrTemp;
	private float playerRefreshTimer;
	private float distRefreshTimer;
	
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
		//味方のレーダー範囲を表示
		if(variableManage.myTeamID == myTeamID){
			if(variableManage.mapEnabled && myTeamID != 0){
				rdSphere.SetActive(true);
			}else{
				rdSphere.SetActive(false);
			}
		}else{
			rdSphere.SetActive(false);
		}
		//敵をレーダーに表示する
		if(myPV.isMine){
			//敵味方一覧を３.5秒間隔でリフレッシュ
			playerRefreshTimer += Time.deltaTime;
			if(playerRefreshTimer > 3.5f){
				rdEnTemp = GameObject.FindGameObjectsWithTag("enemy");
				rdFrTemp = GameObject.FindGameObjectsWithTag("Player");
			}
			//全味方のレーダー範囲に敵がいるか２秒間隔で計算
			distRefreshTimer += Time.deltaTime;
			if(distRefreshTimer > 2.0f && rdFrTemp != null){
				//全プレイヤーを取り出す
				foreach(GameObject player in rdFrTemp){
					//全敵を取り出す
					foreach(GameObject enemy in rdEnTemp){
						//総当りで距離を算出し、レーダー範囲であればオブジェクトをONに
						float dist = Vector3.Distance(player.transform.position,enemy.transform.position);
						if(dist < 180f && variableManage.mapEnabled){
							enemy.GetComponent<characterStatus>().rdCube.SetActive(true);
						}else{
							enemy.GetComponent<characterStatus>().rdCube.SetActive(false);
						}
					}
				}
			}
			//
		}
		//そもそもマップ表示画面でなければレーダー用オブジェクトは強制OFF
		if(!variableManage.mapEnabled){
			rdCube.SetActive(false);
		}
		//
	}

	[RPC]
	void syncMyID(int myID){
		myTeamID = myID;
	}

}
