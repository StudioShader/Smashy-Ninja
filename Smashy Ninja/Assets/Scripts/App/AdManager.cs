using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdManager : MonoBehaviour, IRewardedVideoAdListener {
    private const string APP_KEY = "fd96c5ebef8d139cf39db21fc54907a23e048518a53ddc4c";
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        InitAds();
    }
    private void InitAds()
    {
        //Appodeal.disableLocationPermissionCheck();
        //Appodeal.setTesting(true);
        Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO | Appodeal.BANNER_VIEW);
        Appodeal.setRewardedVideoCallbacks(this);
    }
    public void ShowInterstitial()
    {
        GameObject.FindGameObjectWithTag("debugtext").GetComponent<Text>().text = "Show interstitial";
        //GameObject.FindGameObjectWithTag("debugtext").GetComponent<Text>().text = Appodeal.isLoaded(Appodeal.REWARDED_VIDEO).ToString();
        Appodeal.show(Appodeal.REWARDED_VIDEO);
    }
    public void ShowBanner()
    {
        if (Appodeal.isLoaded(Appodeal.BANNER_VIEW))
        {
            Appodeal.show(Appodeal.BANNER_VIEW);
        }
    }
    public void ShowRewarded()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }
    #region Rewarded Video callback handlers
    public void onRewardedVideoLoaded() { print("Video loaded"); }
    public void onRewardedVideoFailedToLoad() { GameObject.FindGameObjectWithTag("debugtext").GetComponent<Text>().text = "failed to load rewarded video"; }
    public void onRewardedVideoShown() { print("Video shown"); }
    public void onRewardedVideoClosed(bool finished) { GameObject.FindGameObjectWithTag("debugtext").GetComponent<Text>().text = "Ad closed and " + (finished ? "finished": "not finished"); }
    public void onRewardedVideoFinished(int amount, string name) { PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") + 100); }
    #endregion
}
