using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SocialPlatforms;

public class LocalMultiplayerLobby : MonoBehaviour
{
    [SerializeField] List<GameObject> playerPrefabs = new List<GameObject>();
    [SerializeField] int maxPlayers;
    [SerializeField] string gamepadControlScheme;
    [SerializeField] string keyboardAndMouseControlScheme;
    List<InputDevice> previousInputDevices;
    InputAction joinAction;
    int joinedCount;

    void Awake()
    {
        previousInputDevices = new List<InputDevice>();

        // Bind joinAction to any button press.
        joinAction = new InputAction(binding: "/*/<button>");
        joinAction.performed += JoinPlayer;

        BeginJoining();
    }

    /// <summary>
    /// Call this method to add a player to the game
    /// </summary>
    void JoinPlayer(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        var inputDevices = new List<InputDevice>();

        if (previousInputDevices.Contains(device))
            return;

        string controlScheme = gamepadControlScheme;

        if (device is Mouse || device is Keyboard)
        {
            controlScheme = keyboardAndMouseControlScheme;
            previousInputDevices.Add(Keyboard.current);
            previousInputDevices.Add(Mouse.current);
            inputDevices.Add(Keyboard.current);
            inputDevices.Add(Mouse.current);
        }
        else
        {
            previousInputDevices.Add(device);
            inputDevices.Add(device);
        }

        InputUser user = InputUser.PerformPairingWithDevice(device);

        if(inputDevices.Count > 1)
        {
            foreach (InputDevice inputDevice in inputDevices)
            {
                InputUser.PerformPairingWithDevice(inputDevice, user);
            }
        }
        
        var newPlayer = Instantiate(playerPrefabs[joinedCount]);

        var userInputs = newPlayer.GetComponent<ILocalMultiplayer>().UserControls();

        user.AssociateActionsWithUser(userInputs);

        user.ActivateControlScheme(controlScheme);

        userInputs.Enable();

        newPlayer.GetComponent<ILocalMultiplayer>().NewUser(userInputs);

        joinedCount++;

        if (joinedCount >= maxPlayers)
            EndJoining();
    }

    /// <summary>
    /// Call this method to turn on the lobby functionality
    /// </summary>
    public void BeginJoining()
    {
        joinAction.Enable();
    }

    /// <summary>
    /// Call this method to turn off the lobby functionality
    /// </summary>
    public void EndJoining()
    {
        joinAction.Disable();
    }

    void OnDisable()
    {
        EndJoining();
    }

    
}
