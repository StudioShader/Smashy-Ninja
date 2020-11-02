using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour {
    [SerializeField]
    private float PrePosX = 0, SumDelta, first, second, third, speed = 0.4f;

    private bool WasTouch;

    private float mainHome;

    private GameObject text, Money;
	void Start () {
        mainHome = first;
        mainHome = PlayerPrefs.GetInt("MainHome");
        gameObject.transform.position = new Vector3(mainHome, transform.position.y, -100);
        text = GameObject.FindGameObjectWithTag("debugtext");
        Money = GameObject.FindGameObjectWithTag("Money");
    }
	
	void Update () {
        Money.GetComponent<Text>().text = PlayerPrefs.GetInt("AllMoney").ToString();
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                transform.position -= new Vector3(Input.GetTouch(0).deltaPosition.x * speed, transform.position.y, 0);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - (transform.position.x - Nearest()) * 0.6f, transform.position.y, -100);
            }

        }
        else
        {
        }
        //text.GetComponent<Text>().text = Nearest().ToString();
    }
    private float Nearest()
    {
        if (Mathf.Abs(transform.position.x - first) < Mathf.Abs(transform.position.x - second) && Mathf.Abs(transform.position.x - first) < Mathf.Abs(transform.position.x - third))
        {
            return first;
        }
        if (Mathf.Abs(transform.position.x - second) < Mathf.Abs(transform.position.x - first) && Mathf.Abs(transform.position.x - second) < Mathf.Abs(transform.position.x - third))
        {
            return second;
        }
        if (Mathf.Abs(transform.position.x - third) < Mathf.Abs(transform.position.x - second) && Mathf.Abs(transform.position.x - third) < Mathf.Abs(transform.position.x - first))
        {
            return third;
        }
        else return mainHome;
    }
    public void VisitHome()
    {
        if(Nearest() == first)
        {
            SceneManager.LoadScene("Home1");
        }
    }
    public void Levels()
    {
        SceneManager.LoadScene("Levels");
    }
    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }
}
