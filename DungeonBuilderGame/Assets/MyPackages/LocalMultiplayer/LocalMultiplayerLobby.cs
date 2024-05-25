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
//TODO: Create the inputs that will create and remove players


//This script handles the creation of a local multiplayer lobby with 1 keyboard and mouse and any number of controllers.
public class LocalMultiplayerLobby : MonoBehaviour
{
    [Tooltip("The prefabs placed in this list must contain a class that implements the ILocalMultiplayer interface.")]
    //[SerializeField] List<GameObject> lobbyPlayerPrefabs = new List<GameObject>();
    [SerializeField] GameObject lobbyPlayerPrefab;
    [SerializeField] GameObject multiplayerEventSystemPrefab;
    [SerializeField] int maxPlayers;
    //[Tooltip("The gameobjects placed in this list must contain the following components: MultiplayerEventSystem, InputSystemUIInputModule.")]
    // [SerializeField] List<MultiplayerUI> multiplayerUIs = new List<MultiplayerUI>();

    [Header ("Control Schemes")]
    [SerializeField] string gamepadControlScheme;
    [SerializeField] string keyboardAndMouseControlScheme;
    
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
        localMultiplayerLobbyUI = GetComponent<ILocalMultiplayerLobbyUI>();
       // maxPlayers = lobbyPlayerPrefabs.Count;
        //maxPlayers = (int)MathF.Min(lobbyPlayerPrefabs.Count, multiplayerUIs.Count);

        // Bind joinAction to any button press.
        joinAction = new InputAction(binding: "/*/<button>");
        joinAction.performed += JoinLobby;

        leaveAction = new InputAction(binding: "<Gamepad>/buttonEast");
        leaveAction.performed += LeaveLobby;

        BeginJoining();
        
    }

    /// <summary>
    /// Call this method to add a player to the game
    /// </summary>
    void JoinLobby(InputAction.CallbackContext context)
    {
        var device = context.control.device;

        var inputDevices = new List<InputDevice>();

        if (inputDevicesPairedWithUsers.Contains(device))
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

        newLobbyPlayer.GetComponent<ILocalMultiplayerLobby>().SetupPlayerUIControls(userInputActions, multiplayerEventSystem, inputSystemUIInputModule, playerPanel);

        //var localMultiplayerInterface = newPlayer.GetComponent<ILocalMultiplayer>();

        /*localMultiplayerInterface.ProvideUIInputModule(multiplayerUIs[joinedCount]);
        localMultiplayerInterface.ProvideNewUserInputActions(userInputs, user);*/

        joinedCount++;

        if (joinedCount >= maxPlayers)
            EndJoining();
    }

    void LeaveLobby(InputAction.CallbackContext context)
    {
        var device = context.control.device;

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
        currentLobbyPlayers.RemoveAt(userIndex);
        Destroy(multiplayerEventSystemToRemove);

        joinedCount--;
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
    }

    void OnDisable()
    {
        EndJoining();
    }

    
}
