using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public interface ILocalMultiplayer
{
    /// <summary>
    /// This method returns the InputActionCollection generated from the InputActionMap
    /// </summary>
    public IInputActionCollection UserControls();

    public void NewUser(IInputActionCollection InputActions);
}
