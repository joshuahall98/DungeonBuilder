using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LocalPlayerLobbyInputController : MonoBehaviour, ILocalPlayerSetup
{
    [SerializeField] InputActionReference submit;
    //[SerializeField] InputActionReference move;

    public void SetupPlayerPanel(GameObject playerPanel, MultiplayerEventSystem multiplayerEventSystem)
    {
        multiplayerEventSystem.playerRoot = playerPanel;
        multiplayerEventSystem.firstSelectedGameObject = playerPanel.GetComponent<LocalPlayerPanel>().GetReadyUpBtn().gameObject;
    }

    public void SetupPlayerUIControls(InputActionAsset inputActions, InputSystemUIInputModule inputSystemUIInputModule)
    {
        inputSystemUIInputModule.submit = InputActionReference.Create(inputActions.FindAction(submit.name));

    }
}
