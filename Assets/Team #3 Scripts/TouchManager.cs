using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction TouchPosition;
    private InputAction TouchPress;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        TouchPress = playerInput.actions["TouchPress"];
        TouchPosition = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        TouchPress.performed += TouchPressed;
    }

    private void OnDisable()
    {
        TouchPress.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);


    }
}
