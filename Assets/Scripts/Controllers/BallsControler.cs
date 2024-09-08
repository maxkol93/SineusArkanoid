using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BallsControler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform carriage;
    [SerializeField] private Transform forcePivot;
    [SerializeField] private Transform ballPivot;
    [SerializeField] private int attemptsCount = 3;

    private int _attemptsLeft = 3;
    private List<Ball> _balls = new List<Ball>();

    private void Start()
    {
        GlobalEvents.RestartLevel += GlobalEvents_RestartLevel;
        GlobalEvents.GameOver += GlobalEvents_GameOver;

        _attemptsLeft = attemptsCount;
        AddBall();
    }

    public void AddBall()
    {
        var ball = Instantiate(ballPrefab, ballPivot.position, Quaternion.identity).GetComponent<Ball>();
        ball.ForcePivot = forcePivot;
        ball.BallsControler = this;
        _balls.Add(ball);
        ball.PlaceOnCarriage(carriage);
    }

    public void BallOutside(Ball ball)
    {
        if (_balls.Count > 1)
        {
            Debug.Log($"Ball destroy, balls count = {_balls.Count}");
            _balls.Remove(ball);
            Destroy(ball.gameObject);
            return;
        }

        _attemptsLeft--;
        GlobalEvents.AttemptsLeft(_attemptsLeft);
        if (_attemptsLeft > 0)
        {
            ball.transform.position = ballPivot.position;
            ball.PlaceOnCarriage(carriage);
        }
        else
        {
            _balls.Remove(ball);
            Destroy(ball.gameObject);
            GlobalEvents.OnGameOver(false);
        }
    }

    public void ChangeForce(float force, float duration)
    {
        foreach (var ball in _balls)
        {
            ball.ChangeForce(force, duration);
        }
    }

    private void GlobalEvents_GameOver(object sender, GameOverEventArgs e)
    {
        for (int i = _balls.Count - 1; i >= 0; i--)
        {
            Destroy(_balls[i].gameObject);
        }
        _balls.Clear();
    }

    private void GlobalEvents_RestartLevel(object sender, System.EventArgs e)
    {
        for (int i = _balls.Count - 1; i >= 0; i--)
        {
            Destroy(_balls[i].gameObject);
        }
        _balls.Clear();
        _attemptsLeft = attemptsCount;
        AddBall();
    }
}
