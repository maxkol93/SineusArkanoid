using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalEvents
{
    public static event EventHandler<BrickDestroyEventArgs> BrickDestroy;
    public static void OnBrickDestroy(Vector3 position, int value, bool isLast)
    {
        BrickDestroy?.Invoke(null, new BrickDestroyEventArgs() { ScoreValue = value, Position = position, IsLastBrick = isLast });
    }

    public static event EventHandler<BallsLeftChangedEventArgs> BallsLeftChanged;
    public static void AttemptsLeft(int count)
    {
        BallsLeftChanged?.Invoke(null, new BallsLeftChangedEventArgs() { Count = count });
    }

    public static event EventHandler<GameOverEventArgs> GameOver;
    public static void OnGameOver(bool isLevelWin, bool isGameWin = false)
    {
        GameOver?.Invoke(null, new GameOverEventArgs() { IsLevelWin = isLevelWin, IsGameWin = isGameWin });
    }

    public static event EventHandler UpdateNormalTime;
    public static void OnUpdateNormalTime()
    {
        UpdateNormalTime?.Invoke(null, EventArgs.Empty);
    }

    public static event EventHandler<ResartLevelEventArgs> RestartLevel;
    public static void OnRestartLevel(bool nextLevel = false)
    {
        RestartLevel?.Invoke(null, new ResartLevelEventArgs() { NextLevel = nextLevel });
    }

    public static event EventHandler SwitchPause;
    public static void OnSwitchPause()
    {
        SwitchPause?.Invoke(null, EventArgs.Empty);
    }
}

public class BrickDestroyEventArgs : EventArgs
{
    public int ScoreValue;
    public Vector3 Position;
    public bool IsLastBrick;
}

public class BallsLeftChangedEventArgs : EventArgs
{
    public int Count;
}

public class GameOverEventArgs : EventArgs
{
    public bool IsLevelWin;
    public bool IsGameWin;
}

public class ResartLevelEventArgs : EventArgs
{
    public bool NextLevel;
}
