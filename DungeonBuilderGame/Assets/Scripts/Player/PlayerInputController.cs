using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputController : MonoBehaviour
{
     Controls controls;

    PlayerDataController dataController;

    private void Start()
    {
        dataController = GetComponent<PlayerDataController>();
    }

    public void SetInputActions(IInputActionCollection inputActions)
    {
        controls = (Controls)inputActions;

        controls.PlayerControls.DoThing.performed += LogResult;
        controls.PlayerControls.MoveNorth.performed += MoveNorth;
        controls.PlayerControls.MoveEast.performed += MoveEast;
        controls.PlayerControls.MoveWest.performed += MoveWest;
        controls.PlayerControls.MoveSouth.performed += MoveSouth;
        
        controls.LobbyControls.StartGame.performed += StartGame;

        EnableLobbyControls();
    }

    #region -- PlayerControls --

    void LogResult(InputAction.CallbackContext input)
    {
        //PlayerManager.playerManagerInstance.WhatIsOtherPlayerHealth();
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
        PlayerManager.playerManagerInstance.DisableAllPLayerLobbyControlsAndEnableAllPlayerControls();

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
