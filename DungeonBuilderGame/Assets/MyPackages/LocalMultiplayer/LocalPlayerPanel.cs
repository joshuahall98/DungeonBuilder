using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerIDTxt;
    [SerializeField] Button readyUpBtn;
    
    public void SetPlayerIDText(string playerID)
    {
        playerIDTxt.text = playerID;
    }

    public Button GetReadyUpBtn()
    {
        return readyUpBtn;
    }
}
