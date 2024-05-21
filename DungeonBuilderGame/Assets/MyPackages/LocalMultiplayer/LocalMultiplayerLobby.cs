using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;

[Serializable]
public class MultiplayerUI
{
    public MultiplayerEventSystem multiplayerEventSystem;
    public InputSystemUIInputModule inputSystemUIInputModules;
}

public class LocalMultiplayerLobby : MonoBehaviour
{
    [Tooltip("The prefabs placed in this list must contain a class that implements the ILocalMultiplayer interface.")]
    [SerializeField] List<GameObject> playerPrefabs = new List<GameObject>();
    [Tooltip("The gameobjects placed in this list must contain the following components: MultiplayerEventSystem, InputSystemUIInputModule.")]
    [SerializeField] List<MultiplayerUI> multiplayerUIs = new List<MultiplayerUI>();

    [Header ("Control Schemes")]
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
        maxPlayers = (int)MathF.Min(playerPrefabs.Count, multiplayerUIs.Count);

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
            inputDevices.Add(Keyboard.current);
            inputDevices.Add(Mouse.current);
        }
        else
        {
            inputDevices.Add(device);
        }

        InputUser user = InputUser.CreateUserWithoutPairedDevices();
        
        foreach (InputDevice inputDevice in inputDevices)
        {
            InputUser.PerformPairingWithDevice(inputDevice, user);
            previousInputDevices.Add(inputDevice);
        }

        var userInputs = userControls.CreateNewIInputActionCollection();

        user.AssociateActionsWithUser(userInputs);

        user.ActivateControlScheme(controlScheme);

        userInputs.Enable();

        var newPlayer = Instantiate(playerPrefabs[joinedCount]);

        var localMultiplayerInterface = newPlayer.GetComponent<ILocalMultiplayer>();

        localMultiplayerInterface.ProvideUIInputModule(multiplayerUIs[joinedCount]);
        localMultiplayerInterface.ProvideNewUserInputActions(userInputs, user);
        
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
