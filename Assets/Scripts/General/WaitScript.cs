using System.Collections;
using UnityEngine;
public class WaitScript: MonoBehaviour
{
    public delegate void WaitCompleted();
    public event WaitCompleted OnWaitCompleted;

    public void StartWait(float time)
    {
        StartCoroutine(Countdown(time));
    }

    IEnumerator Countdown(float time)
    {
        float currentTime = time;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }
        OnWaitCompleted?.Invoke();
    }
}
