using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public interface ILocalMultiplayerLobby
{
    public void SetupPlayerUIControls(IInputActionCollection inputActions, MultiplayerEventSystem multiplayerEventSystem, InputSystemUIInputModule inputSystemUIInputModule, GameObject playerPanel);
}
