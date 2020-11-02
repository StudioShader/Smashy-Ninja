using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {


	void Start () {
        Invoke("Levels", 8.3f);
	}
	void Levels()
    {
        SceneManager.LoadScene("Levels");
    }

}
