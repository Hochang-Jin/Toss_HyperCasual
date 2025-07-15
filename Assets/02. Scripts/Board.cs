using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private float targetTime = 0.3f;
    public CoroutineQueue coroutines;

    private void Start()
    {
        coroutines = gameObject.GetComponent<CoroutineQueue>();
    }

    public void Rotation(bool isLeft)
    {
        coroutines.Enqueue(RotateRoutine(isLeft));
    }

    IEnumerator RotateRoutine(bool isLeft)
    {
        float rotation = isLeft ? 90 : -90;
        float timer = 0f;
        
        Quaternion startRot = transform.rotation;
        Quaternion endRot = transform.rotation * Quaternion.Euler(0, 0, rotation);
        
        while (timer < targetTime)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, timer / targetTime);
            timer += Time.deltaTime;
            yield return null;
        }
        
        transform.rotation = endRot;
    }
}
