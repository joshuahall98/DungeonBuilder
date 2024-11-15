using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LocalPlayerLobbyInputController : MonoBehaviour, ILocalPlayerSetup
{
    [SerializeField] InputActionReference submit;

    //DEPREACTED
    public void SetupPlayerUIControls(IInputActionCollection inputActions, InputSystemUIInputModule inputSystemUIInputModule)
    {
        /*controls = (Controls)inputActions;

        inputSystemUIInputModule.move = InputActionReference.Create(controls.UI.Navigate);
        inputSystemUIInputModule.submit = InputActionReference.Create(controls.UI.Submit);*/

    }

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
