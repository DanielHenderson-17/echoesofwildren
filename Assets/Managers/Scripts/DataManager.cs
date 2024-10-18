using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public int saveInterval = 5; // in seconds
    public UnityEvent save = new UnityEvent(); // event for saving

    private void Start()
    {
        Debug.Log("DataManager started. Timer for auto-save is running.");
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(saveInterval);
        while (true)
        {
            Debug.Log("Triggering save event from DataManager.");
            save.Invoke(); // trigger the save event every 5 seconds
            yield return delay;
        }
    }
}
