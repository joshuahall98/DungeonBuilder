using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class LocalPlayerCreation : MonoBehaviour, ILocalMultiplayer
{
    public void AssignNewUserInputActions(IInputActionCollection inputActions, InputUser user)
    {
        GetComponent<PlayerInputController>().SetInputActions(inputActions);

        GetComponent<PlayerDataController>().SetInputUserData(user);
    }
}
