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
    public InputSystemUIInputModule inputSystemUIInputModule;
}

//TODO: Make player 1 kill lobby on exit
//TODO: Create the players


//This script handles the creation of a local multiplayer lobby with 1 keyboard and mouse and any number of controllers.
public class LocalMultiplayerLobby : MonoBehaviour
{
    [SerializeField] GameObject lobbyPlayerPrefab;
    [SerializeField] GameObject multiplayerEventSystemPrefab;
    [SerializeField] int maxPlayers;

    //Control Schemes are defined in the input actions asset
    [Header ("Control Schemes")]
    [SerializeField] string gamepadControlScheme;
    [SerializeField] string keyboardAndMouseControlScheme;

    [Header("Input Bindings")]
    [SerializeField] string joinActionGamepad = "<Gamepad>/<button>";
    [SerializeField] string joinActionKeyboard = "<Keyboard>/<button>";
    [SerializeField] string joinActionMouse = "<Mouse>/<button>";
    [SerializeField] string leaveActionGamepad = "<Gamepad>/buttonEast";
    [SerializeField] string leaveActionKeyboard = "<Keyboard>/escape";
    //[SerializeField] string leaveActionMouse = "<Mouse>/rightButton";

    List<InputDevice> inputDevicesPairedWithUsers = new List<InputDevice>();
    List<GameObject> currentLobbyPlayers = new List<GameObject>();
    List<GameObject> multiplayerEventSystems = new List<GameObject>();
    InputAction joinAction;
    InputAction leaveAction;
    int joinedCount;
    
    IUserControls userControls;
    ILocalMultiplayerLobbyUI localMultiplayerLobbyUI;

    void Awake()
    {
        userControls = GetComponent<IUserControls>();

        if(userControls == null )
        {
            Debug.LogError($"No IUserControls componenet attached to this gameobject, please attach component");
        }

        localMultiplayerLobbyUI = GetComponent<ILocalMultiplayerLobbyUI>();

        if (localMultiplayerLobbyUI == null)
        {
            Debug.LogError($"No ILocalMultiplayerLobbyUI componenet attached to this gameobject, please attach component");
        }

        // Bind joinAction to any button press.
        joinAction = new InputAction(binding: joinActionGamepad);
        joinAction.AddBinding(joinActionKeyboard);
        joinAction.AddBinding(joinActionMouse);
        joinAction.started += JoinLobby;

        // Bind leaveAction to specific button press.
        leaveAction = new InputAction(binding: leaveActionGamepad);
        leaveAction.AddBinding(leaveActionKeyboard);
       // leaveAction.AddBinding(leaveActionMouse);
        leaveAction.started += LeaveLobby;

        BeginJoining();
    }

    /// <summary>
    /// Call this method to add a player to the lobby
    /// </summary>
    void JoinLobby(InputAction.CallbackContext context)
    {
        if (joinedCount >= maxPlayers)
        {
            return;
        }

        var device = context.control.device;

        var inputDevices = new List<InputDevice>();

        if (inputDevicesPairedWithUsers.Contains(device))
            return;

        string controlScheme = ControlSchemeSetup(device, inputDevices);

        var user = InputUser.CreateUserWithoutPairedDevices();

        foreach (var inputDevice in inputDevices)
        {
            InputUser.PerformPairingWithDevice(inputDevice, user);
            inputDevicesPairedWithUsers.Add(inputDevice);
        }

        var newLobbyPlayer = Instantiate(lobbyPlayerPrefab);

        currentLobbyPlayers.Add(newLobbyPlayer);

        var userInputActions = userControls.CreateNewIInputActionCollection();

        user.AssociateActionsWithUser(userInputActions);

        user.ActivateControlScheme(controlScheme);

        userInputActions.Enable();

        var playerPanel = localMultiplayerLobbyUI.CreatePlayerUI();

        var multiplayerEventSystemObj = Instantiate(multiplayerEventSystemPrefab);
        multiplayerEventSystems.Add(multiplayerEventSystemObj);

        var multiplayerEventSystem = multiplayerEventSystemObj.GetComponent<MultiplayerEventSystem>();
        var inputSystemUIInputModule = multiplayerEventSystemObj.GetComponent<InputSystemUIInputModule>();

        var localLobbyPlayer = newLobbyPlayer.GetComponent<ILocalPlayerSetup>();

        localLobbyPlayer.SetupPlayerUIControls(userInputActions, inputSystemUIInputModule);

        if (playerPanel != null)
        {
            localLobbyPlayer.SetupPlayerPanel(playerPanel, multiplayerEventSystem);
        }

        joinedCount++;

    }

    /// <summary>
    /// Call this method to remove a player from the lobby
    /// </summary>
    void LeaveLobby(InputAction.CallbackContext context)
    {
        if(joinedCount <= 0)
        {
            //load main menu scene
            return;
        }

        var device = context.control.device;

        if (InputUser.FindUserPairedToDevice(device) == null)
        {
            return;
        }

        var userToRemove = InputUser.FindUserPairedToDevice(device).Value;

        var userIndex = userToRemove.index;

        userToRemove.actions.Disable();

        userToRemove.UnpairDevicesAndRemoveUser();

        for (int i = inputDevicesPairedWithUsers.Count - 1; i >= 0; i--) 
        {
            var pairedDevice = inputDevicesPairedWithUsers[i];

            if ((device is Mouse || device is Keyboard) && (pairedDevice is Mouse || pairedDevice is Keyboard))
            {
                inputDevicesPairedWithUsers.Remove(pairedDevice);
            }
            else if (pairedDevice == device)
            {
                inputDevicesPairedWithUsers.Remove(pairedDevice);
            }
        }

        localMultiplayerLobbyUI.DestroyPlayerUI(userIndex, maxPlayers);

        var playerToRemove = currentLobbyPlayers[userIndex];
        currentLobbyPlayers.RemoveAt(userIndex);
        Destroy(playerToRemove);

        var multiplayerEventSystemToRemove = multiplayerEventSystems[userIndex];
        multiplayerEventSystems.RemoveAt(userIndex);
        Destroy(multiplayerEventSystemToRemove);

        joinedCount--;
    }

    private string ControlSchemeSetup(InputDevice device, List<InputDevice> inputDevices)
    {
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

        return controlScheme;
    }


    /// <summary>
    /// Call this method to turn on the lobby functionality
    /// </summary>
    public void BeginJoining()
    {
        joinAction.Enable();
        leaveAction.Enable();
    }

    /// <summary>
    /// Call this method to turn off the lobby functionality
    /// </summary>
    public void EndJoining()
    {
        joinAction.Disable();
        leaveAction.Disable();
    }

    void OnDisable()
    {
        EndJoining();
    }
}
