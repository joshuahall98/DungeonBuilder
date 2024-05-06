using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;


//https://forum.unity.com/threads/manual-local-multiplayer-using-inputdevices.1295664/

public class PlayerController : MonoBehaviour
{
    /*public Controls controls;

    PlayerInput playerInput;

    private void Awake()
    {
        controls = new Controls();

        //InputUser user = InputUser.PerformPairingWithDevice(device)

        playerInput = GetComponent<PlayerInput>();

        Debug.Log(playerInput.user);
    }*/

    void LogResult(InputAction.CallbackContext input)
    {
        Debug.Log(input.control.device);
    }

    /*public Controls ReturnScheme()
    {
        return controls;
    }*/

    public void DoThing(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (context.performed)
        {
            Debug.Log(context.control.device);
        }
    }

    /*private void OnEnable()
    {
        controls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControls.Disable();
    }*/
}
