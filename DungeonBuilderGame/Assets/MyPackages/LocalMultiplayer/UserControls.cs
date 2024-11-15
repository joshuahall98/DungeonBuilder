using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserControls : MonoBehaviour, IUserControls
{

    [SerializeField] InputActionAsset _actionAsset;

    //DEPRECATED
    public IInputActionCollection CreateNewIInputActionCollection()
    {
        var controls = new Controls();

        return controls;
    }

    public InputActionAsset CreateNewInputActionAsset()
    {
        var clonedAsset = InputActionAsset.FromJson(_actionAsset.ToJson());

        return clonedAsset;
    }
}
