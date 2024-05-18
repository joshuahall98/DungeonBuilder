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
    }

    [SerializeField] PlayerDataContainer playerDataContainer = new PlayerDataContainer();

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.playerManagerInstance.InsertNewPlayerIntoManager(this);
    }

    public void AssignInputUserData(InputUser user)
    {
        playerDataContainer.userID = (int)user.id;
    }

    public float CheckPlayerHealth()
    {
        return playerDataContainer.playerHealth;
    }

    public int GetPlayerName()
    {
        return playerDataContainer.userID;
    }
}
