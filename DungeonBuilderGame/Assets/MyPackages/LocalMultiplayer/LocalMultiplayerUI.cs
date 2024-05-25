using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalMultiplayerUI : MonoBehaviour, ILocalMultiplayerLobbyUI
{

    List<GameObject> playerPanels = new List<GameObject>();
    [SerializeField] GameObject playerPanelPrefab;
    [SerializeField] GridLayoutGroup playerPanelUIGrid;

    public GameObject CreatePlayerUI()
    {
        var playerPanel = Instantiate(playerPanelPrefab, playerPanelUIGrid.transform);
        playerPanels.Add(playerPanel);
        playerPanel.GetComponent<LocalPlayerPanel>().SetPlayerIDText($"Player {playerPanels.Count}"); 
        return playerPanel;
    }

    public void DestroyPlayerUI(int playerIndex, int maxPlayers)
    {
        var playerPanel = playerPanels[playerIndex];
        playerPanels.RemoveAt(playerIndex);
        Destroy(playerPanel);

        for(int i = 0; i < playerPanels.Count; i++)
        {
            playerPanels[i].GetComponent<LocalPlayerPanel>().SetPlayerIDText($"Player {i + 1}");
        }
    }
}
