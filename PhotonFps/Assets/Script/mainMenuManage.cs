﻿using UnityEngine;
using System.Collections;

public class mainMenuManage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// レベルアップ処理
		if (variableManage.currentExp >= variableManage.nextExp) {
			variableManage.currentLv += 1;
			variableManage.currentExp = variableManage.currentExp - variableManage.nextExp;
			variableManage.showLvupMes = true;
		}
		// レベルから次の必要経験値を計算
		variableManage.nextExp = variableManage.currentLv * 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}