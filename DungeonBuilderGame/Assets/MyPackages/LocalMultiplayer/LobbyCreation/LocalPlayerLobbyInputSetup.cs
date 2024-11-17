using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LocalPlayerLobbyInputSetup : MonoBehaviour, ILocalPlayerLobbyInputSetup
{
    [SerializeField] InputActionReference submit;
    //[SerializeField] InputActionReference move;

    public void SetupPlayerPanel(GameObject playerPanel, MultiplayerEventSystem multiplayerEventSystem)
    {
        multiplayerEventSystem.playerRoot = playerPanel;
        multiplayerEventSystem.firstSelectedGameObject = playerPanel.GetComponent<LocalPlayerLobbyPanel>().GetReadyUpBtn().gameObject;
    }

    public void SetupPlayerUIControls(InputActionAsset inputActions, InputSystemUIInputModule inputSystemUIInputModule)
    {
        inputSystemUIInputModule.submit = InputActionReference.Create(inputActions.FindAction(submit.name));

    }
}
