using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class LocalPlayerPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerIDTxt;
    [SerializeField] Button readyUpBtn;

    bool ready = false;
    
    public void SetPlayerIDText(string playerID)
    {
        playerIDTxt.text = playerID;
    }

    public Button GetReadyUpBtn()
    {
        return readyUpBtn;
    }

    public void ReadyUp()
    {
        if (!ready)
        {
            ready = true;
            Debug.Log("Ready");
        }
    }
}
