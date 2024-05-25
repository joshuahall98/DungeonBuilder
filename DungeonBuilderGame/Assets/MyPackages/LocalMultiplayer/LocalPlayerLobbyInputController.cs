using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LocalPlayerLobbyInputController : MonoBehaviour, ILocalMultiplayerLobby
{
    Controls controls;

    InputSystemUIInputModule _inputSystemUIInputModule;
    MultiplayerEventSystem _multiplayerEventSystem;

    public void SetupPlayerUIControls(IInputActionCollection inputActions, MultiplayerEventSystem multiplayerEventSystem, InputSystemUIInputModule inputSystemUIInputModule)
    {
        controls = (Controls)inputActions;

        _inputSystemUIInputModule = inputSystemUIInputModule;

        _inputSystemUIInputModule.move = InputActionReference.Create(controls.UI.Navigate);
        _inputSystemUIInputModule.submit = InputActionReference.Create(controls.UI.Submit);

        _multiplayerEventSystem = multiplayerEventSystem;
    }

    public void SetupPlayerPanel(GameObject playerPanel)
    {
        _multiplayerEventSystem.playerRoot = playerPanel;
        _multiplayerEventSystem.firstSelectedGameObject = playerPanel.GetComponent<LocalPlayerPanel>().GetReadyUpBtn().gameObject;
    }
}
