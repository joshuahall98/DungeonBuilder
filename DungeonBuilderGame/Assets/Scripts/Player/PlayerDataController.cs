using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class PlayerDataController : MonoBehaviour
{
    [Serializable]
    private class PlayerDataContainer
    {
        public int userID = 0;
        public float playerHealth = 50;
        public PlayerInputController controller;
        public Vector3Int currentTile = new Vector3Int(0,0,0);
    }

    [SerializeField] PlayerDataContainer playerDataContainer = new PlayerDataContainer();

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.playerManagerInstance.InsertNewPlayerIntoManager(this);
        playerDataContainer.controller = GetComponent<PlayerInputController>();
    }

    public void SetInputUserData(InputUser user)
    {
        playerDataContainer.userID = (int)user.id;
    }

    public float GetPlayerHealth()
    {
        return playerDataContainer.playerHealth;
    }

    public int GetPlayerName()
    {
        return playerDataContainer.userID;
    }

    public PlayerInputController GetPlayerInputController()
    {
        return playerDataContainer.controller;
    }

    public void SetPlayerLocation(Vector3Int newLocation)
    {
        playerDataContainer.currentTile = newLocation;
    }

    public Vector3Int GetPlayerLocation()
    {
        return playerDataContainer.currentTile;
    }
}
