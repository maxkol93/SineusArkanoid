using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputController : MonoBehaviour
{
    public static Vector2 ScreenMousePos;
    public static event EventHandler MouseLeftClick;
    public static event EventHandler PausePerfromed;
    public static event EventHandler Mode1Perfomed;
    public static event EventHandler Mode2Perfomed;
    public static event EventHandler Mode3Perfomed;
    public static event EventHandler Mode4Perfomed;

    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        _gameInput.Gameplay.MouseLeftClick.performed += MouseLeftClick_performed;
        _gameInput.Gameplay.Pause.performed += Pause_performed;
        _gameInput.Gameplay.Mode1.performed += Mode1_performed;
        _gameInput.Gameplay.Mode2.performed += Mode2_performed;
        _gameInput.Gameplay.Mode3.performed += Mode3_performed;
        _gameInput.Gameplay.Mode4.performed += Mode4_performed;
    }

    private void Update()
    {
        var vector2 = _gameInput.Gameplay.MousePosition.ReadValue<Vector2>();
        ScreenMousePos = new Vector3(vector2.x, vector2.y);
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        PausePerfromed?.Invoke(null, EventArgs.Empty);
    }

    private void MouseLeftClick_performed(InputAction.CallbackContext obj)
    {
        MouseLeftClick?.Invoke(null, EventArgs.Empty);
    }

    private void Mode1_performed(InputAction.CallbackContext obj)
    {
        Mode1Perfomed?.Invoke(null, EventArgs.Empty);
    }

    private void Mode2_performed(InputAction.CallbackContext obj)
    {
        Mode2Perfomed?.Invoke(null, EventArgs.Empty);
    }

    private void Mode3_performed(InputAction.CallbackContext obj)
    {
        Mode3Perfomed?.Invoke(null, EventArgs.Empty);
    }

    private void Mode4_performed(InputAction.CallbackContext obj)
    {
        Mode4Perfomed?.Invoke(null, EventArgs.Empty);
    }
}
