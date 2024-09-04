using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalEvents
{
    public static event EventHandler<ScoreAddedEventArgs> ScoreAdded;
    public static void AddScore(Vector3 position, int value)
    {
        ScoreAdded?.Invoke(null, new ScoreAddedEventArgs() { Value = value, Position = position });
    }

    public static event EventHandler<BallsLeftChangedEventArgs> BallsLeftChanged;
    public static void AttemptsLeft(int count)
    {
        BallsLeftChanged?.Invoke(null, new BallsLeftChangedEventArgs() { Count = count});
    }


    public static event EventHandler GameOver;
    public static void OnGameOver()
    {
        GameOver?.Invoke(null, EventArgs.Empty);
    }

    public static event EventHandler UpdateNormalTime;
    public static void OnUpdateNormalTime()
    {
        UpdateNormalTime?.Invoke(null, EventArgs.Empty);
    }

}

public class ScoreAddedEventArgs : EventArgs
{
    public int Value;
    public Vector3 Position;
}

public class BallsLeftChangedEventArgs : EventArgs
{
    public int Count;
}
