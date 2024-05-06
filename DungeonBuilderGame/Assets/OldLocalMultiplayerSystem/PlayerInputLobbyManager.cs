using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputLobbyManager : MonoBehaviour
{

    //[SerializeField] HorizontalLayoutGroup horizontalLayoutGroup;
    //[SerializeField] LocalJoiner joinerPrefab;
    [SerializeField] List<GameObject> playerPrefabs = new List<GameObject>();
    [SerializeField] Color[] colors;
    [SerializeField] int maxPlayers;
    List<InputDevice> inputDevices;
    InputAction joinAction;
    int joinedCount;

    [SerializeField]PlayerInputManager playerInputManager;

    void Awake()
    {
        inputDevices = new List<InputDevice>();

        // Using this event, you cannot set the parent or transform of what you instantiate
        //InputUser.onUnpairedDeviceUsed += JoinPlayer;

        // Bind joinAction to any button press.
        joinAction = new InputAction(binding: "/*/<button>");
        joinAction.started += OnJoinPressed;

        BeginJoining(); 
    }

    void OnJoinPressed(InputAction.CallbackContext context)
    {
        JoinPlayer(context.control.device);
    }

    //KBM is treated separately sometyimes
    void JoinPlayer(InputDevice device)
    {
        if (inputDevices.Contains(device))
            return;

        inputDevices.Add(device);

        PlayerInput p = PlayerInput.Instantiate(playerPrefabs[joinedCount], -1, null, -1, device);
        Debug.Log(p.currentControlScheme);

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
