using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public interface ILocalMultiplayer
{
    public void NewUser(InputUser user);
}
