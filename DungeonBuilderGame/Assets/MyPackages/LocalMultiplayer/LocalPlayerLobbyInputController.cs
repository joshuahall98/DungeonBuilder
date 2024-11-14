using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LocalPlayerLobbyInputController : MonoBehaviour, ILocalPlayerSetup
{
    Controls controls;

    public void SetupPlayerUIControls(IInputActionCollection inputActions, InputSystemUIInputModule inputSystemUIInputModule)
    {
        controls = (Controls)inputActions;

       // var _inputSystemUIInputModule = inputSystemUIInputModule;

        inputSystemUIInputModule.move = InputActionReference.Create(controls.UI.Navigate);
        inputSystemUIInputModule.submit = InputActionReference.Create(controls.UI.Submit);

        //_multiplayerEventSystem = multiplayerEventSystem;
    }

    public void SetupPlayerPanel(GameObject playerPanel, MultiplayerEventSystem multiplayerEventSystem)
    {
        multiplayerEventSystem.playerRoot = playerPanel;
        multiplayerEventSystem.firstSelectedGameObject = playerPanel.GetComponent<LocalPlayerPanel>().GetReadyUpBtn().gameObject;
    }
}
