using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerLobbyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> playerPrefabs = new List<GameObject>();
    [SerializeField] Color[] colors;
    [SerializeField] int maxPlayers;
    List<InputDevice> inputDevices;
    InputAction joinAction;
    int joinedCount;

    void Awake()
    {
        inputDevices = new List<InputDevice>();

        // Bind joinAction to any button press.
        joinAction = new InputAction(binding: "/*/<button>");
        joinAction.started += OnJoinPressed;

        BeginJoining();
    }

    void OnJoinPressed(InputAction.CallbackContext context)
    {
        JoinPlayer(context.control.device);
    }

    void JoinPlayer(InputDevice device)
    {
        if (inputDevices.Contains(device))
            return;

        inputDevices.Add(device);

        // Get a new InputUser, now paired with the device
        InputUser user = InputUser.PerformPairingWithDevice(device);

        Controls userInputs = new Controls();

        user.AssociateActionsWithUser(userInputs);

        string controlScheme = "Gamepad";

        user.ActivateControlScheme(controlScheme);

        userInputs.Enable();

        var newPlayer = Instantiate(playerPrefabs[joinedCount]);

        newPlayer.GetComponent<PlayerInputController>().SetupData(user);

        joinedCount++;

        if (joinedCount == maxPlayers)
            EndJoining();
    }

    void BeginJoining()
    {
        joinAction.Enable();
    }

    void EndJoining()
    {
        joinAction.Disable();
    }

    void OnDestroy()
    {
        joinAction.started -= OnJoinPressed;
    }
}
