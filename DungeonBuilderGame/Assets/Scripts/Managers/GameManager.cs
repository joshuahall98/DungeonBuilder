using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    int numberOfPlayers;
    int currentPlayerTurn = 1;

    private void Awake()
    {
        gameManagerInstance = this;
    }

    public void StartGame()
    {
        numberOfPlayers = PlayerManager.playerManagerInstance.GetNumberOfPlayers();

        for (int i = 1; i <= numberOfPlayers; i++)
        {
            PlayerManager.playerManagerInstance.DisablePlayerLobbyControls(i);
            PlayerManager.playerManagerInstance.EnablePlayerControls(i);

            SpawnCharacter(i);
        }

        CurrentPlayersTurn();
    }

    private void SpawnCharacter(int playerID)
    {
        var playerCharacter = PlayerManager.playerManagerInstance.GetPlayerCharacter(playerID);

        switch (playerID)
        {
            case 1:
                Instantiate(playerCharacter, new Vector3(1, 0, 1), Quaternion.identity);
                break;
            case 2:
                Instantiate(playerCharacter, new Vector3(5, 0, 1), Quaternion.identity);
                break;
            case 3:
                Instantiate(playerCharacter, new Vector3(1, 0, 5), Quaternion.identity);
                break;
            case 4:
                Instantiate(playerCharacter, new Vector3(5, 0, 5), Quaternion.identity);
                break;
            default:
                return;

        }
    }

    private void CurrentPlayersTurn()
    {
        for (int i = 1; i <= numberOfPlayers; i++) 
        {
            if(i != currentPlayerTurn)
            {
                PlayerManager.playerManagerInstance.DisablePlayerControls(i);
            }
            else
            {
                PlayerManager.playerManagerInstance.EnablePlayerControls(i);
            }
        }

        Debug.Log($"It is player {currentPlayerTurn} turn");
    }
}
