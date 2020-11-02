using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolInfo
{
    public string poolName;
    public GameObject prefab;
    public int poolSize;
    public bool fixedSize;
}

class Pool
{
    private Stack<PooledObjectScript> availableObjStack = new Stack<PooledObjectScript>();

    private bool fixedSize;
    private GameObject poolObjectPrefab;
    private int poolSize;
    private string poolName;

    public Pool(string poolName, GameObject poolObjectPrefab, int initialCount, bool fixedSize)
    {
        this.poolName = poolName;
        this.poolObjectPrefab = poolObjectPrefab;
        this.poolSize = initialCount;
        this.fixedSize = fixedSize;
        //populate the pool
        for (int index = 0; index < initialCount; index++)
        {
            AddObjectToPool(NewObjectInstance());
        }
    }

    //o(1)
    private void AddObjectToPool(PooledObjectScript po)
    {
        po.gameObject.SetActive(false);
        po.gameObject.transform.parent = null; //custom

        availableObjStack.Push(po);
        po.isPooled = true;
    }

    private PooledObjectScript NewObjectInstance()
    {
        GameObject go = (GameObject)GameObject.Instantiate(poolObjectPrefab);
        PooledObjectScript po = go.GetComponent<PooledObjectScript>();
        if (po == null)
        {
            po = go.AddComponent<PooledObjectScript>();
        }
        //set name
        po.poolName = poolName;
        return po;
    }

    //o(1)
    public GameObject NextAvailableObject(Vector3 position, Quaternion rotation)
    {
        PooledObjectScript po = null;
        if (availableObjStack.Count > 0)
        {
            po = availableObjStack.Pop();
        }
        else if (fixedSize == false)
        {
            //increment size var, this is for info purpose only
            poolSize++;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////Debug.Log(string.Format("Growing pool {0}. New size: {1}",poolName,poolSize));
            //create new object
            po = NewObjectInstance();
        }
        else
        {
            //Debug.LogWarning("No object available & cannot grow pool: " + poolName);
        }

        GameObject result = null;
        if (po != null)
        {
            po.isPooled = false;
            result = po.gameObject;
            result.SetActive(true);

            result.transform.position = position;
            result.transform.rotation = rotation;
        }

        return result;
    }

    //o(1)
    public void ReturnObjectToPool(PooledObjectScript po)
    {

        if (poolName.Equals(po.poolName))
        {
            if (po.isPooled)
            {
                //Debug.LogWarning(po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage.");
            }
            else
            {
                AddObjectToPool(po);
            }

        }
        else
        {
            //Debug.LogError(string.Format("Trying to add object to incorrect pool {0} {1}", po.poolName, poolName));
        }
    }
}

public class PoolScript : MonoBehaviour
{

    public static PoolScript instance;
    public PoolInfo[] poolInfo;

    private Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

    void Start()
    {
        instance = this;
        CheckForDuplicatePoolNames();
        CreatePools();
    }

    private void CheckForDuplicatePoolNames()
    {
        for (int index = 0; index < poolInfo.Length; index++)
        {
            string poolName = poolInfo[index].poolName;
            if (poolName.Length == 0)
            {
                // Debug.LogError(string.Format("Pool {0} does not have a name!", index));
            }
            for (int internalIndex = index + 1; internalIndex < poolInfo.Length; internalIndex++)
            {
                if (poolName.Equals(poolInfo[internalIndex].poolName))
                {
                    //Debug.LogError(string.Format("Pool {0} & {1} have the same name. Assign different names.", index, internalIndex));
                }
            }
        }
    }

    private void CreatePools()
    {
        foreach (PoolInfo currentPoolInfo in poolInfo)
        {

            Pool pool = new Pool(currentPoolInfo.poolName, currentPoolInfo.prefab,
                                 currentPoolInfo.poolSize, currentPoolInfo.fixedSize);


            //Debug.Log("Creating pool: " + currentPoolInfo.poolName);
            //add to mapping dict
            poolDictionary[currentPoolInfo.poolName] = pool;
        }
    }


    /* Returns an available object from the pool 
    OR 
    null in case the pool does not have any object available & can grow size is false.
    */
    public GameObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        Debug.Log("its ok");
        if (poolDictionary.ContainsKey(poolName))
        {
            Pool pool = poolDictionary[poolName];
            result = pool.NextAvailableObject(position, rotation);
            //scenario when no available object is found in pool
            if (result == null)
            {
                //Debug.LogWarning("No object available in pool. Consider setting fixedSize to false.: " + poolName);
            }

        }
        else
        {
            //Debug.LogError("Invalid pool name specified: " + poolName);
        }

        return result;
    }

    public void ReturnObjectToPool(GameObject go)
    {
        //custom
        for (int i = 0; i < go.transform.childCount; i++)
        {
            GameObject child = go.transform.GetChild(i).gameObject;
            if (child.GetComponent<PooledObjectScript>() != null)
            {
                ReturnObjectToPool(child.gameObject);
                i--;
            }
        }
        //custom
        PooledObjectScript po = go.GetComponent<PooledObjectScript>();
        if (po == null)
        {
            //Debug.LogWarning("Specified object is not a pooled instance: " + go.name);
        }
        else
        {
            if (poolDictionary.ContainsKey(po.poolName))
            {
                Pool pool = poolDictionary[po.poolName];
                pool.ReturnObjectToPool(po);
            }
            else
            {
                //Debug.LogWarning("No pool available with name: " + po.poolName);
            }
        }
    }
}
