using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//player can contact dice roll manager
public class DiceRollManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPool<int>> sixSidedOneToThreeDie = new List<ObjectPool<int>>();

    DiceRollManager diceRollManagerInstance;

    private void Awake()
    {
        diceRollManagerInstance = this;
    }

    public int RollSixSidedOneToThreeDie()
    {
        var result  = RandomUtility.ObjectPoolCalculator(sixSidedOneToThreeDie);

        Debug.Log($"Die has rolled {result}");

        return result;
    }
}
