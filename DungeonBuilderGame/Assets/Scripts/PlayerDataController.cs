using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    [Serializable]
    private class PlayerDataContainer
    {
        public string playerName;
        public float playerHealth = 50;
    }

    [SerializeField] PlayerDataContainer playerDataContainer = new PlayerDataContainer();

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.playerManagerInstance.InsertNewPlayerIntoManager(this);
    }

    public float CheckPlayerHealth()
    {
        return playerDataContainer.playerHealth;
    }

    public string GetPlayerName()
    {
        return playerDataContainer.playerName;
    }
}
