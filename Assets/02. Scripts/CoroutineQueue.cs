using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineQueue : MonoBehaviour
{
    private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
    private bool isRunning = false;

    public void Enqueue(IEnumerator coroutine)
    {
        coroutineQueue.Enqueue(coroutine);
        if (!isRunning)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    private IEnumerator ProcessQueue()
    {
        isRunning = true;

        while (coroutineQueue.Count > 0)
        {
            IEnumerator current = coroutineQueue.Dequeue();
            yield return StartCoroutine(current);
        }

        isRunning = false;
    }
}