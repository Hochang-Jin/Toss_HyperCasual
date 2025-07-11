using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Queue<GameObject> objectPool = new Queue<GameObject>();
    
    public GameObject fruit;
    public GameObject parent;

    private void Start()
    {
        CreateObject();
    }

    public void CreateObject()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject go = Instantiate(fruit, parent.transform);
            EnqueueObject(go);
        }
    }

    public void EnqueueObject(GameObject go)
    {
        objectPool.Enqueue(go);
        go.SetActive(false);
    }

    public GameObject DequeueObject()
    {
        if(objectPool.Count < 10)
            CreateObject();
        
        GameObject go = objectPool.Dequeue();
        go.SetActive(true);
        return go;
    }

    public void PoolReset()
    {
        Fruits[] children = parent.GetComponentsInChildren<Fruits>();
        foreach (var vChild in children)
        {
            EnqueueObject(vChild.gameObject);
        }
    }
}

