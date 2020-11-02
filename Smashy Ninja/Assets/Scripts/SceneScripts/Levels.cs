using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Levels : MonoBehaviour, IRewardedVideoAdListener {

    [SerializeField]
    private int numberColumn = 3, LevelsNumber = 5;
    GameObject Sn;
    [SerializeField]
    private float dX, dY, x;
    private bool sn = false;
    private GameObject lvlEx, table, table1, tableAbout;
    private const string APP_KEY = "fd96c5ebef8d139cf39db21fc54907a23e048518a53ddc4c";
	void Start () {
        //PlayerPrefs.SetInt("AllMoney", 1000);
        if (Input.GetButtonDown("z"))
        {
            ScreenCapture.CaptureScreenshot("D:\\unity\\Smashy Ninja\\ScreenShots\\com1.png");
        }
        /*tableAbout = GameObject.Find("Table about");
        tableAbout.SetActive(false);
        table = GameObject.FindGameObjectWithTag("Table");
        table1 = GameObject.FindGameObjectWithTag("Table1");
        table.SetActive(false);
        table1.SetActive(false);
        PlayerPrefs.SetInt("ExtraLevelAvailable", 0);
        FindObjectOfType<AudioManager>().Stop("Hot");
        Sn = GameObject.FindGameObjectWithTag("SmashyNinja");
        Sn.SetActive(false);
        lvlEx = GameObject.FindGameObjectWithTag("FirstLevelIcon");
        GameObject Lvl = Resources.Load<GameObject>("Buttons/LevelEx1");
        /*for (int i = 0; i < numberColumn; i++)
        {
            for (int b = 0; b < LevelsNumber; b++)
            {
                Button levl = (Instantiate(Lvl, new Vector3(lvlEx.GetComponent<RectTransform>().position.x + dX * b, lvlEx.GetComponent<RectTransform>().position.y - i * dY, lvlEx.GetComponent<RectTransform>().position.z), Quaternion.Euler(0, 0, 0)) as GameObject).GetComponent<Button>();
                levl.onClick.AddListener(openLevel);
            }
        }
        InitAds();
        */
    }
    private void InitAds()
    {
        Appodeal.disableLocationPermissionCheck();
        //Appodeal.setTesting(true);
        Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO | Appodeal.BANNER_VIEW);
        Appodeal.setRewardedVideoCallbacks(this);

    }

	/*void Update () {
        if (sn)
        {
            Sn.SetActive(true);
            x += 0.05f;
            Sn.GetComponent<Image>().color = new Color(255, 255, 255, x);
        }
	}
    public void ExtraSn()
    {
        if (PlayerPrefs.GetInt("AllMoney") >= 1000)
        {
            sn = true;
            Invoke("OpenExtraLevel", 2);
        }
        else
        {
            table.SetActive(true);
            GameObject.FindGameObjectWithTag("TextMoney").GetComponent<Text>().text = "Money: " + PlayerPrefs.GetInt("AllMoney").ToString() + "/1000";
        }
        
    }
    public void OpenAbout()
    {
        tableAbout.SetActive(true);
    }
    public void ExitAbout()
    {
        tableAbout.SetActive(false);
    }
    public void OpenExtraLevel()
    {
        SceneManager.LoadScene("Extra");
    }
    public void OpenLevel(int number)
    {
        SceneManager.LoadScene("Level" + number.ToString());
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu2");
    }
    public void EndlessModeSn()
    {
        sn = true;
        Invoke("EndlessMode",2);
    }
    public void EndlessMode()
    {
        SceneManager.LoadScene("Endless mode");
    }
    public void TutorialSn()
    {
        sn = true;
        Invoke("OpenTutorial", 2);
    }
    public void OpenTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public static bool ShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ExitTable()
    {
        table.SetActive(false);
        //if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)) Appodeal.show(Appodeal.INTERSTITIAL);
        //GameObject.FindGameObjectWithTag("debugtext").GetComponent<Text>().text = Appodeal.isLoaded(Appodeal.REWARDED_VIDEO).ToString();
    }
    public void ShowRewarded()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else
        {
            table1.SetActive(true);
        }
    }
    public void ExitTable1()
    {
        table1.SetActive(false);
    }*/
    public void onRewardedVideoLoaded() { print("Video loaded"); }
    public void onRewardedVideoFailedToLoad() { print("Video failed"); }
    public void onRewardedVideoShown() { print("Video shown"); }
    public void onRewardedVideoClosed(bool finished) { print("Video closed"); }
    public void onRewardedVideoFinished(int amount, string name) { PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") + 100); GameObject.FindGameObjectWithTag("TextMoney").GetComponent<Text>().text = "Money: " + PlayerPrefs.GetInt("AllMoney").ToString() + "/1000"; }
}
