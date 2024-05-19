using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputController : MonoBehaviour
{
     Controls controls;

    PlayerDataController dataController;
    PlayerMovement playerMovement;

    public event Action actionButtonEvent;

    private void Start()
    {
        dataController = GetComponent<PlayerDataController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetInputActions(IInputActionCollection inputActions)
    {
        controls = (Controls)inputActions;

        controls.PlayerControls.ActionButton.performed += ActionButton;
        controls.PlayerControls.MoveNorth.performed += MoveNorth;
        controls.PlayerControls.MoveEast.performed += MoveEast;
        controls.PlayerControls.MoveWest.performed += MoveWest;
        controls.PlayerControls.MoveSouth.performed += MoveSouth;
        
        controls.LobbyControls.StartGame.performed += StartGame;

        EnableLobbyControls();
    }

    #region -- PlayerControls --
     
    
    void ActionButton(InputAction.CallbackContext input)
    {
        actionButtonEvent?.Invoke();
    }

    void MoveNorth(InputAction.CallbackContext input)
    {
        var playerLocation = dataController.GetPlayerLocation();
        var newLocation = MovementManager.movementManagerInstance.MoveNorth(playerLocation);
        dataController.SetPlayerLocation(newLocation);
    }

    void MoveEast(InputAction.CallbackContext input)
    {
        var playerLocation = dataController.GetPlayerLocation();
        var newLocation = MovementManager.movementManagerInstance.MoveEast(playerLocation);
        dataController.SetPlayerLocation(newLocation);
    }

    void MoveWest(InputAction.CallbackContext input)
    {
        var playerLocation = dataController.GetPlayerLocation();
        var newLocation = MovementManager.movementManagerInstance.MoveWest(playerLocation);
        dataController.SetPlayerLocation(newLocation);
    }

    void MoveSouth(InputAction.CallbackContext input)
    {
        var playerLocation = dataController.GetPlayerLocation();
        var newLocation = MovementManager.movementManagerInstance.MoveSouth(playerLocation);
        dataController.SetPlayerLocation(newLocation);
    }

    #endregion

    #region -- LOBBY CONTROLS --

    void StartGame(InputAction.CallbackContext input)
    {
        GameManager.gameManagerInstance.StartGame();
    }

    #endregion

    #region -- ENABLE AND DISABLE INPUT ACTIONS --

    public void EnableAllControls()
    {
        EnableLobbyControls();
        EnablePlayerControls();
    }

    public void DisableAllControls()
    {
        DisableLobbyControls();
        DisablePlayerControls();
    }

    public void EnableLobbyControls()
    {
        controls.LobbyControls.Enable();
    }

    public void DisableLobbyControls()
    {
        controls.LobbyControls.Disable();
    }

    public void EnablePlayerControls()
    {
        controls.PlayerControls.Enable();
    }

    public void DisablePlayerControls()
    {
        controls.PlayerControls.Disable();
    }

    private void OnDisable()
    {
        DisableAllControls();
    }

    #endregion
}
