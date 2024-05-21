using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;

public interface ILocalMultiplayer
{
    /// <summary>
    /// This method accepts an Input Action Collection and so it can be assigned to the newly created user.
    /// </summary>
    public void ProvideNewUserInputActions(IInputActionCollection InputActions, InputUser user);

    /// <summary>
    /// This method accepts an InputSystemUIInputModule and so it can be assigned to the newly created user.
    /// </summary>
    public void ProvideUIInputModule(MultiplayerUI multiplayerUI);
}
