using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MiniJSON;

public class AppCCloud
{
	
	/// <summary>
	/// API.
	/// </summary>
	public enum API
	{
		Data, Gamers, Purchase, Push, Reward, 
	}
	
	/// <summary>
	/// Horizon.
	/// </summary>
	public enum Horizon
	{
		Left, Center, Right,
	}
	
	/// <summary>
	/// Vertical.
	/// </summary>
	public enum Vertical
	{
		Top, Bottom,
	}

	public _Ad Ad;
	public _Data Data;
	public _Gamers Gamers;
	public _Purchase Purchase;
	public _Push Push;
	public _Reward Reward;
	
	private string mk;
	private static List<API> apis = new List<API>();
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AppCCloud"/> class.
	/// </summary>
	public AppCCloud()
	{
		Ad = new _Ad();
	}
	
	/// <summary>
	/// Raises the  event.
	/// </summary>
	/// <param name="api">API.</param>
	public AppCCloud On(API api)
	{
		apis.Add(api);
		switch (api)
		{
		case API.Data:
			Data = new _Data();
			break;
		case API.Gamers:
			Gamers = new _Gamers();
			break;
		case API.Purchase:
			Purchase = new _Purchase();
			break;
		case API.Push:
			Push = new _Push();
			break;
		case API.Reward:
			Reward = new _Reward();
			break;
		default:
			break;
		}
		return this;
	}
	
	#if UNITY_EDITOR
	/// <summary>
	/// Sets the M k_i O.
	/// </summary>
	/// <returns>The M k_i O.</returns>
	/// <param name="mk">Mk.</param>
	public AppCCloud SetMK_iOS(string mk)
	{
		return this;
	}
	
	/// <summary>
	/// Start this instance.
	/// </summary>
	public AppCCloud Start(){
		return this;
	}
	/// <summary>
	/// Opens the recovery view.
	/// </summary>
	public void OpenRecoveryView() {}

	#elif UNITY_IPHONE
	
	[DllImport("__Internal")]
	private static extern void _setupAppCWithMediaKey(string mk, int opt_ad, int opt_gamers, int opt_push, int opt_data, int opt_purchase, int opt_reward);
	[DllImport("__Internal")]
	private static extern int _isForegroundAd();
	[DllImport("__Internal")]
	private static extern void _showAppCCutinView();
	[DllImport("__Internal")]
	private static extern void _hideAppCCutinView();
	[DllImport("__Internal")]
	private static extern int _showingAppCCutin();
	[DllImport("__Internal")]
	private static extern void _showAppCMatchAppView(double horizontal, double vertical);
	[DllImport("__Internal")]
	private static extern void _hideAppCMatchAppView();
	
	[DllImport("__Internal")]
	private static extern int _openGamers();
	[DllImport("__Internal")]
	private static extern string _gamersGetNickname();
	[DllImport("__Internal")]
	private static extern int _gamersPlayCountIncrement();
	[DllImport("__Internal")]
	private static extern int _gamersGetPlayCount();
	[DllImport("__Internal")]
	private static extern void _gamersAddActiveLb(string[] lb_id, int count);
	[DllImport("__Internal")]
	private static extern int _gamersAddLbInt(int lb_id, int score);
	[DllImport("__Internal")]
	private static extern int _gamersAddLbDec(int lb_id, float score);
	[DllImport("__Internal")]
	private static extern string _gamersGetLb(int lb_id);
	
	[DllImport("__Internal")]
	private static extern void _startRecovery();
	
	[DllImport("__Internal")]
	private static extern void _dataStoreAddActiveData(string id);
	[DllImport("__Internal")]
	private static extern int _dataStoreIntegerWithKey(string key);
	[DllImport("__Internal")]
	private static extern string _dataStoreStringWithKey(string key);
	[DllImport("__Internal")]
	private static extern int _dataStoreSetIntegerWithKey(string key, int value);
	[DllImport("__Internal")]
	private static extern int _dataStoreSetStringWithKey(string key, string value);
	
	[DllImport("__Internal")]
	private static extern int _purchaseShowList(); 
	[DllImport("__Internal")]
	private static extern int _purchaseRestore();
	[DllImport("__Internal")]
	private static extern  void _purchaseAddActiveItem(string key, string[] product_id, int count);
	[DllImport("__Internal")]
	private static extern int _purchaseUseCountWithKey(string key, int reduce);
	[DllImport("__Internal")]
	private static extern int _purchaseSetCountWithKey(string key, int count);
	[DllImport("__Internal")]
	private static extern int _purchaseGetCountWithKey(string key);
	[DllImport("__Internal")]
	private static extern string _purchaseGetJsonItem(string key);

	public AppCCloud SetMK_iOS(string mk)
	{
		this.mk = mk;
		return this;
	}
	
	public AppCCloud Start()
	{
		_setupAppCWithMediaKey(mk,
		                       (Ad == null ? 0 : 1),
		                       (Gamers == null ? 0 : 1),
		                       (Push == null ? 0 : 1),
		                       (Data == null ? 0 : 1),
		                       (Purchase == null ? 0 : 1),
		                       (Reward == null ? 0 : 1)
		                       );
		return this;
	}
	public void OpenRecoveryView()
	{
		_startRecovery();
	}

	#elif UNITY_ANDROID
	private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	private static AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	
	public AppCCloud SetMK_iOS(string mk)
	{
		return this;
	}

	public AppCCloud Start()
	{
		int[] apiNums = new int[apis.Count];
		for (int i = 0; i < apis.Count; i++) {
			apiNums[i] = (int)apis[i];
		}
		activity.Call("start", (int[])apiNums);
		return this;
	}

	public void OpenRecoveryView()
	{
		activity.Call("callRecoveryActivity");
	}

	#endif
	
	/// <summary>
	/// Ad.
	/// </summary>
	public class _Ad
	{
		#if UNITY_EDITOR

		/// <summary>
		/// Determines whether this instance is ad.
		/// </summary>
		/// <returns><c>true</c> if this instance is ad; otherwise, <c>false</c>.</returns>
		public bool IsForegroundAd() { return false; }

		/// <summary>
		/// Shows the simple view.
		/// </summary>
		/// <param name="vertical">Vertical.</param>
		public void ShowSimpleView(Vertical vertical){}
		
		/// <summary>
		/// Shows the simple view.
		/// </summary>
		/// <param name="vertical">Vertical.</param>
		/// <param name="skinColor">Skin color.</param>
		/// <param name="textColor">Text color.</param>
		public void ShowSimpleView(Vertical vertical, string skinColor, string textColor){}
		
		/// <summary>
		/// Hides the simple view.
		/// </summary>
		public void HideSimpleView(){}
		
		/// <summary>
		/// Shows the marquee view.
		/// </summary>
		/// <param name='vertical'>
		/// Vertical.Top or Bottom
		/// </param>
		public void ShowMarqueeView(Vertical vertical){}
		
		/// <summary>
		/// Shows the marquee view.
		/// </summary>
		/// <param name='vertical'>
		/// Vertical.Top or Bottom
		/// </param>
		/// <param name='textColor'>
		/// Text color. Android only "black","green","pink" etc...
		/// </param>
		public void ShowMarqueeView(Vertical vertical, string textColor){}
		
		/// <summary>
		/// Hides the marquee view.
		/// </summary>
		public void HideMarqueeView(){}
		
		/// <summary>
		/// Shows the move icon view.
		/// </summary>
		/// <param name='horizon'>
		/// Horizon.Left or Center or Right
		/// </param>
		/// <param name='vertical'>
		/// Vertical.Top or Bottom
		/// </param>
		public void ShowMoveIconView(Horizon horizon, Vertical vertical){}
		
		/// <summary>
		/// Shows the move icon view.
		/// </summary>
		/// <param name='horizon'>
		/// Horizon.Left or Center or Right
		/// </param>
		/// <param name='vertical'>
		/// Vertical.Top or Bottom
		/// </param>
		/// <param name='skinColor'>
		/// Skin color. Android only "black","green","pink" etc...
		/// </param>
		/// <param name='textColor'>
		/// Text color. Android only "black","green","pink" etc...
		/// </param>
		public void ShowMoveIconView(Horizon horizon, Vertical vertical, string skinColor, string textColor){}
		
		/// <summary>
		/// Hides the move icon view.
		/// </summary>
		public void HideMoveIconView(){}
		
		/// <summary>
		/// Shows the web view.
		/// </summary>
		public void OpenAdView(){}

		/// <summary>
		/// Determines whether this instance is ad view.
		/// </summary>
		/// <returns><c>true</c> if this instance is ad view; otherwise, <c>false</c>.</returns>
		public bool IsAdView() { return false; }

		/// <summary>
		/// Shows the cutin view.
		/// </summary>
		/// <param name='cutinView'>
		/// CutinView.Basic or Icons
		/// </param>
		public void ShowCutinView(){}
		
		/// <summary>
		/// Shows the cutin view finish Android only.
		/// </summary>
		/// <param name='cutinView'>
		/// CutinView.Basic or Icons
		/// </param>
		public void ShowCutinViewFinish_Android(){}
		
		/// <summary>
		/// Hides the cutin view.
		/// </summary>
		public void HideCutinView(){}
		
		/// <summary>
		/// Is cutin view.
		/// </summary>
		public bool IsCutinView(){ return false; }
		
		/// <summary>
		/// Shows the match app view.
		/// </summary>
		/// <param name="vertical">Vertical.</param>
		public void ShowMatchAppView(Horizon horizon, Vertical vertical){}
		
		/// <summary>
		/// Hides the match app view.
		/// </summary>
		public void HideMatchAppView(){}

		/// <summary>
		/// Shows the rec banner view.
		/// </summary>
		/// <param name="vertical">Vertical.</param>
		public void ShowRecBannerView(Vertical vertical){}
		
		/// <summary>
		/// Hides the rec banner view.
		/// </summary>
		public void HideRecBannerView(){}
		
		/// <summary>
		/// Shows the rec interstitial view.
		/// </summary>
		public void ShowRecInterstitialView(){}
		
		/// <summary>
		/// Hides the rec interstitial view.
		/// </summary>
		public void ShowRecInterstitialViewFinish_Android(){}

		/// <summary>
		/// Hides all views.
		/// </summary>
		public void HideAllViews(){}

		#elif UNITY_IPHONE

		public bool IsForegroundAd() {
			if(_isForegroundAd() != 0)
				return true;
			else
				return false;
		}

		public void ShowSimpleView(Vertical vertical) {}
		
		public void ShowSimpleView(Vertical vertical, string skinColor, string textColor) {}
		
		public void HideSimpleView() {}
		
		public void ShowMarqueeView(Vertical vertical) {}
		
		public void ShowMarqueeView(Vertical vertical, string textColor) {}
		
		public void HideMarqueeView() {}
		
		private double horizonIOS(Horizon horizon)
		{
			double h = 0;
			switch (horizon)
			{
			case Horizon.Left: 
				h = 1;
				break;
			case Horizon.Right:
				h = 2;
				break;
			default:
				h =  0;
				break;
			}
			return h;
		}
		
		private double verticalIOS(Vertical vertical)
		{
			double v = 0;
			switch (vertical)
			{
			case Vertical.Top:
				v = 1;
				break;
			case Vertical.Bottom:
				v = 2;
				break;
			default:
				v = 1; 
				break;
			}
			return v;
		}
		
		public void ShowMoveIconView(Horizon horizon, Vertical vertical) {}
		
		public void ShowMoveIconView(Horizon horizon, Vertical vertical, string skinColor, string textColor) {}
		
		public void HideMoveIconView() {}
		
		public void OpenAdView() {}

		public bool IsAdView() { return false; }

		public void ShowCutinView()
		{
			_showAppCCutinView();
		}
		
		public void ShowCutinViewFinish_Android() {}
		
		public bool IsCutinView() {
			if(_showingAppCCutin() != 0)
				return true;
			else
				return false;
		}
		
		public void HideCutinView() {
			_hideAppCCutinView();
		}
		
		public void ShowMatchAppView(Horizon horizon, Vertical vertical) {
			double h = horizonIOS(horizon);
			double v = verticalIOS(vertical);
			_showAppCMatchAppView(h, v);
		}
		
		public void HideMatchAppView(){
			_hideAppCMatchAppView();
		}

		public void ShowRecBannerView(Vertical vertical){}
		
		public void HideRecBannerView(){}
		
		public void ShowRecInterstitialView(){}
		
		public void ShowRecInterstitialViewFinish_Android(){}

		public void HideAllViews() {
			_hideAppCCutinView();
			_hideAppCMatchAppView();
		}
		
		#elif UNITY_ANDROID
		public bool IsForegroundAd() {
			return activity.Call<bool>("isForegroundAd");
		}

		public void ShowSimpleView(Vertical vertical)
		{
			ShowSimpleView(vertical, "", "");
		}
		
		public void ShowSimpleView(Vertical vertical, string skinColor, string textColor)
		{
			activity.Call("showSimpleView", (int)vertical, (string)skinColor, (string)textColor);
		}
		
		public void HideSimpleView()
		{
			activity.Call("hideSimpleView");
		}
		
		public void ShowMarqueeView(Vertical vertical)
		{
			ShowMarqueeView(vertical, "");
		}
		
		public void ShowMarqueeView(Vertical vertical, string textColor)
		{
			activity.Call("showMarqueeView", (int)vertical, (string)textColor);
		}
		
		public void HideMarqueeView()
		{
			activity.Call("hideMarqueeView");
		}
		
		public void ShowMoveIconView(Horizon horizon, Vertical vertical)
		{
			ShowMoveIconView(horizon, vertical, "", "");
		}
		
		public void ShowMoveIconView(Horizon horizon, Vertical vertical, string skinColor, string textColor)
		{
			activity.Call("showMoveIconView", (int)horizon, (int)vertical, (string)skinColor, (string)textColor);
		}
		
		public void HideMoveIconView()
		{
			activity.Call("hideMoveIconView");
		}
		
		public void OpenAdView()
		{
			activity.Call("callAdActivity");
		}

		public bool IsAdView() {
			return activity.Call<bool>("isAdActivity");
		}

		public void ShowCutinView()
		{
			activity.Call("callCutin");
		}
		
		public void ShowCutinViewFinish_Android()
		{
			activity.Call("callCutinFinish");
		}
		
		public void HideCutinView() {
			activity.Call("initCutin");
		}
		
		public bool IsCutinView() {
			return activity.Call<bool>("isCutinView");
		}
		
		public void ShowMatchAppView(Horizon horizon, Vertical vertical)
		{
			activity.Call("showMatchAppView", (int)horizon, (int)vertical);
		}
		
		public void HideMatchAppView()
		{
			activity.Call("hideMatchAppView");
		}

		public void ShowRecBannerView(Vertical vertical)
		{
			activity.Call("showRecBannerView", (int)vertical);
		}
		
		public void HideRecBannerView()
		{
			activity.Call("hideRecBannerView");
		}
		
		public void ShowRecInterstitialView()
		{
			activity.Call("callRecInterstitial");
		}
		
		public void ShowRecInterstitialViewFinish_Android()
		{
			activity.Call("callRecInterstitialFinish");
		}

		public void HideAllViews() {
			activity.Call("hideAllAdViews");
		}
		#endif
	}
	
	/// <summary>
	/// Data.
	/// </summary>
	public class _Data {
		
		#if UNITY_EDITOR 
		/// <summary>
		/// Sets the data store.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="val">Value.</param>
		public void SetDataStore(string id, int val) {}
		
		/// <summary>
		/// Sets the data store.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="text">Text.</param>
		public void SetDataStore(string id, string text) {}
		
		/// <summary>
		/// Gets the data store.
		/// </summary>
		/// <returns>The data store.</returns>
		/// <param name="id">Identifier.</param>
		public DataStore GetDataStore(string id) { return null; }
		
		#elif UNITY_IPHONE
		public void SetDataStore(string id, int val)
		{
			_dataStoreSetIntegerWithKey(id, val);
		}
		
		public void SetDataStore(string id, string text)
		{
			_dataStoreSetStringWithKey(id, text);
		}
		
		public DataStore GetDataStore(string id)
		{
			int val = _dataStoreIntegerWithKey(id);
			string text = _dataStoreStringWithKey(id);
			return new DataStore(id, val, text);
		}
		
		#elif UNITY_ANDROID
		public void SetDataStore(string id, int val)
		{
			activity.Call("setDataStore", (string)id, (int)val);
		}
		
		public void SetDataStore(string id, string text) {
			activity.Call("setDataStore", (string)id, (string)text);
		}
		
		public DataStore GetDataStore(string id)
		{
			try
			{
				return DataStore.Create(activity.Call<string>("getDataStore", (string)id));
			}
			catch
			{
				return new DataStore();
			}
		}
		#endif
	}
	
	/// <summary>
	/// Gamers.
	/// </summary>
	public class _Gamers {
		
		public enum MessageType
		{
			Welcome, BestScore, Achieve,
		}
		
		#if UNITY_EDITOR 
		/// <summary>
		/// Incs the play count.
		/// </summary>
		public void IncPlayCount() {}
		
		/// <summary>
		/// Gets the play count.
		/// </summary>
		/// <returns>
		/// The play count.
		/// </returns>
		public int GetPlayCount() { return 0; }
		
		/// <summary>
		/// Sets the leader board.
		/// </summary>
		/// <param name='id'>
		/// Identifier.
		/// </param>
		/// <param name='score'>
		/// Score.
		/// </param>
		public void SetLeaderBoard(int id, int score) {}
		
		/// <summary>
		/// Sets the leader board.
		/// </summary>
		/// <param name='id'>
		/// Identifier.
		/// </param>
		/// <param name='score'>
		/// Score.
		/// </param>
		public void SetLeaderBoard(int id, float time) {}
		
		/// <summary>
		/// Gets the leader board.
		/// </summary>
		/// <returns>The leader board.</returns>
		/// <param name="id">Identifier.</param>
		public LeaderBoard GetLeaderBoard(int id) { return null; }
		
		/// <summary>
		/// Opens the gamers view.
		/// </summary>
		public void OpenGamersView() {}
		
		/// <summary>
		/// Sets the active leader boards.
		/// </summary>
		/// <param name="ids">Identifiers.</param>
		public void setActiveLeaderBoards(params int[] ids){}
		
		#elif UNITY_IPHONE
		public void IncPlayCount()
		{
			_gamersPlayCountIncrement();
		}
		
		public int GetPlayCount()
		{
			return _gamersGetPlayCount();
		}
		
		public void SetLeaderBoard(int id, int score)
		{
			_gamersAddLbInt(id, score);
		}
		
		public void SetLeaderBoard(int id, float time)
		{
			_gamersAddLbDec(id, time);
		}
		
		public LeaderBoard GetLeaderBoard(int id)
		{
			string json = _gamersGetLb(id);
			try
			{
				return LeaderBoard.Create(json);
			} catch {
				return new LeaderBoard();
			}
		}
		
		public void OpenGamersView()
		{
			_openGamers();
		}
		
		public void setActiveLeaderBoards(params int[] ids){
			string[] idss = new string[ids.Length];
			for (int i=0 ; i<ids.Length ;i++) {
				int id = ids[i];
				idss[i] = id.ToString();
			}
			_gamersAddActiveLb(idss, idss.Length);
		}
		
		#elif UNITY_ANDROID
		public void IncPlayCount()
		{
			activity.Call("incPlayCount");
		}
		
		public int GetPlayCount() {
			try
			{
				return activity.Call<int>("getPlayCount");
			}
			catch
			{
				return 0;
			}
		}
		
		public void SetLeaderBoard(int id, int score)
		{
			activity.Call("setLeaderBoard", (int)id, (int)score);
		}
		
		public void SetLeaderBoard(int id, float time) {
			activity.Call("setLeaderBoard", (int)id, (float)time);
		}
		
		public LeaderBoard GetLeaderBoard(int id)
		{
			try
			{
				return LeaderBoard.Create(activity.Call<string>("getLeaderBoard", (int)id));
			}
			catch
			{
				return new LeaderBoard();
			}
		}
		
		public void OpenGamersView()
		{
			activity.Call("showGamersView");
		}
		
		public void setActiveLeaderBoards(params int[] ids)
		{
			activity.Call("setActiveLeaderBoards", (int[])ids);
		}
		#endif
	}
	
	/// <summary>
	/// Push.
	/// </summary>
	public class _Push {
	}
	
	/// <summary>
	/// Purchase.
	/// </summary>
	public class _Purchase {
		
		#if UNITY_EDITOR 
		/// <summary>
		/// Adds the item count.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="addCount">Add count.</param>
		public void AddItemCount(string id, int addCount) {}
		
		/// <summary>
		/// Sets the item count.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="count">Count.</param>
		public void SetItemCount(string id, int count) {}
		
		/// <summary>
		/// Gets the item count.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public int GetItemCount(string id) { return 0; }
		
		/// <summary>
		/// Gets the item.
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="id">Identifier.</param>
		public Item GetItem(string id) { return null; }
		
		/// <summary>
		/// Opens the purchase view.
		/// </summary>
		public void OpenPurchaseView() {}
		
		/// <summary>
		/// Opens the Restore view.
		/// </summary>
		public void OpenRestoreView() {}
		
		/// <summary>
		/// Sets the active items.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="ids">Identifiers.</param>
		public void setActiveItems(string key, params string[] ids) {}
		
		#elif UNITY_IPHONE
		public void AddItemCount(string id, int addCount) {
			_purchaseUseCountWithKey(id, -addCount);
		}
		
		public void SetItemCount(string id, int count) {
			_purchaseSetCountWithKey(id, count);
		}
		
		public int GetItemCount(string id)
		{
			return _purchaseGetCountWithKey(id);
		}
		
		public Item GetItem(string id)
		{
			string json = _purchaseGetJsonItem(id);
			if(json == "")
				return null;
			else
				return Item.Create(json);
		}
		
		public void OpenPurchaseView()
		{
			_purchaseShowList();
		}
		
		public void OpenRestoreView()
		{
			_purchaseRestore();
		}
		
		public void setActiveItems(string key, params string[] ids) {
			_purchaseAddActiveItem(key, ids, ids.Length);
		}
		
		#elif UNITY_ANDROID
		public void AddItemCount(string id, int addCount)
		{
			activity.Call("addItemCount", (string)id, (int)addCount);
		}
		
		public void SetItemCount(string id, int count)
		{
			activity.Call("setItemCount", (string)id, (int)count);
		}
		
		public int GetItemCount(string id)
		{
			try
			{
				return activity.Call<int>("getItemCount", (string)id);
			}
			catch
			{
				return 0;
			}
		}
		
		public Item GetItem(string id)
		{
			try
			{
				return Item.Create(activity.Call<string>("getItem", (string)id));
			}
			catch
			{
				return new Item();
			}
		}
		
		public void OpenPurchaseView()
		{
			activity.Call("showPurchaseView");
		}
		
		public void OpenRestoreView() {}
		
		public void setActiveItems(string key, params string[] ids)
		{
			activity.Call("setActiveItems", (string)key, (string[])ids);
		}
		
		#endif
	}
	
	/// <summary>
	/// Reward.
	/// </summary>
	public class _Reward {
		
		#if UNITY_EDITOR 
		/// <summary>
		/// Opens the reward point view.
		/// </summary>
		public void OpenRewardPointView() {}
		/// <summary>
		/// Opens the reward service view.
		/// </summary>
		/// <param name="serviceId">Service identifier.</param>
		public void OpenRewardServiceView(string serviceId) {}
		/// <summary>
		/// Determines whether this instance is reward service the specified serviceId.
		/// </summary>
		/// <returns><c>true</c> if this instance is reward service the specified serviceId; otherwise, <c>false</c>.</returns>
		/// <param name="serviceId">Service identifier.</param>
		public bool IsRewardService(string serviceId) { return false; }
		/// <summary>
		/// Clears the reward service.
		/// </summary>
		/// <param name="serviceId">Service identifier.</param>
		public void ClearRewardService(string serviceId) {}

		#elif UNITY_IPHONE
		public void OpenRewardPointView() {}

		public void OpenRewardServiceView(string serviceId) {}

		public bool IsRewardService(string serviceId) { return false; }

		public void ClearRewardService(string serviceId) {}

		#elif UNITY_ANDROID
		public void OpenRewardPointView()
		{
			activity.Call("showRewardPointView");
		}

		public void OpenRewardServiceView(string serviceId)
		{
			activity.Call("showRewardServiceView", (string)serviceId);
		}

		public bool IsRewardService(string serviceId)
		{
			return activity.Call<bool>("isRewardService", (string)serviceId);
		}

		public void ClearRewardService(string serviceId)
		{
			activity.Call("clearRewardService", (string)serviceId);
		}

		#endif
	}
	
	
	/// <summary>
	/// Data store.
	/// </summary>
	public class DataStore
	{
		public string Id { get; private set;}
		public  int Val { get; private set;}
		public  string Text { get; private set;}
		
		public DataStore()
		{
			this.Id = "";
			this.Val = 0;
			this.Text = "";
		}
		
		public DataStore(string id, int val, string text):base()
		{
			this.Id = id;
			this.Val = val;
			this.Text = text;
		}
		public static DataStore Create(string json)
		{
			IDictionary obj = (IDictionary)Json.Deserialize(json);
			return new DataStore(obj["id"].ToString(), int.Parse(obj["val"].ToString()), obj["text"].ToString());
		}
	}
	
	/// <summary>
	/// Leader board.
	/// </summary>
	public class LeaderBoard
	{
		public int Id { get; private set;}
		public string Name { get; private set;}
		public int Score { get; private set;}
		public float Time { get; private set;}
		
		public LeaderBoard()
		{
			this.Id = 0;
			this.Name = "";
			this.Score = 0;
			this.Time = 0.000f;
		}
		
		public LeaderBoard(int id, string name, int score, float time):base()
		{
			this.Id = id;
			this.Name = name;
			this.Score = score;
			this.Time = time;
		}
		public static LeaderBoard Create(string json)
		{
			IDictionary obj = (IDictionary)Json.Deserialize(json);
			return new LeaderBoard(int.Parse(obj["id"].ToString()), obj["name"].ToString(), int.Parse(obj["score"].ToString()), float.Parse(obj["time"].ToString()));
		}
	}
	
	/// <summary>
	/// Item.
	/// </summary>
	public class Item
	{
		public string Id { get; private set;}
		public string Name { get; private set;}
		public int Count { get; private set;}
		
		public Item()
		{
			this.Id = "";
			this.Name = "";
			this.Count = 0;
		}
		
		public Item(string id, string name, int count):base()
		{
			this.Id = id;
			this.Name = name;
			this.Count = count;
		}
		public static Item Create(string json)
		{
			IDictionary obj = (IDictionary)Json.Deserialize(json);
			return new Item(obj["id"].ToString(), obj["name"].ToString(), int.Parse(obj["count"].ToString()));
		}
	}
	
}