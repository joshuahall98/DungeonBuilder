using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//player can contact dice roll manager
public class DiceRollManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPool<int>> sixSidedOneToThreeDie = new List<ObjectPool<int>>();

    public static DiceRollManager diceRollManagerInstance;

    int activeDie = 1;

    private void Awake()
    {
        diceRollManagerInstance = this;
    }

    public void SetActiveDie(int die)
    {
        activeDie = die;
    }

    public int RollActiveDie()
    {
        var result = 0;

        switch (activeDie)
        {
            case 1:
                result = RollSixSidedOneToThreeDie();
                break;
            default:
                result = 0;
                break;
        }

        return result;
    }

    private int RollSixSidedOneToThreeDie()
    {
        var result  = RandomUtility.ObjectPoolCalculator(sixSidedOneToThreeDie);

        Debug.Log($"Die has rolled {result}");

        return result;
    }
}
