using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CountdownScript: MonoBehaviour
{
    public delegate void CountdownCompleted();
    public event CountdownCompleted OnCountdownCompleted;

    public void StartCountdown(float time)
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
        OnCountdownCompleted?.Invoke();
    }
}
