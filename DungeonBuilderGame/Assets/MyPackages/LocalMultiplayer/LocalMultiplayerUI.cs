using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerUI : MonoBehaviour, ILocalMultiplayerLobbyUI
{

    [SerializeField] List<GameObject> playerPanels = new List<GameObject>();

    public void EnableUI(int playerIndex)
    {
        playerPanels[playerIndex].SetActive(true); 
    }
}
