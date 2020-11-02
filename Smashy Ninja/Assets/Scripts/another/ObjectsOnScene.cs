using UnityEngine;
using System.Collections;

public class ObjectsOnScene : MonoBehaviour {

    [SerializeField]
    private GameObject[] objects;

    private GameObject Player;

    [SerializeField]
    private float dX;
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        objects = new GameObject[20];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            objects[i] = transform.GetChild(i).gameObject;
            objects[i].SetActive(false);
        }
    }
	

	void Update () {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (Mathf.Abs(Player.transform.position.x - objects[i].transform.position.x) < dX)
            {
                objects[i].SetActive(true);
            }
            else
            {
                objects[i].SetActive(false);
            }
        }
	}
}
