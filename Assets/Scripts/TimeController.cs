using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;

public class TimeController : MonoBehaviour
{
    private bool slowmoAvailable = false;

    private Vector3 _lastMousePosition;
    private bool _mode1;

    private void Start()
    {
        GlobalEvents.ScoreAdded += GlobalEvents_ScoreAdded;
        _lastMousePosition = Input.mousePosition;

        GameInputController.Mode1Perfomed += GameInputController_Mode1Perfomed;
        GameInputController.Mode2Perfomed += GameInputController_Mode2Perfomed;
    }

    private void GameInputController_Mode1Perfomed(object sender, EventArgs e)
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        _mode1 = true;
    }

    private void GameInputController_Mode2Perfomed(object sender, EventArgs e)
    {
        _mode1 = false;
    }

    private void Update()
    {
        if (_mode1) 
        {
            GlobalEvents.OnUpdateNormalTime();
        }
        else
        {
            var pos = Input.mousePosition;
            var distance = Vector3.Distance(_lastMousePosition, pos);
            if (distance > 0.01)
            {
                Time.timeScale = 0.5f + 0.5f * Mathf.Min(distance / 7, 50);
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                GlobalEvents.OnUpdateNormalTime();
            }
            else
            {
                Time.timeScale = 0.03f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                GlobalEvents.OnUpdateNormalTime();
            }
            _lastMousePosition = pos;
        }
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
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }
    }
}
