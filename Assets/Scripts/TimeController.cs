using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private bool slowmoAvailable = true;

    private void Start()
    {
        GlobalEvents.ScoreAdded += GlobalEvents_ScoreAdded;
    }

    private void GlobalEvents_ScoreAdded(object sender, ScoreAddedEventArgs e)
    {
        if (slowmoAvailable)
        {
            slowmoAvailable = false;
            StartCoroutine(StartTimer());
            StartCoroutine(SlowTime());
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(3);
        slowmoAvailable = true;
    }

    private IEnumerator SlowTime()
    {
        var d = 0f;
        while (d < 0.4f)
        {
            d += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(0.1f, 1f,  d / 0.4f);

            //Time.timeScale = 1.1f - d;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }
    }
}
