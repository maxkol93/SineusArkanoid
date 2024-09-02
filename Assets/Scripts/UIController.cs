using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text ballsLeftLabel;

    private int _score = 0;

    private void Start()
    {
        GlobalEvents.ScoreAdded += GlobalEvents_ScoreAdded;
        GlobalEvents.BallsLeftChanged += GlobalEvents_BallsLeftChanged;
    }

    private void GlobalEvents_BallsLeftChanged(object sender, BallsLeftChangedEventArgs e)
    {
        ballsLeftLabel.text = e.Count.ToString();
    }

    private void GlobalEvents_ScoreAdded(object sender, ScoreAddedEventArgs e)
    {
        _score += e.Value;
        scoreLabel.text = _score.ToString();
    }

    private void OnDestroy()
    {
        GlobalEvents.ScoreAdded -= GlobalEvents_ScoreAdded;
        GlobalEvents.BallsLeftChanged -= GlobalEvents_BallsLeftChanged;
    }
}
