using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public int saveInterval = 5;
    public UnityEvent save = new UnityEvent();

    void Start()
    {
        StartCoroutine(TimerRoutine());
    }
    IEnumerator TimerRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(saveInterval);
        while (true)
        {
            save.Invoke();
            yield return delay;
             Debug.Log("Invoking");
        }
    }
}
