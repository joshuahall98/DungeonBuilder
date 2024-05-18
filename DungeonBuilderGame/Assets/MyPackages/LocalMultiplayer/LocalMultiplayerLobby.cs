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
    [SerializeField] string gamepadControlScheme;
    [SerializeField] string keyboardAndMouseControlScheme;
    List<InputDevice> previousInputDevices = new List<InputDevice>();
    InputAction joinAction;
    int joinedCount;
    int maxPlayers;

    IUserControls userControls;

    void Awake()
    {
        userControls = GetComponent<IUserControls>();
        maxPlayers = playerPrefabs.Count;

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

        InputUser user = InputUser.CreateUserWithoutPairedDevices();
        
        foreach (InputDevice inputDevice in inputDevices)
        {
            InputUser.PerformPairingWithDevice(inputDevice, user);
        }

        var userInputs = userControls.CreateNewUserControls();

        user.AssociateActionsWithUser(userInputs);

        user.ActivateControlScheme(controlScheme);

        userInputs.Enable();

        var newPlayer = Instantiate(playerPrefabs[joinedCount]);

        newPlayer.GetComponent<ILocalMultiplayer>().AssignNewUserInputActions(userInputs, user);

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
