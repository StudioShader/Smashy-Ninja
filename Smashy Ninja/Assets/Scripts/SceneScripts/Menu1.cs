using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu1 : MonoBehaviour {


	void Start () {
	
	}
	

	void Update () {
	
	}
    public void NextScene()
    {
        SceneManager.LoadScene("Menu2");
    }
}
