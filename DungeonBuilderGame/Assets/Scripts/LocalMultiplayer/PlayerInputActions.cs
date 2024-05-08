using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputActions : MonoBehaviour, IInputActions
{
    public string GamepadControlScheme()
    {
        return "Gamepad";
    }

    public string KeyboardAndMouseControlScheme()
    {
        return "KBM";
    }

    public IInputActionCollection UserControls()
    {
        Controls controls = new Controls();

        return controls;
    }
}
