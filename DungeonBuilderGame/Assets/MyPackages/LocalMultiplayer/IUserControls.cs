using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IUserControls
{
    /// <summary>
    /// This method returns the InputActionCollection generated from the InputActionMap
    /// </summary>
    public IInputActionCollection CreateNewIInputActionCollection();
}
