using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action JumpPressed;

    void Update()
    {
        bool isPressed = Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0);

        if (isPressed)
        {
            JumpPressed?.Invoke();
        }
    }
}
