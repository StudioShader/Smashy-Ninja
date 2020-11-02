using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour {


	void Start () {
	
	}
	

	void Update () {
	
	}
    public void Back()
    {
        SceneManager.LoadScene("Menu2");
    }

}
