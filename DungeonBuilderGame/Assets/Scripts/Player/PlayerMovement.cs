using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int currentNumberOfMoves;

    public void SetCurrentNumberOfMoves(int amount)
    {
        currentNumberOfMoves = amount;
    }
}
