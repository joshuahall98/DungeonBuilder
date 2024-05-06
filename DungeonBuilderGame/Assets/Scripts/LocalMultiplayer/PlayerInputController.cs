using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputController : MonoBehaviour
{
    Controls controls;

    void LogResult(InputAction.CallbackContext input)
    {
        Debug.Log(input.control.device);
    }

    public void SetupData(InputUser user)
    {
        controls = (Controls)user.actions;

        controls.PlayerControls.DoThing.performed += LogResult;

        EnablePlayerControls();
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
