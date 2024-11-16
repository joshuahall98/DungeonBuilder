using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


[Serializable]
public class MultiplayerUI
{
    public MultiplayerEventSystem multiplayerEventSystem;
    public InputSystemUIInputModule inputSystemUIInputModule;
}

//TODO: Make player 1 kill lobby on exit
//TODO: Create the players

public class LocalMultiplayerLobby : MonoBehaviour
{
    [SerializeField] GameObject lobbyPlayerPrefab;
    [SerializeField] GameObject multiplayerEventSystemPrefab;
    [SerializeField] int maxPlayers = 2;
    [SerializeField] InputActionAsset inputActionAsset;

    //Control Schemes are defined in the input actions asset
    [Header("Control Schemes")]
    [SerializeField] string gamepadControlScheme;
    [SerializeField] string keyboardAndMouseControlScheme;

    [Header("Input Bindings")]
    [SerializeField] string joinActionGamepad = "<Gamepad>/<button>";
    [SerializeField] string joinActionKeyboard = "<Keyboard>/<button>";
    [SerializeField] string joinActionMouse = "<Mouse>/<button>";
    [SerializeField] string leaveActionGamepad = "<Gamepad>/buttonEast";
    [SerializeField] string leaveActionKeyboard = "<Keyboard>/escape";

    List<GameObject> currentLobbyPlayers = new List<GameObject>();
    List<GameObject> multiplayerEventSystems = new List<GameObject>();

    InputAction joinAction;
    InputAction leaveAction;
    int joinedCount;

    ILocalMultiplayerLobbyUI localMultiplayerLobbyUI;

    void Awake()
    {
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
        leaveAction.started += LeaveLobby;

        BeginJoining();
    }

    private void JoinLobby(InputAction.CallbackContext context)
    {

        if (joinedCount >= maxPlayers)
        {
            return;
        }

        var tuple = LocalMultiplayerUserCreationSystem.CreateUser(context, inputActionAsset);

        var newUserInputActions = tuple.Item1;
        var userCreated = tuple.Item2;
        
        if (!userCreated)
        {
            return;
        }

        var newLobbyPlayer = Instantiate(lobbyPlayerPrefab);

        currentLobbyPlayers.Add(newLobbyPlayer);

        var playerPanel = localMultiplayerLobbyUI.CreatePlayerUI();

        var multiplayerEventSystemObj = Instantiate(multiplayerEventSystemPrefab);
        multiplayerEventSystems.Add(multiplayerEventSystemObj);

        var multiplayerEventSystem = multiplayerEventSystemObj.GetComponent<MultiplayerEventSystem>();
        var inputSystemUIInputModule = multiplayerEventSystemObj.GetComponent<InputSystemUIInputModule>();

        var localLobbyPlayer = newLobbyPlayer.GetComponent<ILocalPlayerSetup>();

        localLobbyPlayer.SetupPlayerUIControls(newUserInputActions, inputSystemUIInputModule);

        if (playerPanel != null)
        {
            localLobbyPlayer.SetupPlayerPanel(playerPanel, multiplayerEventSystem);
        }

        joinedCount++;
    }

    private void LeaveLobby(InputAction.CallbackContext context)
    {

        if (joinedCount <= 0)
        {
            //load main menu scene
            return;
        }

        var userIndex = LocalMultiplayerUserCreationSystem.DeleteUser(context);

        localMultiplayerLobbyUI.DestroyPlayerUI(userIndex, maxPlayers);

        var playerToRemove = currentLobbyPlayers[userIndex];
        currentLobbyPlayers.RemoveAt(userIndex);
        Destroy(playerToRemove);

        var multiplayerEventSystemToRemove = multiplayerEventSystems[userIndex];
        multiplayerEventSystems.RemoveAt(userIndex);
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
        leaveAction.Disable();
    }

    void OnDisable()
    {
        EndJoining();
    }
}
