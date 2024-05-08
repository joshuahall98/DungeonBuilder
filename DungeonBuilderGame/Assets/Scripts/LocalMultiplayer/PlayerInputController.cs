using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputController : MonoBehaviour, ILocalMultiplayer
{
     Controls controls;

    public IInputActionCollection UserControls()
    {
        Controls controls = new Controls();

        return controls;
    }

    public void NewUser(IInputActionCollection inputActions)
    {
        controls = (Controls)inputActions;
        
        controls.PlayerControls.DoThing.performed += LogResult;

        EnablePlayerControls();
    }

    void LogResult(InputAction.CallbackContext input)
    {
        Debug.Log(input.control.device);
    }

    public void EnablePlayerControls()
    {
        controls.PlayerControls.Enable();
    }

    public void DisablePlayerControls()
    {
        controls.PlayerControls.Disable();
    }

    private void OnDisable()
    {
        DisablePlayerControls();
    }
}
