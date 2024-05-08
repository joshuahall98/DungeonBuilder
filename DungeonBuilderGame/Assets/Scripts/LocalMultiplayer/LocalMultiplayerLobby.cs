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
    List<InputDevice> inputDevices;
    InputAction joinAction;
    int joinedCount;

    void Awake()
    {
        inputDevices = new List<InputDevice>();

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

        if (inputDevices.Contains(device))
            return;

        InputUser user = InputUser.PerformPairingWithDevice(device);

        var newPlayer = Instantiate(playerPrefabs[joinedCount]);

        var userInputs = newPlayer.GetComponent<ILocalMultiplayer>().UserControls();

        user.AssociateActionsWithUser(userInputs);

        string controlScheme = gamepadControlScheme;

        if (device is Mouse || device is Keyboard)
        {
            controlScheme = keyboardAndMouseControlScheme;
            inputDevices.Add(Keyboard.current);
            inputDevices.Add(Mouse.current);
        }
        else
        {
            inputDevices.Add(device);
        }
            
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
