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

    public void DisableAllPLayerLobbyControlsAndEnableAllPlayerControls()
    {
        foreach (var controller in playerDataControllers)
        {
            controller.GetPlayerInputController().DisableLobbyControls();
            controller.GetPlayerInputController().DisableLobbyControls(); 
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
