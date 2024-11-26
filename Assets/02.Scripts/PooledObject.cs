using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public static PooledObject instance = null;

    void Awake()
    {
        if(PooledObject.instance == null)
        {
            PooledObject.instance = this;
        }
    }

    public GameObject[] poolObj_Object;
    public GameObject poolObj_Object_XRAY;
    public GameObject group_Object;
    public int poolAmount_Object;
    [HideInInspector] public List<GameObject> poolObjs_Object = new List<GameObject>();
    [HideInInspector] public List<GameObject> poolObjs_Object_XRAY = new List<GameObject>();

    public Transform spawnObjectPoint;

    private int sessionTypeNum;

    void Start()
    {
        sessionTypeNum = (int)RootManager.sessionType;

        for (int i = 0; i < poolAmount_Object; i++)
        {
            GameObject obj_Object = (GameObject)Instantiate(poolObj_Object[sessionTypeNum - 1], spawnObjectPoint.position, Quaternion.identity);
            obj_Object.name = "CoffeeCup";

            obj_Object.transform.parent = group_Object.transform;
            obj_Object.SetActive(false);
            poolObjs_Object.Add(obj_Object);

            if (RootManager.sessionType.Equals(SessionType.Move))
            {
                obj_Object = (GameObject)Instantiate(poolObj_Object_XRAY, spawnObjectPoint.position, Quaternion.identity);
                obj_Object.name = "CoffeeCupXRAY";

                obj_Object.transform.parent = group_Object.transform;
                obj_Object.SetActive(false);
                poolObjs_Object_XRAY.Add(obj_Object);
            }
        }
    }

    public GameObject GetPooledObject_Object()
    {
        for(int i = 0; i < poolObjs_Object.Count; i++)
        {
            if(!poolObjs_Object[i].activeInHierarchy)
            {
                //poolObjs_Object[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_Object[i];
            }
        }

        return null;
    }

    public GameObject GetPooledObject_Object_XRAY()
    {
        for (int i = 0; i < poolObjs_Object_XRAY.Count; i++)
        {
            if (!poolObjs_Object_XRAY[i].activeInHierarchy)
            {
                //poolObjs_Object[i].transform.SetPositionAndRotation(posi.position, Quaternion.identity);

                return poolObjs_Object_XRAY[i];
            }
        }

        return null;
    }
}
