using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;

public class LocalPlayerCreation : MonoBehaviour, ILocalMultiplayer
{
    public void ProvideNewUserInputActions(IInputActionCollection inputActions, InputUser user)
    {
        GetComponent<PlayerInputController>().SetInputActions(inputActions);

        GetComponent<PlayerDataController>().SetPlayerInputUserData(user);
    }

    public void ProvideUIInputModule(MultiplayerUI multiplayerUI)
    {
        GetComponent<PlayerInputController>().MapInputActionsToUI(multiplayerUI);
    }
}
