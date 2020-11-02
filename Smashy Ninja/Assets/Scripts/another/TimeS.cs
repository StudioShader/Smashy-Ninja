using UnityEngine;
using System.Collections;

public class TimeS : MonoBehaviour {

    public float time = 0;
    GameObject Player;
	void Update () {
        Player = GameObject.FindGameObjectWithTag("Player");
        time += Time.deltaTime;
	}
}
