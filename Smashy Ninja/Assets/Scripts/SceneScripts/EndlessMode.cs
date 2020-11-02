using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndlessMode : MonoBehaviour {

    private GameObject stopMenu, Player, Sn, app;

    private int a;

    [SerializeField]
    private float x = 0, y = 1;

    void Start () {
        app = GameObject.FindGameObjectWithTag("ADManager");
        FindObjectOfType<AudioManager>().Continue("Hot");
        Sn = GameObject.FindGameObjectWithTag("SmashyNinja");
        Invoke("SmashyNinjaOff", 0.4f);
        stopMenu = GameObject.FindGameObjectWithTag("StopMenu");
        GameObject.FindGameObjectWithTag("StopMenu").SetActive(false);

        Time.timeScale = 1;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	

	void Update () {
        //Application.CaptureScreenshot("D:\\unity\\Smashy Ninja\\ScreenShots\\Video" + "\\Video" + a + ".png");
        //a++;
        //Debug.Log(PlayerPrefs.GetInt("NRuns"));
        if (Player.GetComponent<Player>().dead)
        {
            Sn.SetActive(true);
            x += 0.05f;
            Sn.GetComponent<Image>().color = new Color(255,255,255,x);
            Invoke("loadScene",0.6f);
        }
        else
        {
            if(y >= 0){
                y -= 0.05f;
                
            }
            Sn.GetComponent<Image>().color = new Color(255, 255, 255, y);
        }
    }
    public void OpenMenu()
    {
        FindObjectOfType<AudioManager>().Stop("Hot");
        Time.timeScale = 0;
        stopMenu.SetActive(true);
    }
    public void CloseMenu()
    {
        FindObjectOfType<AudioManager>().Continue("Hot");
        Time.timeScale = 1;
        stopMenu.SetActive(false);
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Levels");
        Time.timeScale = 1;
    }
    public void loadScene()
    {
        //Debug.Log(PlayerPrefs.GetInt("NRuns"));
        //GameObject.FindGameObjectWithTag("debugtext").GetComponent<Text>().text = Levels.ShowInterstitial() + " " + PlayerPrefs.GetInt("NRuns");
        if (PlayerPrefs.GetInt("NRuns") == 20) {
            //app.GetComponent<AdManager>().ShowBanner();
            //Levels.ShowInterstitial();
            PlayerPrefs.SetInt("NRuns",0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            PlayerPrefs.SetInt("NRuns", PlayerPrefs.GetInt("NRuns") + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void SmashyNinjaOff()
    {
        Sn.SetActive(false);
    }

}
