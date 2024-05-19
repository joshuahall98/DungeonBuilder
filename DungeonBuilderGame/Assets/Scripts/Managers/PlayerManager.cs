using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManagerInstance;

    [SerializeField] List<PlayerDataController> playerDataControllers = new List<PlayerDataController>(); 

    private void Awake()
    {
        playerManagerInstance = this;
    }

    public void InsertNewPlayerIntoManager(PlayerDataController playerDataController)
    {
        playerDataControllers.Add(playerDataController);
    }

    void RemovePlayerfromManager()
    {

    }

    public int GetNumberOfPlayers()
    {
        return playerDataControllers.Count;
    }

    public GameObject GetPlayerCharacter(int requestID)
    {
        GameObject playerCharacter = null;

        foreach (var controller in playerDataControllers)
        {
            var playerID = controller.GetPlayerID();
            if (playerID == requestID) 
            { 
                playerCharacter = controller.GetPlayerCharacter(); 
            }  
        }

        return playerCharacter;
    }

    public void DisablePlayerLobbyControls(int requestID)
    {
        foreach (var controller in playerDataControllers)
        {
            var playerID = controller.GetPlayerID();
            if (playerID == requestID) { controller.GetPlayerInputController().DisableLobbyControls(); }
        }
    }

    public void EnablePlayerControls(int requestID)
    {
        foreach (var controller in playerDataControllers)
        {
            var playerID = controller.GetPlayerID();
            if (playerID == requestID) { controller.GetPlayerInputController().EnablePlayerControls(); }
        }
    }

    public void DisablePlayerControls(int requestID)
    {
        foreach (var controller in playerDataControllers)
        {
            var playerID = controller.GetPlayerID();
            if (playerID == requestID) { controller.GetPlayerInputController().DisablePlayerControls(); }
        }
    }

    /*public List<float> WhatIsOtherPlayerHealth()
    {
        var listOfHealth = new List<float>();

        foreach (var controller in playerDataControllers)
        {
            var result =  controller.GetPlayerHealth();
            listOfHealth.Add(result);
            var name = controller.GetPlayerName();
            Debug.Log($"Player {name} has {result} HP");
        }

        return listOfHealth;
    }*/
}
