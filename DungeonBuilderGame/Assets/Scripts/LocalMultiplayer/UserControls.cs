using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserControls : MonoBehaviour, IUserControls
{
    public IInputActionCollection CreateNewUserControls()
    {
        var controls = new Controls();

        return controls;
    }
}
