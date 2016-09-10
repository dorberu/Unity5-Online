using UnityEngine;
using System.Collections;

public class mainMenuManage : MonoBehaviour {
	

	void Start () {
		//レベルアップ処理
		if(variableManage.currentExp >= variableManage.nextExp){
			variableManage.currentLv += 1;
			variableManage.currentExp = 
				variableManage.nextExp - variableManage.currentExp;
			variableManage.showLvupMes = true;
		}
		//レベルから次の必要経験値を計算
		variableManage.nextExp = variableManage.currentLv * 100;
	}

	void Update () {

	}
	
}
