using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPool<int>> sixSidedOneToThreeDie = new List<ObjectPool<int>>();

    public int RollSixSidedOneToThreeDie()
    {
        return RandomUtility.ObjectPoolCalculator(sixSidedOneToThreeDie);
    }
}
