using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Camera : MonoBehaviour {
    [SerializeField]
    GameObject Player;

    [SerializeField]
    float dX = 5;

    [SerializeField]
    float dY = 5;

    public void Awake()
    {
        transform.position = Player.transform.position + new Vector3(dX, dY, -10);
    }
    public void Update()
    {
        if (Input.GetKey("r"))
        {
            ReloadScene();
        }
        LerpTransform(new Vector3(Player.transform.position.x + dX, Player.transform.position.y + dY, -10));
        //transform.position = new Vector3(Player.transform.position.x + dX, Player.transform.position.y + dY, -10);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LerpTransform(Vector3 vector)
    {
        transform.position = transform.position + 0.5f * (vector - transform.position);
    }
}
