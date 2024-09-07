using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text ballsLeftLabel;
    [SerializeField] private TMP_Text startMessage;
    [SerializeField] private List<Image> hearts;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalMessage;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button exitButton;

    private int _score = 0;
    private bool _pause;
    private bool _gameOver = false;

    private void Start()
    {
        GlobalEvents.BrickDestroy += GlobalEvents_ScoreAdded;
        GlobalEvents.BallsLeftChanged += GlobalEvents_BallsLeftChanged;
        GlobalEvents.GameOver += GlobalEvents_GameOver;
        retryButton.onClick.AddListener(RetryLevel);
        nextButton.onClick.AddListener(NextLevel);
        exitButton.onClick.AddListener(() => Application.Quit());



        GameInputController.PausePerfromed += GameInputController_PausePerfromed;

        Cursor.visible = false;
        nextButton.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void GameInputController_PausePerfromed(object sender, System.EventArgs e)
    {
        if (!_gameOver)
        {
            GlobalEvents.OnSwitchPause();
            _pause = !_pause;
            Cursor.visible = !Cursor.visible;
            finalMessage.text = "Pause";
            gameOverPanel.SetActive(!gameOverPanel.activeInHierarchy);
        }
    }

    private void GlobalEvents_GameOver(object sender, GameOverEventArgs e)
    {
        _gameOver = true;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);

        if (e.IsGameWin)
        {
            finalMessage.text = "You win! Thanks for playing";
            nextButton.gameObject.SetActive(false);
            return;
        }

        if (e.IsLevelWin)
        {
            startMessage.enabled = false;
            finalMessage.text = "Level complete!";
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            finalMessage.text = "Don't give up and try again!";
            nextButton.gameObject.SetActive(false);
        }
    }

    private void RetryLevel()
    {
        GlobalEvents.OnRestartLevel();
        StartLevel();
    }

    private void NextLevel()
    {
        GlobalEvents.OnRestartLevel(true);
        StartLevel();
    }

    private void StartLevel()
    {
        foreach (var heart in hearts) heart.enabled = true;
        Cursor.visible = false;
        gameOverPanel.SetActive(false);
        ballsLeftLabel.text = "3";
        _gameOver = false;
        nextButton.gameObject.SetActive(false);
    }

    private void GlobalEvents_BallsLeftChanged(object sender, BallsLeftChangedEventArgs e)
    {
        foreach (var heart in hearts) heart.enabled = false;
        for (var i = 0; i < e.Count; i++) hearts[i].enabled = true;
        ballsLeftLabel.text = e.Count.ToString();
    }

    private void GlobalEvents_ScoreAdded(object sender, BrickDestroyEventArgs e)
    {
        _score += e.ScoreValue;
        scoreLabel.text = _score.ToString();
    }

    private void OnDestroy()
    {
        GlobalEvents.BrickDestroy -= GlobalEvents_ScoreAdded;
        GlobalEvents.BallsLeftChanged -= GlobalEvents_BallsLeftChanged;
        GlobalEvents.GameOver -= GlobalEvents_GameOver;
    }
}
