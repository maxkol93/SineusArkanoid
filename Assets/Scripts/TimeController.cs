using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TimeController : MonoBehaviour
{
    private bool _pause;
    private bool _slowmoAvailable = false;

    private Vector3 _lastMousePosition;
    private bool _superhotMode = false;

    private void Start()
    {
        GlobalEvents.BrickDestroy += GlobalEvents_ScoreAdded;
        GlobalEvents.GameOver += GlobalEvents_GameOver;
        GlobalEvents.RestartLevel += GlobalEvents_RestartLevel;
        GlobalEvents.SwitchPause += OnSwitchPause;
        _lastMousePosition = Input.mousePosition;

        //GameInputController.PausePerfromed += OnPause;
        GameInputController.Mode1Perfomed += GameInputController_Mode1Perfomed;
    }

    private void GlobalEvents_GameOver(object sender, GameOverEventArgs e)
    {
        _pause = true;
        Time.timeScale = 0f;
    }

    private void OnSwitchPause(object sender, EventArgs e)
    {
        _pause = !_pause;
        if (_pause) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    private void GlobalEvents_RestartLevel(object sender, EventArgs e)
    {
        _pause = false;
        Time.timeScale = 1f;
    }

    private void GameInputController_Mode1Perfomed(object sender, EventArgs e)
    {
        _superhotMode = !_superhotMode;
        if (_superhotMode)
        {
            _slowmoAvailable = false;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            _slowmoAvailable = true;
        }
    }

    private void Update()
    {
        if (_pause) return;
        if (!_superhotMode) 
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

    private void GlobalEvents_ScoreAdded(object sender, BrickDestroyEventArgs e)
    {
        if (_slowmoAvailable)
        {
            _slowmoAvailable = false;
            StartCoroutine(StartTimer());
            StartCoroutine(SlowTime());
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(3);
        _slowmoAvailable = true;
    }

    private IEnumerator SlowTime()
    {
        var d = 0f;
        while (d < 0.4f)
        {
            if (_pause)
            {
                break;
            }
            d += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(0.1f, 1f,  d / 0.4f);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }
    }
}
