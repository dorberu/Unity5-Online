using UnityEngine;
using System.Collections;

public class adsManage : MonoBehaviour {

	public static AppCCloud appCCloud;

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
		//appc初期化 ※iOSメディアキーはiOSでリリースしないなら不要
		appCCloud = new AppCCloud().SetMK_iOS("iOSメディアキー")
		.On(AppCCloud.API.Purchase)
		.Start();
	}

}
