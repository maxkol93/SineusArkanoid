using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputController : MonoBehaviour
{
    public static Vector2 ScreenMousePos;
    public static event EventHandler Mode1Perfomed;
    public static event EventHandler Mode2Perfomed;

    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        _gameInput.Gameplay.Mode1.performed += Mode1_performed;
        _gameInput.Gameplay.Mode2.performed += Mode2_performed;
    }

    private void Mode1_performed(InputAction.CallbackContext obj)
    {
        Mode1Perfomed?.Invoke(null, EventArgs.Empty);
    }

    private void Mode2_performed(InputAction.CallbackContext obj)
    {
        Mode2Perfomed?.Invoke(null, EventArgs.Empty);
    }

    private void Update()
    {
        //var vector2test = Mouse.current.position.value;
        var vector2 = _gameInput.Gameplay.MousePosition.ReadValue<Vector2>();
        ScreenMousePos = new Vector3(vector2.x, vector2.y);
    }
}
