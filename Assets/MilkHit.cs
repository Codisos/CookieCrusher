using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkHit : MonoBehaviour
{
    [SerializeField] MilkCannon cannon;

    public void OnMilkHit()
    {
        cannon.OnMilk();
    }
}
