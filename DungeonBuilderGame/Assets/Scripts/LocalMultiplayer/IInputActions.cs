using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputActions
{
    /// <summary>
    /// This method returns the InputActionCollection generated from the InputActionMap
    /// </summary>
    public IInputActionCollection UserControls();
    /// <summary>
    /// Returns the name of the Gamepad control scheme in the Input Action Map
    /// </summary>
    public string GamepadControlScheme();
    /// <summary>
    /// Returns the name of the Keyboard and Mouse control scheme in the Input Action Map
    /// </summary>
    public string KeyboardAndMouseControlScheme();
}
