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
    [SerializeField] private int attemptsLeft = 3;

    private List<Ball> _balls = new List<Ball>();

    private void Start()
    {
        AddBall();
    }

    public void AddBall()
    {
        var ball = Instantiate(ballPrefab, ballPivot.position, Quaternion.identity).GetComponent<Ball>();
        ball.forcePivot = forcePivot;
        ball.ballsControler = this;
        _balls.Add(ball);
        ball.PlaceOnCarriage(carriage);
    }

    public void BallOutside(Ball ball)
    {
        if (_balls.Count > 1)
        {
            _balls.Remove(ball);
            Destroy(ball);
            return;
        }

        attemptsLeft--;
        if (attemptsLeft == 0)
        {
            GlobalEvents.OnGameOver();
        }
        GlobalEvents.AttemptsLeft(attemptsLeft);

        ball.transform.position = ballPivot.position;
        ball.PlaceOnCarriage(carriage);
    }
}
