using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGen : MonoBehaviour {

    public int numbersOfLevels;

    [SerializeField]
    private int levelCount = 2, PreLevel;

    [SerializeField]
    private int[] LevelDistribution;
    public GameObject[] L1,L2,L3,L4;
    private bool[,] WasLevl;

    private bool no = false;

    public GameObject Levels, Player, currentLevel, lastLevel, nextLevel;

    private void Awake()
    {
        WasLevl = new bool[4, 10];
        //LevelDistribution = new int[4];
        Player = GameObject.FindGameObjectWithTag("Player");
        Levels = GameObject.FindGameObjectWithTag("levels");
        L1 = new GameObject[10];
        L2 = new GameObject[10];
        L3 = new GameObject[10];
        L4 = new GameObject[10];
        L1 = Resources.LoadAll<GameObject>("Levels/area1");
        L2 = Resources.LoadAll<GameObject>("Levels/area2");
        L3 = Resources.LoadAll<GameObject>("Levels/area3");
        L4 = Resources.LoadAll<GameObject>("Levels/area4");
        //numbersOfLevels = L1.GetLength(0);
        int x = Random.Range(0,L1.Length);
        PreLevel = x;
        GameObject levl = Instantiate(L1[x], Levels.transform) as GameObject;
        levl.transform.position = new Vector3(10, 0, 0);
        currentLevel = levl;
        //Debug.Log(number);
        int x1 = Random.Range(0, L1.Length);
        while (x1 == x)
        {
            x1 = Random.Range(0, L1.Length);
        }
        nextLevel = Instantiate(L1[x1], Levels.transform) as GameObject;
        nextLevel.transform.position = new Vector3(currentLevel.transform.position.x + currentLevel.GetComponent<BoxCollider2D>().offset.x + currentLevel.GetComponent<BoxCollider2D>().size.x/2, 0, 0);
    }
    void Update () {
        if (Player.transform.position.x >= currentLevel.GetComponent<BoxCollider2D>().offset.x + currentLevel.transform.position.x + currentLevel.GetComponent<BoxCollider2D>().size.x/2)
        {
            
            //int y = Random.Range(0, numbersOfLevels);
            SpownLevel();
            levelCount += 1;
        }
	}
    public void SpownLevel()
    {
        int number = Random.Range(0, DefineCurrentArea().Length);
        //Debug.Log(number);
        for (int i=0;i<DefineCurrentArea().Length;i++)
        {
            if (!WasLevl[DefineCurrentAreaNumber()-1,i])
            {
                //Debug.Log("itsok");
                while (WasLevl[DefineCurrentAreaNumber()-1, number] == true)
                {
                    number = Random.Range(0, DefineCurrentArea().Length);
                    //Debug.Log(number + " " + DefineCurrentArea().Length);
                }
                break;
            }
        }
        //Debug.Log(DefineCurrentArea().Length + "  " + number);
        float x = nextLevel.GetComponent<BoxCollider2D>().offset.x + nextLevel.transform.position.x + nextLevel.GetComponent<BoxCollider2D>().size.x / 2 + DefineCurrentArea()[number].GetComponent<BoxCollider2D>().size.x / 2 - DefineCurrentArea()[number].GetComponent<BoxCollider2D>().offset.x;
        WasLevl[DefineCurrentAreaNumber()-1,number] = true;
        GameObject levl = Instantiate(DefineCurrentArea()[number],Levels.transform) as GameObject;
        levl.transform.position = new Vector3(x, 0, 0);
        Destroy(lastLevel);
        lastLevel = currentLevel;
        currentLevel = nextLevel;
        nextLevel = levl;
    }
    public GameObject[] DefineCurrentArea()
    {
        if (levelCount > LevelDistribution[1] + LevelDistribution[0] + LevelDistribution[2])
        {
            return L4;
        }
        if (levelCount > LevelDistribution[0] + LevelDistribution[1])
        {
            return L3;
        }
        if (levelCount > LevelDistribution[0])
        {
            return L2;
        }
        return L1;
    }
    public int DefineCurrentAreaNumber()
    {
        if (levelCount > LevelDistribution[1] + LevelDistribution[0] + LevelDistribution[2])
        {
            return 4;
        }  
        if (levelCount > LevelDistribution[0] + LevelDistribution[1])
        {
            return 3;
        }
        if (levelCount > LevelDistribution[0])
        {
            return 2;
        }
        return 1;
    }
}
