using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILocalMultiplayerLobbyUI
{
    public GameObject CreatePlayerUI();

    public void DestroyPlayerUI(int playerIndex, int maxPlayers);
}
