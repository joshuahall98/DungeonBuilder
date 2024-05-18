using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public interface ILocalMultiplayer
{
    /// <summary>
    /// This method accepts an Input Action Collection and assigns it to the newly created user
    /// </summary>
    public void AssignNewUserInputActions(IInputActionCollection InputActions);
}
